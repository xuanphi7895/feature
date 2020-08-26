using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

using System.Diagnostics;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Infrastructure.Data.Repository;
using Infrastructure.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Core.Specifications;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger _logger;
          
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;

        public ProductsController(IGenericRepository<Product> productRepository,
                                  IGenericRepository<ProductBrand> productBrandRepository,
                                  IGenericRepository<ProductType> productTypeRepository,
                                  //ILoggerFactory logFactory
                                  ILogger<ProductsController> logger)
        {
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            //  _logger = logFactory.CreateLogger<ProductsController>();
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> getProducts()
        {
             _logger.LogInformation("Get list product");
            var spec = new ProductsWithTypesAndbrandsSpecification();
            return Ok(await _productRepository.ListAsync(spec));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProductById(int id)
        {
            _logger.LogInformation("Get By Id Product {id}", id);
            var spec = new ProductsWithTypesAndbrandsSpecification(id);
            return Ok(await _productRepository.GetEntityWithSpec(spec));
        }
         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }
    }
}