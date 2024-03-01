using Common.Application.Services;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Interfaces;

namespace Customers.Application.Services
{
    public sealed class CustomersService : BaseService<CustomerEntity>, ICustomersService
    {
        #region Constructors
        public CustomersService(ICustomersRepository repository) : base(repository)
        {
        }
        #endregion

        #region Methods
        public async Task<CustomerEntity> GetByEmailAsync(string email)
        {
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            var entity = await ((ICustomersRepository)Repository).GetByEmailAsync(email, cancellationTokenSource.Token);
            return entity;
        }

        public async Task GenerateMoqDataAsync()
        {
            var customers = new List<CustomerEntity>
            {
                new() {
                    Name = "Peter Parker",
                    DNI = "000",
                    Phone = "999999990",
                    Mobile = "999999991",
                    Email = "peter.parker@marvel.com",
                    Address = "Address 0",
                    State = "New York",
                    City = "New York",
                },
                new() {
                    Name = "Clark Kent",
                    DNI = "001",
                    Phone = "999999992",
                    Mobile = "999999993",
                    Email = "clark.kent@dailyplanet.com",
                    Address = "Address 1",
                    State = "Delaware",
                    City = "Metropolis",
                },
                new() {
                    Name = "Bruce Wayne",
                    DNI = "002",
                    Phone = "999999994",
                    Mobile = "999999995",
                    Email = "bruce.wayne@wayneindustries.com",
                    Address = "Address 2",
                    State = "New Jersey",
                    City = "Gotham",
                }
            };

            var tasks = new List<Task>();
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            foreach (var customer in customers)
            {
                tasks.Add(Repository.AddAsync(customer, cancellationTokenSource.Token));
            }
            await Task.WhenAll(tasks);
        }
        #endregion

    }
}
