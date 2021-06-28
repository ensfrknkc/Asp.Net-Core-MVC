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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public SubscriptionService(IUnitofWork unitofWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
        }
        public async Task Add(SubscriptionViewDto entity)
        {
            await _unitofWork.Subscription.Add(_mapper.Map<Subscription>(entity));
            await _unitofWork.SaveChangesAsync();
        }

        public void Delete(SubscriptionViewDto entity)
        {
            _unitofWork.Subscription.Delete(_mapper.Map<Subscription>(entity));
        }

        public async Task<List<SubscriptionViewDto>> Get(Expression<Func<SubscriptionViewDto, bool>> filter)
        {
            var dtoFilter = _mapper.Map<Expression<Func<Subscription, bool>>>(filter);
            var result = await _unitofWork.Subscription.Get(dtoFilter);
            return _mapper.Map<List<SubscriptionViewDto>>(result);
        }

        public async Task<List<SubscriptionViewDto>> GetAll()
        {
            var result = await _unitofWork.Subscription.GetAll();
            return _mapper.Map<List<SubscriptionViewDto>>(result);
        }

        public void Update(SubscriptionViewDto entity)
        {
            _unitofWork.Subscription.Update(_mapper.Map<Subscription>(entity));
            _unitofWork.SaveChangesAsync();
        }
    }
}
