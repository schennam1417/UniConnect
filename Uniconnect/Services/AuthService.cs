using Microsoft.AspNetCore.Identity.Data;
using Uniconnect.Models;
using Uniconnect.Models.DTO;
using Uniconnect.Services.IServices;
using UniConnect.Models.DTO;
using UniConnectUtility;

namespace Uniconnect.Services
{
    public class AuthService : BaseService,IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string StudentUrl;

        public AuthService(IHttpClientFactory clientFactory,IConfiguration configuration) : base(clientFactory)
        {
            this._clientFactory = clientFactory;
            StudentUrl = configuration.GetValue<string>("ServiceUrls:UniConnectAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = StudentUrl + "/api/v1/UsersAuth/login"
            });

        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = StudentUrl + "/api/v1/UsersAuth/register"
            });

        }
    }
}
