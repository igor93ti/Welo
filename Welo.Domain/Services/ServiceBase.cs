using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Welo.Domain.Entities.Base;
using Welo.Domain.Interfaces.Repositories.Base;
using Welo.Domain.Interfaces.Services;

namespace Welo.Domain.Services
{
    [Serializable]
    public class ServiceBaseTEntity<TEntity, TIdentifier> : IService<TEntity, TIdentifier> where TEntity : IEntity<TIdentifier>, new() where TIdentifier : struct
    {
        private IRepository<TEntity, TIdentifier> _repository;

        public ServiceBaseTEntity(IRepository<TEntity, TIdentifier> repository)
        {
            _repository = repository;
        }

        public bool Exists(TIdentifier id) => _repository.Exists(id);

        public TEntity Add(TEntity entity) => _repository.Add(entity);

        public TEntity Update(TEntity entity) => _repository.Update(entity);

        public void Remove(TEntity entity) => _repository.Remove(entity);

        public void Remove(TIdentifier id) => _repository.Remove(id);

        public TEntity Get(TIdentifier id) => _repository.Get(id);

        public IEnumerable<TEntity> GetAll() => _repository.GetAll();

        public long Count() => _repository.Count();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query) => _repository.Find(query);
    }
}