using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ChetTime.Models;
using ChetTime.ViewModels;


namespace ChetTime.Controllers
{
    public class UserController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
               //  return RedirectToAction("ChatView", "Chat");
                    return RedirectToAction("Index", "Home");
                
            }
            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                    // return RedirectToAction("ChatView", "Chat");
                }
            }
            ModelState.AddModelError("", "Faild to Login");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                   
                    UserName = registerModel.UserName,
                    Password=registerModel.Password
                    
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.UserName, registerModel.Password, false, false);
                    if (resultSignIn.Succeeded)
                    {
                        return RedirectToAction("Login", "User");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult AddOrEdit(int id=0)
        {
            User userModel = new User();
            return View(userModel);
        }
        

        
    }
}