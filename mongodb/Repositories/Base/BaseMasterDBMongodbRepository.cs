using mongodb.Modelss.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongodb.Repositories.Base
{
    public class BaseMasterDBMongodbRepository<T> : BaseMongodbRepository<T> where T : BaseMongoDBModel
    {
        // MongoDB 連線字串
        private static string connectionString = "mongodb+srv://yuhaoliao:6quQXA0h9lGu4lP0@cluster0.anjcj.gcp.mongodb.net/<dbname>?retryWrites=true&w=majority";
                                                                                                                                                                //string connectionString = 
        public BaseMasterDBMongodbRepository() : base(connectionString, "Master")
        {

        }
    }
}
