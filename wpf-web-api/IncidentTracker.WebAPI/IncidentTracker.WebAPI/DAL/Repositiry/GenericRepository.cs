using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IncidentTracker.DAL.Repositiry
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private IncidentContext _context;

        public GenericRepository(IncidentContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
            _context.SaveChanges();
            return true;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
        

        public async Task<TEntity> Find(int ID)
        {
            return await _context.Set<TEntity>().FindAsync(ID);
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
    }
}
