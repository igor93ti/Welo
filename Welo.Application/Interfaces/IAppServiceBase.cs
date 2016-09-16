using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Welo.Domain.Entities.Base;

namespace Welo.Application.Interfaces
{
    public interface IAppServiceBase<TEntity, in TIdentifier>
        where TEntity : IEntity<TIdentifier>
        where TIdentifier : struct
    {
        bool Exists(TIdentifier id);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);

        void Remove(TIdentifier id);

        TEntity Get(TIdentifier id);

        IEnumerable<TEntity> GetAll();

        long Count();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query);
    }
}