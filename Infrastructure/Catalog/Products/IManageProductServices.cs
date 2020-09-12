using System.Collections.Generic;
using Infrastructure.Catalog.Products.Dtos;

namespace Infrastructure.Catalog.Products {
    public interface IManageProductServices {
        int Create (ProductCreateRequest request);
        int Update (ProductCreateRequest request);
        int Delete (int productId);
        List<ProductViewModel> GetAll ();
        List<ProductViewModel> GetAllPaging ();
    }
}