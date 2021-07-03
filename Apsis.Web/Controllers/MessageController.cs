using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
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

        public MessageController(IMessageService messageService, UserManager<User> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Messages()
        {
            List<MessageViewDto> messageViewDtos = new List<MessageViewDto>();
            List<MessageViewDto> messages = await _messageService.GetAll();
            foreach(MessageViewDto message in messages)
            {
                User user = await _userManager.FindByIdAsync(message.UserId.ToString());
                message.UserName = user.UserName;
                messageViewDtos.Add(message);
            }
            return View(messageViewDtos);
        }
    }
}
