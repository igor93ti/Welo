using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Welo.Application.Interfaces;
using Welo.Domain.Entities.Base;
using Welo.Domain.Interfaces.Services;

namespace Welo.Application
{
    [Serializable]
    public class AppServiceBase<TEntity, TIdentifier> : IAppServiceBase<TEntity, TIdentifier> where TEntity : IEntity<TIdentifier>, new() where TIdentifier : struct
    {
        internal IService<TEntity, TIdentifier> Service;

        public bool Exists(TIdentifier id) => Service.Exists(id);

        public TEntity Add(TEntity entity) => Service.Add(entity);

        public TEntity Update(TEntity entity) => Service.Update(entity);

        public void Remove(TEntity entity) => Service.Remove(entity);

        public void Remove(TIdentifier id) => Service.Remove(id);

        public TEntity Get(TIdentifier id) => Service.Get(id);

        public IEnumerable<TEntity> GetAll() => Service.GetAll();

        public long Count() => Service.Count();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query) => Service.Find(query);
    }
}