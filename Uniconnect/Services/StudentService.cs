using Uniconnect.Models;
using Uniconnect.Models.DTO;
using Uniconnect.Services.IServices;
using UniConnect.Models.DTO;
using UniConnectUtility;

namespace Uniconnect.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IHttpClientFactory _clientFactory;
        
        private string studentUrl;
        public StudentService(IHttpClientFactory clientFactory,IConfiguration configuration) : base(clientFactory) 
        {
            this._clientFactory = clientFactory;
            studentUrl = configuration.GetValue<string>("ServiceUrls:UniConnectAPI");
        }
        public Task<T> CreateAsync<T>(AddStudentDTO addstudentdto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = addstudentdto,
                Url = studentUrl + "/api/v1/StudentAPI",
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                
                Url = studentUrl + "/api/v1/StudentAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(string StudentID, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                
                Url = studentUrl + "/api/v1/StudentAPI/" + StudentID,
                Token = token
            });
        }

        public Task<T> RemoveAsync<T>(string StudentID, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                
                Url = studentUrl + "/api/v1/StudentAPI/" + StudentID,
                Token = token
            });
        }

        public Task<T?> SearchAsync<T>(string searchString, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = studentUrl + "/api/v1/StudentAPI/search?searchQuery=" + searchString,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(UpdateStudentDTO updateStudentDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = updateStudentDTO,
                Url = studentUrl + "/api/v1/StudentAPI/" + updateStudentDTO.StudentID,
                Token = token
            });
        }
    }
}
