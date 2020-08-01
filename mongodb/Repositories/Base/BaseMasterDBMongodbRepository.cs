using mongodb.Modelss.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
