using mongodb.Interfaces;
using mongodb.IRepositories;
using mongodb.Models;
using mongodb.Models.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongodb.Services
{
    public class ProductService : IProductService
    {
        IProductRepository ProductRepository { get; set; }
        public ProductService(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public List<ProductModel> GetAll()
        {
            return ProductRepository.GetAll();
        }
        public PagingResult<ProductModel> GetPagingResult(PaginationModel pagination)
        {
            return ProductRepository.GetPagingResult(pagination);
        }
        public DeleteResult DeleteById(string id)
        {
            return ProductRepository.DeleteById(id);
        }
        public void TestInsert()
        {
            ProductRepository.Insert(new ProductModel 
            {
                Name = Guid.NewGuid().ToString()
            });
        }
    }
}
