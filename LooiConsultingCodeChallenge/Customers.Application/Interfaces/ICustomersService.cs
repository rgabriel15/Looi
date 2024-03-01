using Common.Application.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Application.Interfaces
{
    public interface ICustomersService : IBaseService<CustomerEntity>
    {
        Task<CustomerEntity> GetByEmailAsync(string email);
        Task GenerateMoqDataAsync();
    }
}
