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
    public class BillController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public BillController( IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> AddBill()
        {
            List<Flat> flats = await _unitofWork.Flat.GetAll();
            List<Bill> bills = await _unitofWork.Bill.GetAll();

            ViewBag.Flats = new SelectList(flats, "Id", "User.Name");
            ViewBag.Bills = new List<Bill>(bills);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBill(Bill model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.Bill.Add(model);
                await _unitofWork.SaveChangesAsync();
                return RedirectToAction("AddBill");
            }
            return RedirectToAction("AddBill");
        }

        public async Task<IActionResult> Delete(string billId)
        {
            Bill bill = await _unitofWork.Bill.GetById(x => x.Id == Convert.ToInt32(billId));
            if (bill != null)
            {
                _unitofWork.Bill.Delete(bill);
                await _unitofWork.SaveChangesAsync();
            }

            return RedirectToAction("AddBill");
        }

        public async Task<IActionResult> Update(string billId)
        {
            List<Flat> flats = await _unitofWork.Flat.GetAll();
            ViewBag.Flats = new SelectList(flats, "Id", "User.Name");

            Bill bill = await _unitofWork.Bill.GetById(x => x.Id == Convert.ToInt32(billId));
            return View(bill);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Bill model)
        {
            Bill bill = await _unitofWork.Bill.GetById(x => x.Id == model.Id);
            bill.FlatId = model.FlatId;
            bill.Amount = model.Amount;
            bill.BillType = model.BillType;
            bill.Month = model.Month;
            bill.Year = model.Year;           
            _unitofWork.Bill.Update(bill);
            await _unitofWork.SaveChangesAsync();
            return RedirectToAction("AddBill");
        }
    }
}
