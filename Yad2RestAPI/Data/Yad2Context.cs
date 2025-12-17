using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yad2RestAPI.Models;

namespace Yad2RestAPI.Data
{
    public class Yad2Context : IdentityDbContext<AppUser>
    {
        public Yad2Context(DbContextOptions<Yad2Context> options) : base(options)
        {
            
        }
        public DbSet<ProfileModel> Profiles { get; set; }
    }
}
