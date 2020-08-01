using mongodb.Models;
using mongodb.Models.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongodb.Interfaces
{
    public interface IProductService
    {
        List<ProductModel> GetAll();
        PagingResult<ProductModel> GetPagingResult(PaginationModel pagination);
        void TestInsert();

        DeleteResult DeleteById(string id);
    }
}
