using System.Linq.Expressions;
using UniConnectAPI.Models;

namespace UniConnectAPI.Repository.IRepository
{
    public interface IStudentRepository 
    {
        Task<List<Student>> GetAll(Expression<Func<Student,bool>> filter=null);
        Task<Student> Get(Expression<Func<Student,bool>> filter = null);
        Task Create(Student student);
        Task Remove(Student student);

        Task Save();

    }
}
