using Apsis.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Interfaces
{
    public interface IFlatService
    {
        Task Add(FlatViewDto entity);
        void Delete(FlatViewDto entitiy);
        void Update(FlatViewDto entity);
        Task<List<FlatViewDto>> GetAll();
        Task<List<FlatViewDto>> Get(Expression<Func<FlatViewDto, bool>> filter);

    }
}
