using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.Project.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                // make sure user name and email not present
                var user = await _userManager.FindByNameAsync(model.Username); 
                if (user is null) { 
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null) {
                        user = new AppUser()
                        {
                            UserName = model.Username,
                            FirstName = model.FisrtName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,

                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid SignUp");

            }
            return View(model);
        }
        


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        //Sign In
                        // token persistent or not depending on the RememberMe option 
                        var result= await _signInManager.PasswordSignInAsync(user,model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }

                }

                ModelState.AddModelError("", "Invalid Login");


            }

            return View(model);
        }


        [HttpGet]
        // new -> to override the SignOut funtion in the parent 
        public new async Task< IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));


        }



    }
}
