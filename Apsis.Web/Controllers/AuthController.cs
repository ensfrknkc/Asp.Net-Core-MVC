using Apsis.Domain.Models;
using Apsis.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (userName != null && password != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
                if (!result.Succeeded) return RedirectToAction("Login");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }

        public IActionResult AddUser()
        {
            var users = _userManager.Users.ToList();
            ViewBag.AllUsers = new List<User>(users);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _userManager.CreateAsync(new User
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Surname = model.Surname,
                    IdentificationNumber = model.IdentificationNumber,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                }, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("AddUser");
                }
                else return RedirectToAction("AddUser");
            }
            else return RedirectToAction("AddUser");
        }
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
