using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repositories
{
    public interface IAsyncGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int? id);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
      
    }
}

