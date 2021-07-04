using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
using Apsis.Web.Models;
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
    public class FlatController : Controller
    {
        private readonly IBlockService _blockService;
        private readonly IFlatService _flatService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitofWork _unitofWork;

        public FlatController(IUnitofWork unitofWork,IBlockService blockService, IFlatService flatService, UserManager<User> userManager)
        {
            _blockService = blockService;
            _flatService = flatService;
            _userManager = userManager;
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> AddBlock()
        {
            List<Block> blocks = await _unitofWork.Block.GetAll();
            ViewBag.Blocks = new List<Block>(blocks);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlock(Block model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.Block.Add(model);
                await _unitofWork.SaveChangesAsync();
                return RedirectToAction("AddBlock");
            }
            return RedirectToAction("AddBlock");

        }

        public async Task<IActionResult> AddFlat()
        {
            var users = _userManager.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            List<Flat> flats = await _unitofWork.Flat.GetAll();
            List<Block> block = await _unitofWork.Block.GetAll();
            ViewBag.Flats = new List<Flat>(flats);
            ViewBag.UserList = new List<User>(users);
            ViewBag.BlockList = new SelectList(block, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFlat(Flat model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.Flat.Add(model);
                await _unitofWork.SaveChangesAsync();
                return RedirectToAction("AddFlat");
            }
            return RedirectToAction("AddFlat");
        }

        public async Task<IActionResult> Delete(string flatId)
        {
            List<Flat> flatList = await _unitofWork.Flat.Get(x => x.Id == Convert.ToInt32(flatId));
            Flat flat = flatList.FirstOrDefault();
            if (flat != null)
            {
                List<Bill> bills =  await _unitofWork.Bill.Get(x => x.FlatId == flat.Id);
                foreach(Bill item in bills)
                {
                    _unitofWork.Bill.Delete(item);
                }
                List<Subscription> subscriptions = await _unitofWork.Subscription.Get(x => x.FlatId == flat.Id);
                foreach (Subscription item in subscriptions)
                {
                    _unitofWork.Subscription.Delete(item);
                }

                _unitofWork.Flat.Delete(flat);
                await _unitofWork.SaveChangesAsync();
            }              
            return RedirectToAction("AddFlat");
        }

        public async Task<IActionResult> Update(string flatId)
        {
            var users = _userManager.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            List<Flat> flat = await _unitofWork.Flat.Get(x => x.Id == Convert.ToInt32(flatId));
            return View(flat.FirstOrDefault());
        }
        [HttpPost]
        public async Task<IActionResult> Update(Flat model)
        {
            List<Flat> flatList = await _unitofWork.Flat.Get(x => x.Id == model.Id);
            Flat flat = flatList.FirstOrDefault();
            flat.UserId = model.UserId;
            flat.Status = model.Status;
            flat.OwnerUser = model.OwnerUser;
             _unitofWork.Flat.Update(flat);
            await _unitofWork.SaveChangesAsync();
            return RedirectToAction("AddFlat");
        }

    }
}
