using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
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

        public SubscriptionController(ISubscriptionService subscriptionService, IFlatService flatService)
        {
            _subscriptionService = subscriptionService;
            _flatService = flatService;
        }

        public async Task<IActionResult> SubscriptionAdd()
        {
            List<FlatViewDto> flats = await _flatService.GetAll();
            ViewBag.Flats = new SelectList(flats, "Id", "Id");
            List<SubscriptionViewDto> subscription = await _subscriptionService.GetAll();
            ViewBag.Subscriptions = new List<SubscriptionViewDto>(subscription);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubscriptionAdd(SubscriptionViewDto model)
        {
            if (ModelState.IsValid)
            {
                await _subscriptionService.Add(model);
                return RedirectToAction("SubscriptionAdd");
            }
            return RedirectToAction("SubscriptionAdd");
        }
    }
}
