using System.ComponentModel.DataAnnotations;

namespace Yad2RestAPI.Models
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "Invalid E-mail address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [Length(8, 20, ErrorMessage = "Password length must be 8-20 characters.")]
        [RegularExpression("(\\d.*[a-zA-Z])|([a-zA-Z].*\\d)", ErrorMessage = "Password must contain English letters and digits.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^05\\d{8}$", ErrorMessage = "Invalid phone number")]
        public string Phone {  get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "Name length must be at least 2 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2, ErrorMessage = "Name length must be at least 2 characters.")]
        public string LastName { get; set; }
        public bool AllowAds { get; set; }
    }
}
