using System.Linq.Expressions;
using UniConnectAPI.Models;

namespace UniConnectAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task RemoveAsync(T entity);
        Task Save();
    }
}
