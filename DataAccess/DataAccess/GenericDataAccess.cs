using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.DataAccess
{
    public interface IGenericDataAccess<T> where T : class, IEntity
    {
        IQueryable<T> GetQueryable();
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereCondition);
        T? Get(Expression<Func<T, bool>> whereCondition);
        List<T> GetAll(Expression<Func<T, bool>> whereCondition);
        void Update(T entity);
        void Add(T entity);
    }

    public class GenericDataAccess<T> : IGenericDataAccess<T> where T : class, IEntity
    {
        private readonly Context _context;
        private readonly DbSet<T> _table;

        public GenericDataAccess(Context context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetQueryable()
        {
            return _table.AsNoTracking().AsQueryable();
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereCondition)
        {
            return _table.AsNoTracking().AsQueryable().Where(whereCondition);
        }

        public T? Get(Expression<Func<T, bool>> whereCondition)
        {
            return GetQueryable(whereCondition).FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return GetQueryable(whereCondition).ToList();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }
    }
}
