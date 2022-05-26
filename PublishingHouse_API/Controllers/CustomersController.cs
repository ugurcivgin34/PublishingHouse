using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse_API.Filters;
using PublishingHouse_API.Models;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataTransferObjects.Request.Customer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PublishingHouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomers()
        {
            var Customers = await _customerService.GetCustomers();

            return Ok(Customers);
        }

        [HttpGet("{id}")]
        [IsExists]


        public async Task<IActionResult> GetCustomerById(int id)
        {
            AddCustomerRequest Customer = await _customerService.GetCustomer(id);
            return Ok(Customer);
        }



        [HttpPost]

        public async Task<IActionResult> Add(AddCustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                int CustomerId = await _customerService.AddCustomer(request);

                //Url yönlendirmesi,eklendiği zaman detay olrak istemciye yeni url veriyoruz
                return CreatedAtAction(nameof(GetCustomerById), routeValues: new { id = CustomerId }, value: null);  //nameof Nesne,metot adı kullanıyorsanız hata yapmayı engeller
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [IsExists]


        public async Task<IActionResult> Update(int id, UpdateCustomerRequest request)
        {
            if (await _customerService.IsCustomerExists(id))
            {
                if (ModelState.IsValid)
                {
                    await _customerService.UpdateCustomer(request);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            return NotFound(new { message = $"{id} id'li ürün bulunamadı" });
        }

        [HttpDelete("{id}")]
        [IsExists]
        [CustomException(Order = 1)]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id değeri negatif olamaz!");

            }
            await _customerService.DeleteCustomer(id);
            return Ok();


        }

       
    }
}
