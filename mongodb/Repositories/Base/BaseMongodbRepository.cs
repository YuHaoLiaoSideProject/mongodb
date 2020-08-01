using mongodb.Modelss.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace mongodb.Repositories.Base
{

    public class BaseMongodbRepository<T> where T : BaseMongoDBModel
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<T> _mongoCollection;
        public static string _tableName = typeof(T).Name.Replace("Model", string.Empty);
        public BaseMongodbRepository(string connectionString, string database)
        {
            // 產生 MongoClient 物件
            _mongoClient = new MongoClient(connectionString);
            // 取得 MongoDatabase 物件
            _mongoDatabase = _mongoClient.GetDatabase(database);
            // 取得 Collection
            _mongoCollection = _mongoDatabase.GetCollection<T>(_tableName);
        }

        public List<T> GetAll()
        {
            var products = _mongoCollection.AsQueryable().ToList();

            return products;
        }
        public void Insert(T model)
        {
            _mongoCollection.InsertOne(model);
        }
        public ReplaceOneResult Update(T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", entity.Id);

            return _mongoCollection.ReplaceOne(filter, entity); ;
        }
        public DeleteResult DeleteById(string id)
        {
            return DeleteById(ObjectId.Parse(id)); ;
        }
        public DeleteResult DeleteById(ObjectId id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return _mongoCollection.DeleteOne(filter);
        }
        public T GetById(ObjectId id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return _mongoCollection.Find(filter).FirstOrDefault();
        }
        public T GetById(string id)
        {
            return GetById(ObjectId.Parse(id));
        }
    }
}
