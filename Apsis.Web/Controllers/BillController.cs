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
    public class BillController : Controller
    {
        private readonly IFlatService _flatService;
        private readonly IBillService _billService;

        public BillController(IFlatService flatService, IBillService billService)
        {
            _flatService = flatService;
            _billService = billService;
        }

        public async Task<IActionResult> AddBill()
        {
            List<FlatViewDto> flats = await _flatService.GetAll();
            ViewBag.Flats = new SelectList(flats, "Id", "FlatNo");
            List<BillViewDto> bills = await _billService.GetAll();
            ViewBag.Bills = new List<BillViewDto>(bills);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBill(BillViewDto model)
        {
            if (ModelState.IsValid)
            {
                await _billService.Add(model);
                return RedirectToAction("AddBill");
            }
            return RedirectToAction("AddBill");
        }
    }
}
