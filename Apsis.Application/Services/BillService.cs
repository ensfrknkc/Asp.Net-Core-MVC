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
    public class BillService : IBillService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public BillService(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task Add(BillViewDto entity)
        {
            await _unitofWork.Bill.Add(_mapper.Map<Bill>(entity));
            await _unitofWork.SaveChangesAsync();
        }

        public void Delete(BillViewDto entity)
        {
            _unitofWork.Bill.Delete(_mapper.Map<Bill>(entity));
        }

        public void DeleteRange(List<BillViewDto> entityies)
        {
            _unitofWork.Bill.DeleteRange(_mapper.Map<List<Bill>>(entityies));
        }

        public async Task<List<BillViewDto>> Get(Expression<Func<BillViewDto, bool>> filter)
        {
            var dtoFilter = _mapper.Map<Expression<Func<Bill, bool>>>(filter);
            var result = await _unitofWork.Bill.Get(dtoFilter);
            return _mapper.Map<List<BillViewDto>>(result);
        }

        public async Task<List<BillViewDto>> GetAll()
        {
            var result = await _unitofWork.Bill.GetAll();
            return _mapper.Map<List<BillViewDto>>(result);
        }

        public void Update(BillViewDto entity)
        {
            _unitofWork.Bill.Update(_mapper.Map<Bill>(entity));
            _unitofWork.SaveChangesAsync();
        }
    }
}
