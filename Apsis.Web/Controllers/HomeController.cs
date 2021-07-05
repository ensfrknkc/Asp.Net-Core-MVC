using Apsis.Domain.Models;
using Apsis.Infrastructure;
using Apsis.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitofWork _unitofWork;

        public HomeController(IUnitofWork unitofWork,ILogger<HomeController> logger , UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            Flat flat = await _unitofWork.Flat.GetById(x => x.UserId == userId);
            List<Bill> bills = await _unitofWork.Bill.Get(x => x.FlatId == flat.Id && x.Status==false);
            List<Subscription> subscriptions = await _unitofWork.Subscription.Get(x => x.FlatId == flat.Id && x.Status == false);
            UserInvoice model = new UserInvoice();
            model.Subscriptions = subscriptions;
            model.Bills = bills;
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    }
}
