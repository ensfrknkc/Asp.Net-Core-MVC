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
    public class BlockService : IBlockService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public BlockService(IUnitofWork unitofWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task Add(BlockViewDto entity)
        {
            await _unitofWork.Block.Add(_mapper.Map<Block>(entity));
            await _unitofWork.SaveChangesAsync();
        }

        public void Delete(BlockViewDto entity)
        {
            _unitofWork.Block.Delete(_mapper.Map<Block>(entity));
        }

        public void DeleteRange(List<BlockViewDto> entityies)
        {
            _unitofWork.Block.DeleteRange(_mapper.Map<List<Block>>(entityies));
        }

        public async Task<List<BlockViewDto>> Get(Expression<Func<BlockViewDto, bool>> filter)
        {
            var dtoFilter = _mapper.Map<Expression<Func<Block, bool>>>(filter);
            var result = await _unitofWork.Block.Get(dtoFilter);
            return _mapper.Map<List<BlockViewDto>>(result);
        }

        public async Task<List<BlockViewDto>> GetAll()
        {
            var result = await _unitofWork.Block.GetAll();
            return _mapper.Map<List<BlockViewDto>>(result);
        }

        public void Update(BlockViewDto entity)
        {
            _unitofWork.Block.Update(_mapper.Map<Block>(entity));
            _unitofWork.SaveChangesAsync();
        }
    }
}
