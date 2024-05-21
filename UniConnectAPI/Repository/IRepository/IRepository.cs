using System.Linq.Expressions;
using UniConnectAPI.Models;

namespace UniConnectAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,int PageSize=0,int PageNumber=1);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task RemoveAsync(T entity);
        Task Save();
    }
}
