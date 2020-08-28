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
using Web.DTOs;
using System.Linq;
using AutoMapper;

namespace Web.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;

        public ProductsController(IGenericRepository<Product> productRepository,
                                  IGenericRepository<ProductBrand> productBrandRepository,
                                  IGenericRepository<ProductType> productTypeRepository,
                                  //ILoggerFactory logFactory
                                  ILogger<ProductsController> logger,
                                  IMapper mapper)
        {
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            //  _logger = logFactory.CreateLogger<ProductsController>();
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> getProducts()
        {
             _logger.LogInformation("Get list product");
            var spec = new ProductsWithTypesAndbrandsSpecification();
            var products = await _productRepository.ListAsync(spec);
            //return products.Select(products => new ProductToReturnDto(products)).ToList();
            // return products.Select(products => new ProductToReturnDto{
            //     Id = products.Id
            // }).ToList();
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> getProductById(int id)
        {
            _logger.LogInformation("Get By Id Product {id}", id);
            var spec = new ProductsWithTypesAndbrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(product);
            //return Ok(await _productRepository.GetEntityWithSpec(spec));
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