using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Domain.Entities.Identity;
using TaskManager.WebUI.ViewModels.Identity;

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
        [Route("sign-up")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// The sign-up POST action
        /// </summary>
        /// <returns>Sign-up result</returns>
        [Route("sign-up")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                UserModel user = await _userManager.FindByEmailAsync(signUpModel.Email);
                if (user == null)
                {
                    user = new UserModel();
                    user.UserName = signUpModel.Email;
                    user.Email = signUpModel.Email;

                    var result = await _userManager.CreateAsync(user, signUpModel.Password);

                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View();
                    }

                    return RedirectToAction("Signin");
                }

                ModelState.AddModelError("Email", "Email has already been taken");
                return View();
            }

            return View();
        }

        /// <summary>
        /// The sign-in GET action
        /// </summary>
        /// <returns>Sign-in view</returns>
        [Route("sign-in")]
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
        [Route("sign-in")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Read", "Tasks");
                }

                ModelState.AddModelError("Email", "The user with specified credentials is not registered");
                return View();
            }

            return View();
        }

        /// <summary>
        /// GET sign-out action
        /// </summary>
        /// <returns>Partial view that represents a modal window for signing out</returns>
        [Route("sign-out")]
        [HttpGet]
        public IActionResult SignOut()
        {
            return PartialView("Signout");
        }

        /// <summary>
        /// POST sign-out action
        /// </summary>
        /// <returns>Redirection to sign-in view</returns>
        [Route("Sign-out")]
        [ActionName("SignOut")]
        public async Task<IActionResult> SignOutPost()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        #endregion
    }
}
