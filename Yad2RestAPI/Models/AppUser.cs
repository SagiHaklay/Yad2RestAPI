using Microsoft.AspNetCore.Identity;

namespace Yad2RestAPI.Models
{
    public class AppUser : IdentityUser
    {
        public int ProfileId { get; set; }
    }
}
