using System.ComponentModel.DataAnnotations; // For validation attributes

namespace GovSchedulaWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email, phone, or username.")]
        [Display(Name = "Email, Phone, or Username")] // This is used for the <label>
        public string? LoginIdentifier { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)] // Hides input in some contexts
        public string? Password { get; set; }

        // We might add a "Remember Me" option later
        // public bool RememberMe { get; set; }
    }
}