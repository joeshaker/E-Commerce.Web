using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Execptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDto;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreateOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket: CustomerBasket);
            if (CreateOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else

                throw new Exception(message:

                "Can Not Update Or Create Basket Now , Try Again Later");
        }


        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket= await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else 
            {
                throw new BasketNotFoundException(Key);
            }
        }
        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _basketRepository.DeleteBasketAsync(Key);
        }
    }
}
