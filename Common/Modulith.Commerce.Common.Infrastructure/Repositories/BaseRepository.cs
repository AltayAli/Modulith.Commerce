using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Infrastructure.Extensions;
using System.Linq.Expressions;

namespace Modulith.Commerce.Common.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
                        where TEntity : BaseEntity
                        where TContext : DbContext
    {
        protected DbSet<TEntity> EntityDbSet { get; private set; }
        protected IHttpContextAccessor HttpContextAccesor { get; private set; }
        protected IDateTimeProvider DateTimeProvider { get; private set; }
        protected TContext DbContext { get; private set; }

        public BaseRepository(TContext dataContext,
                                IHttpContextAccessor httpContextAccessor,
                                IDateTimeProvider dateTimeProvider)
        {
            DbContext = dataContext;
            DateTimeProvider = dateTimeProvider;
            EntityDbSet = dataContext.Set<TEntity>();
            HttpContextAccesor = httpContextAccessor;
        }

        public virtual async Task<TEntity?> SelectSimpleOrDefaultAsync(FilteringOptions<TEntity> listingOptions, CancellationToken cancellationToken = default)
        {
            TEntity? model = await (await SelectAsync(listingOptions, cancellationToken)).FirstOrDefaultAsync(cancellationToken);

            return model;
        }

        public virtual Task<IQueryable<TEntity>> SelectAsync(FilteringOptions<TEntity> listingOptions = null, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(listingOptions);
            return Task.Run(() => Select(listingOptions), cancellationToken);
        }

        public virtual IQueryable<TEntity> Select(FilteringOptions<TEntity> listingOptions = null)
        {
            IQueryable<TEntity> queryable;

            queryable = EntityDbSet.AsQueryable();

            if (listingOptions == null)
                listingOptions = new FilteringOptions<TEntity>();

            if (!listingOptions.IsSearchForAll)
            {
                listingOptions.Predicates.Add(x => x.DeletedDate == null && x.DeletedById == null);
            }
            else
            {

                queryable = queryable.IgnoreQueryFilters();
            }

            foreach (var relation in listingOptions.Relations)
            {
                queryable = queryable.Include(relation);
            }

            queryable = listingOptions.Predicates.Where(x => x != null).Aggregate(queryable, (current, predicate) => current.Where(predicate));

            if (listingOptions.IsLoadingAsNoTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            return queryable.OrderByDescending(x => x.AddedDate);
        }

        public virtual async Task<TEntity?> InsertAsync(TEntity model, CancellationToken cancellationToken = default)
        {
            model.MarkAsAdded(HttpContextAccesor.GetUserId(), DateTimeProvider.UtcNow);
            await EntityDbSet.AddAsync(model, cancellationToken);

            return model;
        }
        public virtual async Task<TEntity?> UpdateAsync(TEntity model, CancellationToken cancellationToken = default)
        {

            TEntity? entity = await SelectSimpleOrDefaultAsync(new FilteringOptions<TEntity>()
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<TEntity, bool>>>()
                {
                    x=>x.Id.Equals(model.Id)
                }
            }, cancellationToken);

            if (entity == null)
                throw new NullReferenceException();

            model.MarkAsModified(HttpContextAccesor.GetUserId(), DateTimeProvider.UtcNow);

            var entry = DbContext.Entry(entity);

            entry.CurrentValues.SetValues(model);

            var ownedNavigations = entry.Metadata.GetNavigations()
                .Where(n => n.TargetEntityType.IsOwned() && n.PropertyInfo != null);

            foreach (var navigation in ownedNavigations)
            {
                var ownedEntityValue = navigation.PropertyInfo!.GetValue(model);
                if (ownedEntityValue != null)
                {
                    var ownedEntry = entry.Reference(navigation.Name).TargetEntry;
                    if (ownedEntry != null)
                    {

                        ownedEntry.CurrentValues.SetValues(ownedEntityValue);
                    }
                    else
                    {

                        navigation.PropertyInfo.SetValue(entity, ownedEntityValue);
                    }
                }
            }

            entry.State = EntityState.Modified;

            foreach (var domainEvent in model.GetDomainEvents())
            {
                entity.AddDomainEvent(domainEvent);
            }
            model.ClearDomainEvents();

            return entity;
        }

        public virtual async Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity? model = entity.Clone() as TEntity;

            if (entity is null)
                throw new NullReferenceException();

            if (model is null)
                throw new NullReferenceException();

            Guid? deletedBy = HttpContextAccesor.GetUserId();
            DateTime deletedDate = DateTimeProvider.UtcNow;

            model.MarkAsDeleted(deletedBy, deletedDate);
            EntityDbSet.Attach(entity);
            DbContext.Entry(entity).CurrentValues.SetValues(model);

            entity.MarkAsDeleted(deletedBy, deletedDate);

            return entity;
        }
    }
}
