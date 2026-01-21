using Yad2RestAPI.Models.RealEstate;

namespace Yad2RestAPI.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool AllowAds { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? HouseNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<RealEstateAdModel> RealEstateAds { get; } = new List<RealEstateAdModel>();
    }
}
