using System.ComponentModel.DataAnnotations; // For validation attributes

namespace GovSchedulaWeb.Models.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [Display(Name = "Email")] // This is used for the <label>
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)] // Hides input in some contexts
        public string? Password { get; set; }

        // We might add a "Remember Me" option later
        // public bool RememberMe { get; set; }
    }
}