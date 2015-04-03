
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sample.Domain.CQRS.Specifications;

namespace Sample.Domain.Persistence
{
    public interface IRepository : IDisposable
    {

    }

    public interface IRepository<TEntity> : IRepository, IUnitOfWork where TEntity : class
    {
        void Add(TEntity entity);
        int Count();
        void Delete(TEntity entity);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Store(TEntity entity);
    }
}
