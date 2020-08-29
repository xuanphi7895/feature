using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications;
namespace Core.Specifications {
    public class ProductsWithTypesAndbrandsSpecification : BaseSpecification<Product> {
        public ProductsWithTypesAndbrandsSpecification (ProductSpecParam productSpecParam) : 
                base (x => (!productSpecParam.BrandId.HasValue || x.ProductBrandId == productSpecParam.BrandId) 
                && (!productSpecParam.TypeId.HasValue || x.ProductTypeId == productSpecParam.TypeId)) 
            {
            AddInclude (x => x.ProductType);
            AddInclude (x => x.ProductBrand);
            AddOrderBy (x => x.Name);
            // Result = DataSource.Skip((PN – 1) * NRP).Take(NRP)
            ApplyPaging ((productSpecParam.PageIndex - 1) * productSpecParam.PageSize, productSpecParam.PageSize);

            if (!string.IsNullOrEmpty (productSpecParam.Sort)) {
                switch (productSpecParam.Sort) {
                    case "priceAsc":
                        AddOrderBy (p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending (p => p.Price);
                        break;
                    default:
                        AddOrderBy (n => n.Name);
                        break;
                }

            }

        }

        // criteria: tiêu chí: nghĩa là tên trường truyền vào để query 
        public ProductsWithTypesAndbrandsSpecification (int id, string sort) : base (x => x.Id == id) {
            AddInclude (x => x.ProductType);
            AddInclude (x => x.ProductBrand);
            AddOrderBy (x => x.Name);
            if (!string.IsNullOrEmpty (sort)) {
                switch (sort) {
                    case "priceAsc":
                        AddOrderBy (p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending (p => p.Price);
                        break;
                    default:
                        AddOrderBy (n => n.Name);
                        break;
                }

            }

        }
    }
}