using DataAccess.DataAccess;
using DataAccess.Models;
using System.Linq.Expressions;

namespace Services
{
    public interface IGenericService<T> : IGenericDataAccess<T> where T : class, IEntity
    {
    }

    public class GenericService<T> : IGenericService<T> where T : class, IEntity
    {
        public readonly IGenericDataAccess<T> _repository;

        public GenericService(IGenericDataAccess<T> repository)
        {
            _repository = repository;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        public T? Get(Expression<Func<T, bool>> whereCondition)
        {
            return _repository.Get(whereCondition);
        }

        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return _repository.GetAll(whereCondition);
        }

        public IQueryable<T> GetQueryable()
        {
            return _repository.GetQueryable();
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereCondition)
        {
            return _repository.GetQueryable(whereCondition);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }
    }
}