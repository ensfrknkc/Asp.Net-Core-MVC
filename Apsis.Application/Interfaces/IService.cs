using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Interfaces
{
    public interface IService<TEntity> where TEntity: class
    {
        Task Add(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entityies);
        void Update(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter);
    }
}
