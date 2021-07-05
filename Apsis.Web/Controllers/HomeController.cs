using Apsis.Application.Dto;
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
using Apsis.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Apsis.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitofWork _unitofWork;
        private readonly ICreditCardService _creditCardService;

        public HomeController(ICreditCardService creditCardService,IUnitofWork unitofWork,ILogger<HomeController> logger , UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitofWork = unitofWork;
            _creditCardService = creditCardService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            Flat flat = await _unitofWork.Flat.GetById(x => x.UserId == userId);
            UserInvoice model = new UserInvoice();
            if (flat != null)
            {
                List<Bill> bills = await _unitofWork.Bill.Get(x => x.FlatId == flat.Id && x.Status == false);
                List<Subscription> subscriptions = await _unitofWork.Subscription.Get(x => x.FlatId == flat.Id && x.Status == false);
                
                model.Subscriptions = subscriptions;
                model.Bills = bills;
            }
            
            return View(model);
        }
        public async Task<IActionResult> BillPayment(string billId)
        {
            Bill bill = await _unitofWork.Bill.GetById(x => x.Id == Convert.ToInt32(billId));
            CreditCardDto creditCardDto = new CreditCardDto();
            creditCardDto.Id = Convert.ToInt32(billId);
            creditCardDto.Money = Convert.ToInt32(bill.Amount);
            return View(creditCardDto);
        }
        [HttpPost]
        public async Task<IActionResult> BillPayment(CreditCardDto model)
        {
            bool result = await _creditCardService.WithdrawMoney(model);
            if (result)
            {
                Bill bill = await _unitofWork.Bill.GetById(x => x.Id == model.Id);
                bill.Status = true;
                 _unitofWork.Bill.Update(bill);
                await _unitofWork.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SubscriptionPayment(string subscriptionId)
        {
            Subscription subscription = await _unitofWork.Subscription.GetById(x => x.Id == Convert.ToInt32(subscriptionId));
            CreditCardDto creditCardDto = new CreditCardDto();
            creditCardDto.Id = Convert.ToInt32(subscriptionId);
            creditCardDto.Money = Convert.ToInt32(subscription.Amount);
            return View(creditCardDto);
        }
        [HttpPost]
        public async Task<IActionResult> SubscriptionPayment(CreditCardDto model)
        {
            bool result = await _creditCardService.WithdrawMoney(model);
            if (result)
            {
                Subscription subscription = await _unitofWork.Subscription.GetById(x => x.Id == model.Id);
                subscription.Status = true;
                _unitofWork.Subscription.Update(subscription);
                await _unitofWork.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SendMessage()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            Message message = new Message();
            message.UserId = userId;
            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            message.CreateAt = DateTime.Now;
            await _unitofWork.Message.Add(message);
            await _unitofWork.SaveChangesAsync();
            return RedirectToAction("SendMessage");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    }
}
