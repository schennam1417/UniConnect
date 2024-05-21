using Uniconnect.Models.DTO;
using UniConnect.Models.DTO;

namespace Uniconnect.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO obj);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO obj);

    }
}
