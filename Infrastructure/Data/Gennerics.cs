using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Core.Entities;
using System.Linq;
namespace Infrastructure.Data
{
    public class Gennerics
    {
        private readonly StoreContext _context;
        public Gennerics(StoreContext context){
            _context = context;
        }

        public void ReadSeedData(){

            //  if (!_context.ProductBrands.Any())
            //     {
            //         var fileName = "../Data/SeedData/brands.json";
            //         var brandsData = File.ReadAllText(fileName);
            //         var brands = JsonSerializer.Deserialize<List<T>>(brandsData);
            //         foreach (var item in brands)
            //         {
            //             _context.ProductBrands.Add(item);
            //         }
            //     }
        }
    }
}