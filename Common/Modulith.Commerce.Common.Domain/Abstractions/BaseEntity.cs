namespace Modulith.Commerce.Common.Domain.Abstractions
{
    public class BaseEntity : ICloneable
    {
        public Guid Id { get; init; }
        public Guid? AddedById { get; private set; }
        public DateTime? AddedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public Guid? ModifiedById { get; private set; }
        public DateTime? DeletedDate { get; private set; }
        public Guid? DeletedById { get; private set; }

        private List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _events;

        public void AddDomainEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void RemoveDomainEvent(IDomainEvent @event)
        {
            _events.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        public object Clone()
        {
            var clone = (BaseEntity)this.MemberwiseClone();

            clone._events = new List<IDomainEvent>(_events);

            return clone;
        }
        public void MarkAsAdded(Guid? addedBy, DateTime addedDate)
        {
            AddedById = addedBy;
            AddedDate = addedDate;
        }
        public void MarkAsModified(Guid? modifiedBy, DateTime modifiedDate)
        {
            ModifiedById = modifiedBy;
            ModifiedDate = modifiedDate;
        }
        public virtual void MarkAsDeleted(Guid? deletedBy, DateTime deletedDate)
        {
            DeletedById = deletedBy;
            DeletedDate = deletedDate;
        }
    }
}
