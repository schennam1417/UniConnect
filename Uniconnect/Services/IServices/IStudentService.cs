using Uniconnect.Models.DTO;
using UniConnect.Models.DTO;

namespace Uniconnect.Services.IServices
{
    public interface IStudentService
    {
        //Task<List<Student>> GetAllAsync(Expression<Func<Student,bool>> filter=null);
        //Task<Student> GetAsync(Expression<Func<Student,bool>> filter = null);

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(string StudentID, string token);
        Task<T> CreateAsync<T>(AddStudentDTO addstudentdto, string token);
        
        Task<T> RemoveAsync<T>(string StudentID, string token);

        Task<T> UpdateAsync<T>(UpdateStudentDTO updateStudentDTO, string token);

        Task<T?> SearchAsync<T>(string searchString, string token);
    }
}
