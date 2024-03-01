using Common.Application.Interfaces;
using Common.Domain.Entities;
using Common.Domain.Interfaces;

namespace Common.Application.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        #region Constants
        protected const byte DefaultPage = 0;
        protected const byte DefaultPageLength = 50;
        protected static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
        protected readonly IBaseRepository<T> Repository;
        #endregion

        #region Constructors
        protected BaseService(IBaseRepository<T> repository)
        {
            Repository = repository;
        }
        #endregion

        #region Methods
        public async Task<T?> AddAsync(T entity)
        {
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            var newEntity = await Repository.AddAsync(entity, cancellationTokenSource.Token);
            return newEntity;
        }

        public async Task<uint?> DeleteAsync(uint id)
        {
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            var deletedId = await Repository.DeleteAsync(id, cancellationTokenSource.Token);
            return deletedId;
        }

        public async Task<T?> GetByIdAsync(uint id)
        {
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            var entity = await Repository.GetByIdAsync(id, cancellationTokenSource.Token);
            return entity;
        }

        public async Task<BaseList<T>?> ListAsync(uint page = DefaultPage, uint pageLength = DefaultPageLength)
        {
            pageLength = pageLength > 0 ? pageLength : DefaultPageLength;
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            var entity = await Repository.ListAsync(page, pageLength, cancellationTokenSource.Token);
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            using var cancellationTokenSource = new CancellationTokenSource(Timeout);
            var updatedEntity = await Repository.UpdateAsync(entity, cancellationTokenSource.Token);
            return updatedEntity;
        }
        #endregion
    }
}
