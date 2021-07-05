using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IFlatService _flatService;
        private readonly IUnitofWork _unitofWork;

        public SubscriptionController(IUnitofWork unitofWork,ISubscriptionService subscriptionService, IFlatService flatService)
        {
            _subscriptionService = subscriptionService;
            _flatService = flatService;
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> SubscriptionAdd()
        {
            List<Flat> flats = await _unitofWork.Flat.GetAll();
            ViewBag.Flats = new SelectList(flats, "Id", "Id");
            List<Subscription> subscriptions = await _unitofWork.Subscription.GetAll();
            ViewBag.Subscriptions = new List<Subscription>(subscriptions);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubscriptionAdd(Subscription model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.Subscription.Add(model);
                await _unitofWork.SaveChangesAsync();
                return RedirectToAction("SubscriptionAdd");
            }
            return RedirectToAction("SubscriptionAdd");
        }

        public async Task<IActionResult> Delete(string subscriptionId)
        {
            Subscription subscription = await _unitofWork.Subscription.GetById(x => x.Id == Convert.ToInt32(subscriptionId));
            if (subscription != null)
            {
                _unitofWork.Subscription.Delete(subscription);
                await _unitofWork.SaveChangesAsync();
            }

            return RedirectToAction("SubscriptionAdd");
        }

        public async Task<IActionResult> Update(string subscriptionId)
        {
            List<Subscription> flats = await _unitofWork.Subscription.GetAll();
            ViewBag.Flats = new SelectList(flats, "Id", "Id");

            Subscription subscription = await _unitofWork.Subscription.GetById(x => x.Id == Convert.ToInt32(subscriptionId));
            return View(subscription);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Subscription model)
        {
            Subscription subscription = await _unitofWork.Subscription.GetById(x => x.Id == model.Id);
            subscription.FlatId = model.FlatId;
            subscription.Amount = model.Amount;
            subscription.Month = model.Month;
            subscription.Year = model.Year;
            _unitofWork.Subscription.Update(subscription);
            await _unitofWork.SaveChangesAsync();
            return RedirectToAction("SubscriptionAdd");
        }
    }
}
