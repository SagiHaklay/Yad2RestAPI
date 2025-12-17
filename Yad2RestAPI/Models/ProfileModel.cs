namespace Yad2RestAPI.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone {  get; set; }
        public bool AllowAds { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? HouseNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
