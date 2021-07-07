using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
using Apsis.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitofWork _unitofWork;

        public AdminController(IUnitofWork unitofWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.UserName = user.UserName;

            var userRoles = await _userManager.GetRolesAsync(user);

            return View(userRoles);
        }
        public async Task<IActionResult> Delete(string userId)
        {
            Flat flat = await _unitofWork.Flat.GetById(x => x.UserId == userId);
            if (flat != null)
            {
                List<Bill> bills = await _unitofWork.Bill.Get(x => x.FlatId == flat.Id);
                foreach (Bill item in bills)
                {
                    _unitofWork.Bill.Delete(item);
                }
                List<Subscription> subscriptions = await _unitofWork.Subscription.Get(x => x.FlatId == flat.Id);
                foreach (Subscription item in subscriptions)
                {
                    _unitofWork.Subscription.Delete(item);
                }

                _unitofWork.Flat.Delete(flat);
            }
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            await _unitofWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(User model)
        {
            var result = await _userManager.FindByIdAsync(model.Id);
            result.Name = model.Name;
            result.Surname = model.Surname;
            result.IdentificationNumber = model.IdentificationNumber;
            result.PhoneNumber = model.PhoneNumber;
            result.UserName = model.UserName;
            result.Email = model.Email;
            await _userManager.UpdateAsync(result);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string role)
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
            return RedirectToAction(nameof(DisplayRoles));
        }

        [HttpGet]
        public IActionResult DisplayRoles()
        {
            var roles = _roleManager.Roles.ToList();
            
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddUserToRole()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            ViewBag.Users = new SelectList(users, "Id", "UserName");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);

            await _userManager.AddToRoleAsync(user, userRole.RoleName);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserRole(string role, string userName)
        {

            var user = await _userManager.FindByNameAsync(userName);

            var result = await _userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { userId = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string role)
        {

            var roleToDelete = await _roleManager.FindByNameAsync(role);
            var result = await _roleManager.DeleteAsync(roleToDelete);

            return RedirectToAction(nameof(DisplayRoles));
        }
    }
}
