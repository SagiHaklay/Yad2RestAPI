using System.ComponentModel.DataAnnotations;

namespace Yad2RestAPI.Models
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "Invalid E-mail address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
