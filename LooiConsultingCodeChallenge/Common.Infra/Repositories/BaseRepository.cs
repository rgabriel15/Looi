using Common.Domain.Entities;
using Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Common.Infra.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region Constants
        protected readonly ILogger Logger;
        protected readonly EfContext EfContext;
        #endregion

        #region Constructors
        protected BaseRepository(ILogger logger, EfContext efContext)
        {
            Logger = logger;
            EfContext = efContext;
        }
        #endregion

        #region Methods
        public async Task<T?> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                var valueTask = await EfContext.AddAsync(entity, cancellationToken);
                await EfContext.SaveChangesAsync(cancellationToken);
                return valueTask.Entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return default!;
            }
        }

        public async Task<uint?> DeleteAsync(uint id, CancellationToken cancellationToken)
        {
            try
            {
                T? entity = await EfContext.FindAsync<T>(id, cancellationToken);

                if (entity == null)
                {
                    return null;
                }

                var entityEntry = EfContext.Remove(entity);

                if (entityEntry.Entity == null)
                {
                    return null;
                }

                await EfContext.SaveChangesAsync(cancellationToken);
                return entityEntry.Entity.Id;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }

        public async Task<T?> GetByIdAsync(uint id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await EfContext.FindAsync(typeof(T), [id], cancellationToken);
                return entity == null ? null : entity as T;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return default!;
            }
        }

        public async Task<BaseList<T>?> ListAsync(uint page, uint pageLength, CancellationToken cancellationToken)
        {
            try
            {
                var offset = (int)(page * pageLength);
                var list = await EfContext.Set<T>()
                    //.OrderBy(x => x.Id)
                    .Skip(offset)
                    .Take((int)pageLength)
                    .ToListAsync(cancellationToken);

                if (list == null || list.Count == 0)
                {
                    return default;
                }

                return new BaseList<T>
                {
                    Page = page,
                    PageLength = pageLength,
                    List = list
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return default!;
            }
        }

        public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                var entryEntity = EfContext.Update<T>(entity);
                await EfContext.SaveChangesAsync(cancellationToken);
                return entryEntity.Entity;
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
