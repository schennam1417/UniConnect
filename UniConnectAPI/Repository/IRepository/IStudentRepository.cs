using System.Linq.Expressions;
using UniConnectAPI.Models;

namespace UniConnectAPI.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        //Task<List<Student>> GetAllAsync(Expression<Func<Student,bool>> filter=null);
        //Task<Student> GetAsync(Expression<Func<Student,bool>> filter = null);
        Task CreateAsync(Student student);
        //Task RemoveAsync(Student student);

        Task<Student> UpdateAsync(Student student);

        Task<List<Student>> SearchAsync(string query);

    }
}
