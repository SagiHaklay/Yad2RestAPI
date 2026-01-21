using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using Yad2RestAPI.Models;
using Yad2RestAPI.Models.RealEstate;

namespace Yad2RestAPI.Data
{
    public class Yad2Context : IdentityDbContext<AppUser>
    {
        public Yad2Context(DbContextOptions<Yad2Context> options) : base(options)
        {
            
        }
        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<RealEstateAdModel> RealEstateAds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var strListConverter = GetListValueConverter<List<string>>();
            var strListComparer = GetListValueComparer<string>();
            builder.Entity<RealEstateAdModel>()
                .Property(e => e.ImageUrls)
                .HasConversion(strListConverter, strListComparer);
            builder.Entity<RealEstateAdModel>()
                .Property(e => e.VideoUrls)
                .HasConversion(strListConverter, strListComparer);
            
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<Enum>()
                .HaveConversion<int>();
        }
        private static ValueConverter<T, string> GetListValueConverter<T>()
        {
            return new ValueConverter<T, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<T>(v, (JsonSerializerOptions?)null));
        }
        private static ValueComparer<ICollection<T>> GetListValueComparer<T>()
        {
            return new ValueComparer<ICollection<T>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList());
        }
    }
}
