namespace TaskManager.WebUI.ViewModels.Identity
{
    /// <summary>
    /// ViewModel for the sign-up form
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// Email used for the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Confirmation of the password
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
