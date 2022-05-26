using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse_API.Filters;
using PublishingHouse_API.Models;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataTransferObjects.Request.Writer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PublishingHouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {
        private readonly IWriterService _writerService;

        //[HttpPost]
        //public IActionResult Login(UserLoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = _writerService.Validate(model.UserName, model.Password);
        //        if (user != null)
        //        {
        //            var claims = new[]
        //            {
        //                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
        //                new Claim(JwtRegisteredClaimNames.Email,user.Email),
        //                new Claim(ClaimTypes.Role,user.Role),
        //            };

        //            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("superSecretKey@345"));
        //            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); //salting

        //            var token = new JwtSecurityToken(
        //                issuer: "u.civgin@gmail.com",  //tokena erişecek olan , yani kim tarafındna erişecekse yayıncı kim ise onu yazıyoruz
        //                audience: "u.civgin@gmail.com", //Bu yayıncıyı kim kullanacak
        //                claims: claims,
        //                notBefore: DateTime.Now,//şuandan önce kullanılamaz,
        //                expires: DateTime.Now.AddMinutes(5),
        //                signingCredentials: signinCredentials //token doğrulama
        //                );
        //            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        //        }
        //        else
        //        {
        //            return Unauthorized();
        //        }
        //    }
        //    return BadRequest(ModelState);
        //}

        public WritersController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetWriters()
        {
            var Writers = await _writerService.GetWriters();

            return Ok(Writers);
        }

        [HttpGet("{id}")]
        [IsExists]
        public async Task<IActionResult> GetWriterById(int id)
        {
            UpdateWriterRequest Writer = await _writerService.GetWriter(id);
            return Ok(Writer);
        }



        [HttpPost]
        public async Task<IActionResult> Add(AddWriterRequest request)
        {
            if (ModelState.IsValid)
            {
                int WriterId = await _writerService.AddWriter(request);

                //Url yönlendirmesi,eklendiği zaman detay olrak istemciye yeni url veriyoruz
                return CreatedAtAction(nameof(GetWriterById), routeValues: new { id = WriterId }, value: null);  //nameof Nesne,metot adı kullanıyorsanız hata yapmayı engeller
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [IsExists]
        public async Task<IActionResult> Update(int id, UpdateWriterRequest request)
        {
            if (await _writerService.IsWriterExists(id))
            {
                if (ModelState.IsValid)
                {
                    await _writerService.UpdateWriter(request);
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
            await _writerService.DeleteWriter(id);
            return Ok();


        }
    }
}
