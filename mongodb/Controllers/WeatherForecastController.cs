using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mongodb.ConfigModels;
using mongodb.Interfaces;
using mongodb.Models;
using System;
using System.Collections.Generic;

namespace mongodb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public ConnectionStringConfig _ConnectionString { get; set; }

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductService _ProductService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<ConnectionStringConfig> memberConfig, IProductService productService)
        {
            _logger = logger;
            _ConnectionString = memberConfig.Value;
            _ProductService = productService;
        }
        
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            throw new NotImplementedException();
        }
    }
    
}
