using Apsis.Domain.Interfaces;
using Apsis.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        ApsisDbContext _context;
        private DbSet<TEntity> _entitiy;

        public Repository(ApsisDbContext context)
        {
            _context = context;
            _entitiy = _context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _entitiy.AddAsync(entity);   
        }

        public void Delete(TEntity entity)
        {
            _entitiy.Remove(entity);
        }

        public async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter)
        {
            return await _entitiy.Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _entitiy.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _entitiy.Update(entity);
        }
    }
}
