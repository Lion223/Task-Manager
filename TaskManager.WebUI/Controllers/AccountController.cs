using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Domain.Entities.Identity;
using TaskManager.WebUI.ViewModels;

namespace TaskManager.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Private members

        /// <summary>
        /// Control the user settings
        /// </summary>
        private readonly UserManager<UserModel> _userManager;

        /// <summary>
        /// Control the sign-in process
        /// </summary>
        private readonly SignInManager<UserModel> _signInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Receive Identity tools through DI
        /// </summary>
        /// <param name="userManager">User tools</param>
        /// <param name="signInManager">Sign-in tools</param>
        public AccountController(UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Actions

        /// <summary>
        /// The sign-up GET action
        /// </summary>
        /// <returns>Sign-up view</returns>
        [Route("Signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// The sign-up POST action
        /// </summary>
        /// <returns>Sign-up result</returns>
        [Route("Signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            UserModel user = await _userManager.FindByEmailAsync("Tester@tester.com" /* signUpModel.Email */);
            if (user == null)
            {
                user = new UserModel();
                user.UserName = "Tester@tester.com";
                user.Email = "Tester@tester.com";

                var result = await _userManager.CreateAsync(user, "Tester");
                return RedirectToAction("Signin");
            }

            return View();
        }

        /// <summary>
        /// The sign-in GET action
        /// </summary>
        /// <returns>Sign-in view</returns>
        [Route("Signin")]
        [HttpGet]
        public IActionResult SignIn()
        {
            // Dispose of "?ReturnUrl=%2F" at the end of url
            if (!string.IsNullOrEmpty(Request.QueryString.Value))
                return RedirectToAction("Signin");
            return View();
        }

        /// <summary>
        /// The sign-in POST action
        /// </summary>
        /// <returns>Sign-in result</returns>
        [Route("Signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            UserModel user = new UserModel { Email = "Tester@tester.com"/* signInModel.Email */};
            var result = await _signInManager.PasswordSignInAsync("Tester@tester.com", "Tester", false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("List", "Tasks");   
            }

            return View();
        } 

        /// <summary>
        /// The sign-out action
        /// </summary>
        /// <returns>Redirection to sign-in view</returns>
        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        #endregion
    }
}
