using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mongodb.ConfigModels;
using mongodb.Interfaces;
using mongodb.Models;
using mongodb.Models.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mongodb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public ConnectionStringConfig _ConnectionString { get; set; }

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductService _ProductService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProductService productService)
        {
            _logger = logger;
            _ProductService = productService;
        }
        
        [HttpGet]
        public PagingResult<ProductModel> Get([FromQuery] PaginationModel pagination)
        {
            pagination.SortBy = nameof(ProductModel._id);
            return _ProductService.GetPagingResult(pagination);
        }

        [HttpGet]
        [Route("TestInsert")]
        public string TestInsert()
        {
            _ProductService.TestInsert();
            return "OK";
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public DeleteResult DeleteById(string id)
        {
            return _ProductService.DeleteById(id);
        }
    }
    
}
