using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteDB;
using Welo.Domain.Entities.Base;
using Welo.Domain.Interfaces.Repositories.Base;

namespace Welo.Data.Repository.LiteDB
{
    public partial class LiteDBRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>, IRepositoryAsync<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>,
        new() where TIdentifier : struct
    {
        protected ILiteDBContext DbContext { get; set; }
        private LiteCollection<TEntity> _collection;

        private LiteCollection<TEntity> Collection
        {
            get
            {
                if (_collection == null)
                {
                    var collectionName = typeof(TEntity).Name;
                    _collection = DbContext.Database.GetCollection<TEntity>(collectionName);
                }
                return _collection;
            }
        }

        private string keyName = "_id";

        protected LiteDBRepository()
        {
        }

        public bool Exists(TIdentifier id)
        {
            return Collection.Exists(Query.EQ(keyName, new BsonValue(id)));
        }

        public TEntity Add(TEntity entity)
        {
            Collection.Insert(entity);

            return entity;
        }

        public Int64 Add(IEnumerable<TEntity> entities) => Collection.Insert(entities);

        public Int64 AddBulk(IEnumerable<TEntity> entity, int chunkSize = 32768) => Collection.Insert(entity);

        public long Count() => Collection.Count();

        public TEntity Get(TIdentifier id) => Collection.FindById(new BsonValue(id));

        public IEnumerable<TEntity> GetAll() => Collection.FindAll();

        public void Remove(TEntity entity) => Remove(entity.Id);

        public void Remove(TIdentifier id) => Collection.Delete(new BsonValue(id));

        public TEntity Update(TEntity entity)
        {
            Collection.Update(entity);

            return entity;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query) => Collection.Find(query);
    }
}