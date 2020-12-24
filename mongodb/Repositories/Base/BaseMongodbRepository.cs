using mongodb.Extension;
using mongodb.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mongodb.Repositories.Base
{

    public class BaseMongodbRepository<T> where T : BaseMongoDBModel, new()
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        public IMongoCollection<T> _Collection;
        public static string _tableName = typeof(T).Name.Replace("Model", string.Empty);
        public BaseMongodbRepository(string connectionString, string database)
        {
            // 產生 MongoClient 物件
            _mongoClient = new MongoClient(connectionString);
            // 取得 MongoDatabase 物件
            _mongoDatabase = _mongoClient.GetDatabase(database);
            // 取得 Collection
            _Collection = _mongoDatabase.GetCollection<T>(_tableName);
        }

        public List<T> GetAll()
        {
            var products = _Collection.AsQueryable().ToList();

            return products;
        }

        public T GetById(ObjectId id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return _Collection.Find(filter).FirstOrDefault();
        }
        public T GetById(string id)
        {
            return GetById(ObjectId.Parse(id));
        }
        public PagingResult<T> GetPagingResultByWhere(Expression<Func<T, bool>> predicate, PaginationModel pagination)
        {
            return _Collection.AsQueryable().Where(predicate).ToPagingResult(pagination);
        }
        public PagingResult<T> GetPagingResult(PaginationModel pagination)
        {
            return _Collection.AsQueryable().ToPagingResult(pagination);
        }

        public void Insert(T model)
        {
            _Collection.InsertOne(model);
        }
        public ReplaceOneResult Update(T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", entity.Id);

            return _Collection.ReplaceOne(filter, entity); ;
        }
        public DeleteResult DeleteById(string id)
        {
            return DeleteById(ObjectId.Parse(id)); ;
        }
        public DeleteResult DeleteById(ObjectId id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return _Collection.DeleteOne(filter);
        }
    }
}
