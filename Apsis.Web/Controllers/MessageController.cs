using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsis.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitofWork  _uniofWork;

        public MessageController(IMessageService messageService, UserManager<User> userManager, IUnitofWork uniofWork)
        {
            _messageService = messageService;
            _userManager = userManager;
            _uniofWork = uniofWork;
        }

        public async Task<IActionResult> Messages()
        {
            List<Message> messages = await _uniofWork.Message.GetAll();

            return View(messages);
        }
    }
}
