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
        private readonly IUnitofWork  _unitofWork;

        public MessageController(IMessageService messageService, UserManager<User> userManager, IUnitofWork unitofWork)
        {
            _messageService = messageService;
            _userManager = userManager;
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> Messages()
        {
            List<Message> messages = await _unitofWork.Message.GetAll();

            return View(messages);
        }
        public async Task<IActionResult> Delete(string messageId)
        {
            Message message = await _unitofWork.Message.GetById(x => x.Id == Convert.ToInt32(messageId));
            if (message != null)
            {
                _unitofWork.Message.Delete(message);
                await _unitofWork.SaveChangesAsync();
            }

            return RedirectToAction("Messages");
        }
    }
}
