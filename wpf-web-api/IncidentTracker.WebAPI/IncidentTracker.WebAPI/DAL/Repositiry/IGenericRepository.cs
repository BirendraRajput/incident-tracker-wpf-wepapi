using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTracker.DAL.Repositiry
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> Find(int ID);
        Task<IEnumerable<TEntity>> FindAll();
       IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        Task<bool> Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
