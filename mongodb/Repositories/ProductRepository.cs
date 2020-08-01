using Microsoft.Extensions.Options;
using mongodb.ConfigModels;
using mongodb.IRepositories;
using mongodb.Models;
using mongodb.Models.Base;
using mongodb.Repositories.Base;
using System.Security.Cryptography.X509Certificates;

namespace mongodb.Repositories
{
    public class ProductRepository : BaseMasterDBMongodbRepository<ProductModel>, IProductRepository
    {
        public ProductRepository(IOptions<ConnectionStringConfig> config) : base(config.Value.MasterDB)
        {
            
        }

    }
}
