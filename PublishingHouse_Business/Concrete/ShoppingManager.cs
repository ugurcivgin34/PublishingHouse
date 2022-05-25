using AutoMapper;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_DataTransferObjects.Request.Shopping;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Concrete
{
    public class ShoppingManager : IShoppingService
    {
        private readonly IShoppingRepository _shoppingRepository;
        private readonly IMapper mapper;

        public ShoppingManager(IShoppingRepository shoppingRepository, IMapper mapper)
        {
            _shoppingRepository = shoppingRepository;
            this.mapper = mapper;
        }

        public async Task<int> BuyShopping(AddShoppingRequest request)
        {
            var shopping = mapper.Map<Shopping>(request);
            await _shoppingRepository.Add(shopping);
            return shopping.Id;
        }

        public async Task DeleteShopping(int id)
        {
            await _shoppingRepository.Delete(id);
        }

        public async Task<AddShoppingRequest> GetShopping(int id)
        {
            var shopping = await _shoppingRepository.GetById(id);
            var shoppingDisplayResponse = mapper.Map<AddShoppingRequest>(shopping);
            return shoppingDisplayResponse;
        }

        public async Task<IList<AddShoppingRequest>> GetShoppings()
        {
            var shoppings = await _shoppingRepository.GetAll();
            var result = mapper.Map<IList<AddShoppingRequest>>(shoppings);
            return result;
        }

        public async Task<bool> IsShoppingExists(int id)
        {
            return await _shoppingRepository.IsExists(id);
        }

        public async Task UpdateShopping(UpdateShoppingRequest request)
        {
            var shopping = mapper.Map<Shopping>(request);
            await _shoppingRepository.Update(shopping);
        }
    }
}
