using PublishingHouse_DataTransferObjects.Request.Shopping;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Abstract
{
    public interface IShoppingService
    {
        Task<IList<AddShoppingRequest>> GetShoppings();
        Task<AddShoppingRequest> GetShopping(int id);
        Task<int> BuyShopping(AddShoppingRequest request);
        Task UpdateShopping(UpdateShoppingRequest request);
        Task DeleteShopping(int id);
        Task<bool> IsShoppingExists(int id);
    }
}
