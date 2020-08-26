using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndbrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndbrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        // criteria: tiêu chí: nghĩa là tên trường truyền vào để query 
        public ProductsWithTypesAndbrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}