using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse_API.Filters;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataTransferObjects.Request.Shopping;

namespace PublishingHouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingsController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingsController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetShoppings()
        {
            var Shoppings = await _shoppingService.GetShoppings();

            return Ok(Shoppings);
        }

        [HttpGet("{id}")]
        [IsExists]
        [Authorize(Roles = "admin")]


        public async Task<IActionResult> GetShoppingById(int id)
        {
            AddShoppingRequest Shopping = await _shoppingService.GetShopping(id);
            return Ok(Shopping);
        }



        [HttpPost]
        [Authorize(Roles = "buy")]

        public async Task<IActionResult> Add(AddShoppingRequest request)
        {
            if (ModelState.IsValid)
            {
                int ShoppingId = await _shoppingService.BuyShopping(request);

                //Url yönlendirmesi,eklendiği zaman detay olrak istemciye yeni url veriyoruz
                return CreatedAtAction(nameof(GetShoppingById), routeValues: new { id = ShoppingId }, value: null);  //nameof Nesne,metot adı kullanıyorsanız hata yapmayı engeller
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [IsExists]
        [Authorize(Roles = "buy")]


        public async Task<IActionResult> Update(int id, UpdateShoppingRequest request)
        {
            if (await _shoppingService.IsShoppingExists(id))
            {
                if (ModelState.IsValid)
            {
                await _shoppingService.UpdateShopping(request);
                return Ok();
            }
            return BadRequest(ModelState);
            }
            return NotFound(new { message = $"{id} id'li ürün bulunamadı" });
        }

        [HttpDelete("{id}")]
        [IsExists]
        [Authorize(Roles = "buy")]
        [CustomException(Order = 1)]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id değeri negatif olamaz!");

            }
            await _shoppingService.DeleteShopping(id);
            return Ok();


        }
    }
}
