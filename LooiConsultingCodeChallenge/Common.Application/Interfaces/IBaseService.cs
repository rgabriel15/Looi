using Common.Domain.Entities;

namespace Common.Application.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(uint id);
        Task<BaseList<T>?> ListAsync(uint page, uint pageLength);
        Task<T?> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<uint?> DeleteAsync(uint id);
    }
}
