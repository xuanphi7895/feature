using Core.Entities;

namespace Core.Specifications {
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product> {
        public ProductWithFiltersForCountSpecification (ProductSpecParam productSpecParam) : base (x => (!productSpecParam.BrandId.HasValue || x.ProductBrandId == productSpecParam.BrandId) &&
            (!productSpecParam.TypeId.HasValue || x.ProductTypeId == productSpecParam.TypeId))
        {

        }
    }
}