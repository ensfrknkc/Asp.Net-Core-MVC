using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    public class FlatController : Controller
    {
        private readonly IBlockService _blockService;
        private readonly IFlatService _flatService;
        private readonly UserManager<User> _userManager;

        public FlatController(IBlockService blockService, IFlatService flatService, UserManager<User> userManager)
        {
            _blockService = blockService;
            _flatService = flatService;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddBlock()
        {
            List<BlockViewDto> blocks = await _blockService.GetAll();
            ViewBag.Blocks = new List<BlockViewDto>(blocks);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlock(BlockViewDto model)
        {
            if (ModelState.IsValid)
            {
                await _blockService.Add(model);
                return RedirectToAction("AddBlock");
            }
            return RedirectToAction("AddBlock");

        }

        public async Task<IActionResult> AddFlat()
        {
            var users = _userManager.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            List<FlatViewDto> flats = await _flatService.GetAll();
            ViewBag.Flats = new List<FlatViewDto>(flats);
            List<BlockViewDto> blockList = await _blockService.GetAll();
            ViewBag.BlockList = new SelectList(blockList, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFlat(FlatViewDto model)
        {
            if (ModelState.IsValid)
            {
                await _flatService.Add(model);
                return RedirectToAction("AddFlat");
            }
            return RedirectToAction("AddFlat");
        }

    }
}
