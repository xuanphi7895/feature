using System;
namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name {get; set;}
        public string Description {get;set;}
        public decimal Price {get;set;}
         public decimal OriginalPrice {get;set;}
        public string PictureUrl {get;set;}
        public int Stock {get;set;}
        public int ViewCount {get;set;}
        public DateTime DateCreated {get;set;}
        public string SeoAlias {get;set;}
        public ProductType ProductType {get;set;}
        public int ProductTypeId {get;set;}
        public ProductBrand ProductBrand {get;set;}
        public int ProductBrandId {get;set;}
    }
}