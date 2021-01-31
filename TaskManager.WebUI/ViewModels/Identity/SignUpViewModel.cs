using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebUI.ViewModels.Identity
{
    /// <summary>
    /// ViewModel for the sign-up form
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// E-mail used by the user
        /// </summary>
        [Required(ErrorMessage = "Enter the e-mail address")]
        [EmailAddress(ErrorMessage = "Incorrect e-mail format")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Incorrect e-mail format")]
        public string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password should contain at least 6 symbols")]
        public string Password { get; set; }

        /// <summary>
        /// Confirmation of the password
        /// </summary>
        [Required(ErrorMessage = "Enter the confirmation password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
