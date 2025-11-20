using Microsoft.AspNetCore.Identity;

namespace FirstWebApp.Models
{
    public class User : IdentityUser
    {
        public string? ProfilePictureUrl { get; set; }
    }
}
