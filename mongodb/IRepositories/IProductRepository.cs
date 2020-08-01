using mongodb.IRepositories.Base;
using mongodb.Models;
using mongodb.Models.Base;

namespace mongodb.IRepositories
{
    public interface IProductRepository : IBaseMongodbRepository<ProductModel>
    {
        
    }
}
