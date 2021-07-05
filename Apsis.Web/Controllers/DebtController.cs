using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
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
        private readonly IUnitofWork _unitofWork;

        public DebtController(IUnitofWork unitofWork,IBillService billService, ISubscriptionService subscriptionService)
        {
            _billService = billService;
            _subscriptionService = subscriptionService;
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> DebtList()
        {
            List<Bill> bills = await _unitofWork.Bill.Get(x => x.Status == false);
            List<Subscription> subscription = await _unitofWork.Subscription.Get(x => x.Status == true);
            DebtTotalModel model = new DebtTotalModel();
            model.Bill = bills;
            model.Subscription = subscription;
            model.BillTotal=bills.Sum(item => item.Amount);
            model.SubscriptionTotal=subscription.Sum(item => item.Amount);
            return View(model);
        }
        public async Task<IActionResult> PaidDebtList()
        {
            List<Bill> bills = await _unitofWork.Bill.Get(x => x.Status == true);
            List<Subscription> subscription = await _unitofWork.Subscription.Get(x => x.Status == false);
            ViewBag.Bills = new List<Bill>(bills);
            ViewBag.Subscription = new List<Subscription>(subscription);
            DebtTotalModel model = new DebtTotalModel();
            model.BillTotal = bills.Sum(item => item.Amount);
            model.SubscriptionTotal = subscription.Sum(item => item.Amount);
            return View(model);
        }
    }
}
