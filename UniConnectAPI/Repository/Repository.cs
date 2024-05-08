using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Repository.IRepository;

namespace UniConnectAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        

        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            this.dbSet=_db.Set<T>();
        }
        

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

      
    }

}

