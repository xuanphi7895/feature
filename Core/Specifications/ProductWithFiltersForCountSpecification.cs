using System.Linq;
using Core.Entities;

namespace Core.Specifications {
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product> {
        public ProductWithFiltersForCountSpecification (ProductSpecParam productSpecParam) : base (x =>
        (string.IsNullOrEmpty(productSpecParam.Search) || x.Name.ToLower().Contains(productSpecParam.Search))
            && (!productSpecParam.BrandId.HasValue || x.ProductBrandId == productSpecParam.BrandId) &&
            (!productSpecParam.TypeId.HasValue || x.ProductTypeId == productSpecParam.TypeId))
        {

        }
    }
}