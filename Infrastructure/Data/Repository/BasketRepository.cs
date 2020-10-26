using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
namespace Infrastructure.Data.Repository {
    public class BasketRepository : IBasketRepository {

        public Task<bool> DeleteBasketAsync (string basketId) {
            throw new System.NotImplementedException ();
        }
        public Task<CustomerBasket> UpdateBasketAsync (CustomerBasket basket) {
            throw new System.NotImplementedException ();
        }
        public Task<CustomerBasket> GetBasketAsync (string basket) {
            throw new System.NotImplementedException ();
        }
    }
}