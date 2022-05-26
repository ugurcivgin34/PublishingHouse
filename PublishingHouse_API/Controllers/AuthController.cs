using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse_API.Models;
using PublishingHouse_Business.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PublishingHouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWriterService _writerService;
        private readonly ICustomerService _customerService;

        public AuthController(IWriterService writerService, ICustomerService customerService)
        {
            _writerService = writerService;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var writerUser = _writerService.Validate(model.UserName, model.Password);
                var customerUser = _customerService.Validate(model.UserName, model.Password);
                if (writerUser != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName,writerUser.UserName),
                        new Claim(JwtRegisteredClaimNames.Email,writerUser.Email),
                        new Claim(ClaimTypes.Role,writerUser.Role),
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); //salting

                    var token = new JwtSecurityToken(
                        issuer: "u.civgin@gmail.com",  //tokena erişecek olan , yani kim tarafındna erişecekse yayıncı kim ise onu yazıyoruz
                        audience: "u.civgin@gmail.com", //Bu yayıncıyı kim kullanacak
                        claims: claims,
                        notBefore: DateTime.Now,//şuandan önce kullanılamaz,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials //token doğrulama
                        );
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else if (customerUser != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName,customerUser.UserName),
                        new Claim(JwtRegisteredClaimNames.Email,customerUser.Email),
                        new Claim(ClaimTypes.Role,customerUser.Role),
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); //salting

                    var token = new JwtSecurityToken(
                        issuer: "u.civgin@gmail.com",  //tokena erişecek olan , yani kim tarafındna erişecekse yayıncı kim ise onu yazıyoruz
                        audience: "u.civgin@gmail.com", //Bu yayıncıyı kim kullanacak
                        claims: claims,
                        notBefore: DateTime.Now,//şuandan önce kullanılamaz,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials //token doğrulama
                        );
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
