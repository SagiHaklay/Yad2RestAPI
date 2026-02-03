using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using Yad2RestAPI.Models;
using Yad2RestAPI.Models.Account;
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
            base.OnModelCreating(builder);
            var strListConverter = GetListValueConverter<string>();
            var strListComparer = GetListValueComparer<string>();
            builder.Entity<RealEstateAdModel>()
                .Property(e => e.ImageUrls)
                .HasConversion(strListConverter, strListComparer);
            builder.Entity<RealEstateAdModel>()
                .Property(e => e.VideoUrls)
                .HasConversion(strListConverter, strListComparer);
            builder.Entity<ProfileModel>()
                .HasMany(e => e.FavoriteAds)
                .WithMany();
            builder.Entity<ProfileModel>()
                .HasMany(e => e.RealEstateAds)
                .WithOne(e => e.Publisher)
                .HasForeignKey(e => e.PublisherId)
                .IsRequired(false);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<Enum>()
                .HaveConversion<int>();
        }
        private static ValueConverter<IList<T>, string> GetListValueConverter<T>()
        {
            return new ValueConverter<IList<T>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<T>>(v, (JsonSerializerOptions?)null));
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
