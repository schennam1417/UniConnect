using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Repository.IRepository;

namespace UniConnectAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task Create(Student student)
        {
           await  _db.Students.AddAsync(student);
            await Save();
        }

        public async Task<Student> Get(Expression<Func<Student,bool>> filter = null)
        {
            IQueryable<Student> query = _db.Students;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<Student>> GetAll(Expression<Func<Student,bool>> filter = null)
        {
            IQueryable<Student> query = _db.Students;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(Student student)
        {
             _db.Students.Remove(student);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
