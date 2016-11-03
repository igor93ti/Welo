using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Welo.Data.Repository.Async;
using Welo.Domain.Entities.Base;

namespace Welo.Data.Repository.LiteDB
{
    public partial class LiteDBRepository<TEntity, TIdentifier> where TEntity : IEntity<TIdentifier>,
        new() where TIdentifier : struct
    {
        public virtual Task<bool> ExistsAsync(TIdentifier id) => AsyncAll.GetAsyncResult(() => Exists(id));

        public virtual Task<TEntity> AddAsync(TEntity entity) => AsyncAll.GetAsyncResult(() => Add(entity));

        public virtual Task<long> CountAsync() => AsyncAll.GetAsyncResult(() => Count());

        public virtual Task<IEnumerable<TEntity>> GetAllAsync() => AsyncAll.GetAsyncResult(() => GetAll());

        public virtual Task<TEntity> GetAsync(TIdentifier id) => AsyncAll.GetAsyncResult(() => Get(id));

        public virtual Task RemoveAsync(TIdentifier id) => AsyncAll.ExecuteAsync(() => Remove(id));

        public virtual Task RemoveAsync(TEntity entity) => AsyncAll.ExecuteAsync(() => Remove(entity));

        public virtual Task<TEntity> UpdateAsync(TEntity entity) => AsyncAll.GetAsyncResult(() => Update(entity));

        public virtual Task FindAsync(Expression<Func<TEntity, bool>> query) => AsyncAll.ExecuteAsync(() => Find(query));
    }
}