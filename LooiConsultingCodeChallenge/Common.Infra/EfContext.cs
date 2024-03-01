using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Infra
{
    public sealed class EfContext : DbContext
    {
        #region Properties
        public DbSet<CustomerEntity> Customers { get; set; }
        #endregion

        #region Constructors
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {   
        }
        #endregion
    }
}
