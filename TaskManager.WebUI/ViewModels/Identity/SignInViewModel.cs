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
        public string Email { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
