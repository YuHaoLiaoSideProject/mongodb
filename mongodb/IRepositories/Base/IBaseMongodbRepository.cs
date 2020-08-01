using mongodb.Models.Base;
using mongodb.Modelss.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace mongodb.IRepositories.Base
{
    public interface IBaseMongodbRepository<T> where T: BaseMongoDBModel,new()
    {
        PagingResult<T> GetPagingResult(PaginationModel pagination);
        List<T> GetAll();
        T GetById(ObjectId id);
        T GetById(string id);
        void Insert(T model);
        ReplaceOneResult Update(T entity);
        DeleteResult DeleteById(string id);
        DeleteResult DeleteById(ObjectId id);

    }
}
