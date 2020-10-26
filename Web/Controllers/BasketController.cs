using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data.Repository;

namespace Web.Controllers {
    public class BasketController : BaseApiController {
        private readonly IBasketRepository _basketRepository;

        public BasketController (IBasketRepository basketRepository) {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById (string id) {
            var basket = await _basketRepository.GetBasketAsync (id);
            return Ok (basket ?? new CustomerBasket(id));
        }
    }
}