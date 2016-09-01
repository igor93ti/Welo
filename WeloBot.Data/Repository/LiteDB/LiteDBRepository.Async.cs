using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeloBot.Data.Repository.Async;
using WeloBot.Domain.Entities.Base;

namespace WeloBot.Data.Repository.LiteDB
{
    public partial class LiteDBRepository<TEntity, TIdentifier> where TEntity : IEntity<TIdentifier>,
        new() where TIdentifier : struct
    {
        public Task<bool> ExistsAsync(TIdentifier id) => AsyncAll.GetAsyncResult(() => Exists(id));

        public Task<TEntity> AddAsync(TEntity entity) => AsyncAll.GetAsyncResult(() => Add(entity));

        public Task<long> CountAsync() => AsyncAll.GetAsyncResult(() => Count());

        public Task<IEnumerable<TEntity>> GetAllAsync() => AsyncAll.GetAsyncResult(() => GetAll());

        public Task<TEntity> GetAsync(TIdentifier id) => AsyncAll.GetAsyncResult(() => Get(id));

        public Task RemoveAsync(TIdentifier id) => AsyncAll.ExecuteAsync(() => Remove(id));

        public Task RemoveAsync(TEntity entity) => AsyncAll.ExecuteAsync(() => Remove(entity));

        public Task<TEntity> UpdateAsync(TEntity entity) => AsyncAll.GetAsyncResult(() => Update(entity));

        public Task FindAsync(Expression<Func<TEntity, bool>> query) => AsyncAll.ExecuteAsync(() => Find(query));
    }
}