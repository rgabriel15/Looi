using Customers.Domain.Entities;
using Customers.Domain.Interfaces;

namespace Customers.UnitTests
{
    public class CustomersRepositoryUnitTest
    {
        #region Constants
        private static readonly TimeSpan _Timeout = TimeSpan.FromSeconds(30);
        private ICustomersRepository _Repository;
        #endregion

        #region Constructors
        public CustomersRepositoryUnitTest(ICustomersRepository repository)
        {
            _Repository = repository;
        }
        #endregion

        #region Methods
        [Fact]
        internal async void AddAsync()
        {
            var email = "test@email.com";
            var customer = new CustomerEntity
            {
                Name = "Test Name",
                DNI = "test dni",
                Phone = "test phone",
                Mobile = "test mobile",
                Email = "test@email.com",
                Address = "test address",
                State = "test state",
                City = "test city",
            };
            
            var cancelationTokenSource = new CancellationTokenSource(_Timeout);
            var newEntity = await _Repository.AddAsync(customer, cancelationTokenSource.Token);
            Assert.Equal(newEntity?.Email, email);
        }

        [Fact]
        internal async void GetByIdAsync()
        {
            uint id = 1;
            var cancelationTokenSource = new CancellationTokenSource(_Timeout);
            var newEntity = await _Repository.GetByIdAsync(id, cancelationTokenSource.Token);
            Assert.Equal(newEntity?.Id, id);
        }

        [Fact]
        internal async void GetByEmailAsync()
        {
            var email = "peter.parker@marvel.com";
            var cancelationTokenSource = new CancellationTokenSource(_Timeout);
            var entity = await _Repository.GetByEmailAsync(email, cancelationTokenSource.Token);
            Assert.Equal(entity?.Email, email);
        }

        [Fact]
        internal async void ListAsync()
        {
            var cancelationTokenSource = new CancellationTokenSource(_Timeout);
            var list = await _Repository.ListAsync(page: 0, 50, cancelationTokenSource.Token);
            Assert.True(list?.List?.Any());
        }

        [Fact]
        internal async void UpdateAsync()
        {
            var email = "new@email.com";
            var customer = new CustomerEntity
            {
                Name = "Test Name",
                DNI = "test dni",
                Phone = "test phone",
                Mobile = "test mobile",
                Email = email,
                Address = "test address",
                State = "test state",
                City = "test city",
                Id = 2
            };

            var cancelationTokenSource = new CancellationTokenSource(_Timeout);
            var newEntity = await _Repository.UpdateAsync(customer, cancelationTokenSource.Token);
            Assert.Equal(newEntity?.Email, email);
        }

        [Fact]
        internal async void DeleteAsync()
        {
            uint id = 3;
            var cancelationTokenSource = new CancellationTokenSource(_Timeout);
            var deletedId = await _Repository.DeleteAsync(id, cancelationTokenSource.Token);
            var deletedEntity = await _Repository.GetByIdAsync(id, cancelationTokenSource.Token);
            Assert.Equal(deletedId, id);
            Assert.Null(deletedEntity);
        }
        #endregion
    }
}
