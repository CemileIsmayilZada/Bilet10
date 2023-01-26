using App.Core.Entities;
using App.DataAccess.Contexts;
using AppBusiness.ViewModels.Auth;
using AppBusiness.ViewModels.TeamMemberrs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContex _context;
        private readonly IValidator<RegisterTeamViewModel> _validatorRegister;
        private readonly IValidator<LoginTeamViewModel> _validatorLogin;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;

        public AuthController(AppDbContex context
            , IValidator<RegisterTeamViewModel> validatorRegister
            , IValidator<LoginTeamViewModel> validatorLogin
            , UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager)
        {
            _context = context;
            _validatorRegister = validatorRegister;
            _validatorLogin = validatorLogin;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterTeamViewModel registerTeam)
        {
            if (!ModelState.IsValid) return View(registerTeam);
            ValidationResult result = await _validatorRegister.ValidateAsync(registerTeam);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    return View();
                }
            }

            AppUser user = new()
            {
                Fullname = registerTeam.Fullname,
                UserName = registerTeam.Username,
                Email = registerTeam.Email,
            };

             var identityResult= await _userManager.CreateAsync(user, registerTeam.Password);
            if(!identityResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed during register");
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginTeamViewModel login)
        {

            ValidationResult result = await _validatorLogin.ValidateAsync(login);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(login);

            var user =await _userManager.FindByEmailAsync(login.UsernameOrEmail);
            if(user is null)
            {
                user= await _userManager.FindByNameAsync(login.UsernameOrEmail);
                if(user is null)
                {
                    ModelState.AddModelError("", "Username or Password or Email is invalid");
                    return View();
                }

                var signInResult= await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
                if(!signInResult.Succeeded)
                {

                    ModelState.AddModelError("", "Username or Password or Email is invalid");
                    return View();
                }
                if(signInResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "More Attempt to login");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home"); ;
        }
    }
}
