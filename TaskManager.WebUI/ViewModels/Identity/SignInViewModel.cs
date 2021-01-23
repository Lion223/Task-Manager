using System.ComponentModel.DataAnnotations;

namespace TaskManager.WebUI.ViewModels.Identity
{
    /// <summary>
    /// ViewModel for the sign-in form
    /// </summary>
    public class SignInViewModel
    {
        /// <summary>
        /// Email used to sign in
        /// </summary>
        [Required(ErrorMessage = "Enter the e-mail address")]
        [EmailAddress(ErrorMessage = "Incorrect e-mail format")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Incorrect e-mail format")]
        public string Email { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
