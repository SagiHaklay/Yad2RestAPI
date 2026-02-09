using Yad2RestAPI.Models.RealEstate;

namespace Yad2RestAPI.Models.Account
{
    public class ProfileDetails
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? HouseNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<RealEstateAdSummary> RealEstateAds { get; set; } = new List<RealEstateAdSummary>();
        public ICollection<RealEstateAdSummary> FavoriteAds { get; set; } = new List<RealEstateAdSummary>();
    }
}
