using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    public class DebtController : Controller
    {
        private readonly IBillService _billService;
        private readonly ISubscriptionService _subscriptionService;

        public DebtController(IBillService billService, ISubscriptionService subscriptionService)
        {
            _billService = billService;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> DebtList()
        {
            List<BillViewDto> bills = await _billService.Get(x => x.Status == true);
            List<SubscriptionViewDto> subscription = await _subscriptionService.Get(x => x.Status == true);
            DebtTotalModel model = new DebtTotalModel();
            model.Bill = bills;
            model.Subscription = subscription;
            model.BillTotal=bills.Sum(item => item.Amount);
            model.SubscriptionTotal=subscription.Sum(item => item.Amount);
            return View(model);
        }
        public async Task<IActionResult> PaidDebtList()
        {
            List<BillViewDto> bills = await _billService.Get(x => x.Status == false);
            List<SubscriptionViewDto> subscription = await _subscriptionService.Get(x => x.Status == false);
            ViewBag.Bills = new List<BillViewDto>(bills);
            ViewBag.Subscription = new List<SubscriptionViewDto>(subscription);
            DebtTotalModel model = new DebtTotalModel();
            model.BillTotal = bills.Sum(item => item.Amount);
            model.SubscriptionTotal = subscription.Sum(item => item.Amount);
            return View(model);
        }
    }
}
