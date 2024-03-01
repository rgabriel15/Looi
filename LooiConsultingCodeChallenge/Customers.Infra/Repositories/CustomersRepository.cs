using Common.Infra;
using Common.Infra.Repositories;
using Customers.Domain.Entities;
using Customers.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Customers.Infra.Repositories
{
    public sealed class CustomersRepository : BaseRepository<CustomerEntity>, ICustomersRepository
    {
        #region Constructrs
        public CustomersRepository(ILogger logger, EfContext efContext) : base(logger, efContext)
        {
        }
        #endregion

        #region Methods
        public async Task<CustomerEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await EfContext.Customers.FirstAsync(x => x.Email == email, cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return default!;
            }
        }
        #endregion
    }
}
