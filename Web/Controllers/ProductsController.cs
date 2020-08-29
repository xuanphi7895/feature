using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.DTOs;
using Web.Errors;
using Web.Helpers;

namespace Web.Controllers {
    public class ProductsController : BaseApiController {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;

        public ProductsController (IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            //ILoggerFactory logFactory
            ILogger<ProductsController> logger,
            IMapper mapper) {
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            //  _logger = logFactory.CreateLogger<ProductsController>();
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> getProducts ([FromQuery] ProductSpecParam productSpecParam) {
            _logger.LogInformation ("Get list product");
            // filter product with condition 
            var spec = new ProductsWithTypesAndbrandsSpecification (productSpecParam);
            // count total
            var countSpec = new ProductWithFiltersForCountSpecification (productSpecParam);
            //return total quantity
            var totalItems = await _productRepository.CountAsync (countSpec);
            // return data
            var products = await _productRepository.ListAsync (spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products);
            //return pagination
            return Ok (new Pagination<ProductToReturnDto>(productSpecParam.PageIndex, productSpecParam.PageSize, totalItems, data));
        }

        [HttpGet ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (typeof (ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> getProductById (int id, string sort) {
            _logger.LogInformation ("Get By Id Product {id}", id);
            var spec = new ProductsWithTypesAndbrandsSpecification (id, sort);
            var product = await _productRepository.GetEntityWithSpec (spec);
            if (product == null) {
                return NotFound (new ApiResponse (404));
            }
            return _mapper.Map<Product, ProductToReturnDto> (product);
            //return Ok(await _productRepository.GetEntityWithSpec(spec));
        }

        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes () {
            return Ok (await _productTypeRepository.ListAllAsync ());
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands () {
            return Ok (await _productBrandRepository.ListAllAsync ());
        }
    }
}