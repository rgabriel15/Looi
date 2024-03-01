using Common.Domain.Entities;

namespace Common.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(uint id, CancellationToken cancellationToken);
        Task<BaseList<T>?> ListAsync(uint page, uint pageLength, CancellationToken cancellationToken);
        Task<T?> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<uint?> DeleteAsync(uint id, CancellationToken cancellationToken);
    }
}
