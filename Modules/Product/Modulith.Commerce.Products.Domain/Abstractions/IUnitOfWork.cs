using System;
using System.Collections.Generic;
using System.Text;

namespace Modulith.Commerce.Products.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
