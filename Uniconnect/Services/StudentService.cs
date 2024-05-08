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
        public Task<T> CreateAsync<T>(AddStudentDTO addstudentdto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = addstudentdto,
                Url = studentUrl + "/api/StudentAPI"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                
                Url = studentUrl + "/api/StudentAPI"
            });
        }

        public Task<T> GetAsync<T>(string StudentID)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                
                Url = studentUrl + "/api/StudentAPI/"+ StudentID
            });
        }

        public Task<T> RemoveAsync<T>(string StudentID)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                
                Url = studentUrl + "/api/StudentAPI/"+ StudentID
            });
        }

        public Task<T> UpdateAsync<T>(string StudentID, UpdateStudentDTO updatestudentdto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = updatestudentdto,
                Url = studentUrl + "/api/StudentAPI/"+StudentID
            });
        }
    }
}
