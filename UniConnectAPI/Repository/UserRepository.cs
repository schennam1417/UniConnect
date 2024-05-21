using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Models.DTO;
using UniConnectAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace UniConnectAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private string secretKey;

        public UserRepository(ApplicationDbContext db,IConfiguration configuration,
            UserManager<ApplicationUser> userManager,IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            this._db = db;
            this.secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
        }
        public bool IsUniqueUser(string username)
        {
            //var user=_db.LocalUsers.FirstOrDefault(x=>x.UserName == username);
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
            
            if (user == null) 
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.ApplicationUsers.
                FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            //&& u.Password == loginRequestDTO.Password);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);


            if (user == null || isValid==false) 
            {
                return new LoginResponseDTO()
                {
                    Token="",
                    User=null
                };
            }
            //if user is found generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescritptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token=tokenHandler.CreateToken(tokenDescritptor);
            LoginResponseDTO loginResponse = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User =  _mapper.Map<UserDTO>(user),
                //Role=roles.FirstOrDefault()

            };
            return loginResponse;

        }

        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
                Email=registrationRequestDTO.UserName,
                NormalizedEmail=registrationRequestDTO.UserName.ToUpper(),
                //PasswordHash = registrationRequestDTO.Password,
                Name = registrationRequestDTO.Name,
                //Role = registrationRequestDTO.Role
            };
            try
            {
                var result = await _userManager.CreateAsync(user,registrationRequestDTO.Password);
                if (result.Succeeded) 
                {
                    if(!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _roleManager.CreateAsync(new IdentityRole("User"));

                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                    var userToReturn=await _db.ApplicationUsers.FirstOrDefaultAsync(u=>u.UserName == registrationRequestDTO.UserName);
                    //return new UserDTO()
                    //{
                    //    Id = userToReturn.Id,
                    //    UserName = registrationRequestDTO.UserName,
                    //    Name = registrationRequestDTO.Name
                    //};
                    if (userToReturn != null)
                    {
                        return _mapper.Map<UserDTO>(userToReturn);
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            // _db.LocalUsers.Add(user);
            //await _db.SaveChangesAsync();
           
            return new UserDTO();
        }
    }
}
