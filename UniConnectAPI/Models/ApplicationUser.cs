using Microsoft.AspNetCore.Identity;

namespace UniConnectAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
