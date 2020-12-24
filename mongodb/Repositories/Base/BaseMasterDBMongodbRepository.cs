using mongodb.Models.Base;

namespace mongodb.Repositories.Base
{
    public class BaseMasterDBMongodbRepository<T> : BaseMongodbRepository<T> where T : BaseMongoDBModel,new()
    {
                                                                                                                                                                //string connectionString = 
        public BaseMasterDBMongodbRepository(string connectionString) : base(connectionString, "Master")
        {

        }
    }
}
