using Uniconnect.Models.DTO;
using UniConnect.Models.DTO;

namespace Uniconnect.Services.IServices
{
    public interface IStudentService
    {
        //Task<List<Student>> GetAllAsync(Expression<Func<Student,bool>> filter=null);
        //Task<Student> GetAsync(Expression<Func<Student,bool>> filter = null);

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(string StudentID);
        Task<T> CreateAsync<T>(AddStudentDTO addstudentdto);
        
        Task<T> RemoveAsync<T>(string StudentID);

        Task<T> UpdateAsync<T>(string StudentID,UpdateStudentDTO updatestudentdto);
    }
}
