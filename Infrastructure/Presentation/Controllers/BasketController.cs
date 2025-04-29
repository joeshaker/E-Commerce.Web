using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDto;

namespace Presentation.Controllers
{

    public class BasketController(IServiceManager serviceManager) : ApiBaseController
    {
        [HttpGet]

        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket = await serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }

        [HttpPost]

        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        [HttpDelete("{key}")]

        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
             var Result = await serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);
        }

    }
}
