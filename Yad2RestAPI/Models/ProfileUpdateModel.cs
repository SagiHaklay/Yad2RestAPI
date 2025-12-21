using System.ComponentModel.DataAnnotations;

namespace Yad2RestAPI.Models
{
    public class ProfileUpdateModel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "Invalid E-mail address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^05\\d{8}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "Name length must be at least 2 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Name length must be at least 2 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "House number is required.")]
        public int HouseNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
