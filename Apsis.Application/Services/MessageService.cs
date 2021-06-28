using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task Add(MessageViewDto entity)
        {
            await _unitofWork.Message.Add(_mapper.Map<Message>(entity));
            await _unitofWork.SaveChangesAsync();
        }

        public void Delete(MessageViewDto entity)
        {
            _unitofWork.Message.Delete(_mapper.Map<Message>(entity));

        }

        public async Task<List<MessageViewDto>> Get(Expression<Func<MessageViewDto, bool>> filter)
        {
            var dtoFilter = _mapper.Map<Expression<Func<Message, bool>>>(filter);
            var result = await _unitofWork.Message.Get(dtoFilter);
            return _mapper.Map<List<MessageViewDto>>(result);
        }

        public async Task<List<MessageViewDto>> GetAll()
        {
            var result = await _unitofWork.Message.GetAll();
            return _mapper.Map<List<MessageViewDto>>(result);
        }

        public void Update(MessageViewDto entity)
        {
            _unitofWork.Message.Update(_mapper.Map<Message>(entity));
        }
    }
}
