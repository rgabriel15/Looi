using Common.Domain.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Domain.Interfaces
{
    public interface ICustomersRepository : IBaseRepository<CustomerEntity>
    {
        Task<CustomerEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
