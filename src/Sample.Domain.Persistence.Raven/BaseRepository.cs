using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Domain.CQRS.Specifications;
using Sample.Domain.Persistence.Raven.Convertors;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace Sample.Domain.Persistence.Raven
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private bool _disposed = false;
        private IRavenSessionFactory _factory;
        private IDocumentSession _session;

        protected BaseRepository(IRavenSessionFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory", "Session Factory can not be null");
            _factory = factory;
            CreateSession();
        }

        protected void CreateSession()
        {
            _session = _factory.CreateSession();
            _session.Advanced.DocumentStore.Conventions.CustomizeJsonSerializer = s => s.Converters.Add(new ClaimJsonConverter());
        }

        public TEntity Get(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Document Id can not be null");
            return _session.Load<TEntity>(id);
        }

        public TDto Get<TTransform, TDto>(string id)
            where TTransform : AbstractTransformerCreationTask<TEntity>, new()
            where TDto : class
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Document Id can not be null");
            if (_disposed) throw new Exception("Object is disposed");
            if (_session == null) throw new Exception("Datastore Session does not exist");

            return _session.Load<TTransform, TDto>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _session.Query<TEntity>().ToList();
        }

        public int Count()
        {
            return _session.Query<TEntity>().Count();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _session.Query<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            return _session.Query<TEntity>().Where(specification.ToExpression);
        }

        public IEnumerable<TDto> FindAll<TIndex, TDto>(ISpecification<TEntity> specification)
            where TIndex : AbstractIndexCreationTask, new()
            where TDto : class
        {
            return _session.Query<TEntity, TIndex>()
                            .Where(specification.ToExpression)
                            .ProjectFromIndexFieldsInto<TDto>();
        }

        public IEnumerable<TDto> FindAll<TDto>(ISpecification<TEntity> specification)
            where TDto : class
        {
            return _session.Query<TEntity>()
                            .Where(specification.ToExpression)
                            .ProjectFromIndexFieldsInto<TDto>();
        }

        public void Add(TEntity entity)
        {
            Store(entity);
            Save();
        }

        public void Update(TEntity entity)
        {
            Add(entity);
        }

        public void Store(TEntity entity)
        {
            _session.Store(entity);
            Save();
        }

        public void Save()
        {
            _session.SaveChanges();
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _factory = null;
                    _session.SaveChanges();
                    _session.Dispose();
                    _session = null;
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
