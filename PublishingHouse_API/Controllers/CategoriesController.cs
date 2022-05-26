using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse_API.Filters;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataTransferObjects.Request.Category;

namespace PublishingHouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCategoryies()
        {
            var Categorys = await _categoryService.GetCategories();

            return Ok(Categorys);
        }

        [HttpGet("{id}")]
        [IsExists]


        public async Task<IActionResult> GetCategoryById(int id)
        {
            AddCategoryRequest Category = await _categoryService.GetCategory(id);
            return Ok(Category);
        }



        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Add(AddCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                int CategoryId = await _categoryService.AddCategory(request);

                //Url yönlendirmesi,eklendiği zaman detay olrak istemciye yeni url veriyoruz
                return CreatedAtAction(nameof(GetCategoryById), routeValues: new { id = CategoryId }, value: null);  //nameof Nesne,metot adı kullanıyorsanız hata yapmayı engeller
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [IsExists]
        [Authorize(Roles = "admin")]


        public async Task<IActionResult> Update(int id, UpdateCategoryRequest request)
        {
            if (await _categoryService.IsCategoryExists(id))
            {
                if (ModelState.IsValid)
                {
                    await _categoryService.UpdateCategory(request);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            return NotFound(new { message = $"{id} id'li ürün bulunamadı" });
        }

        [HttpDelete("{id}")]
        [IsExists]
        [Authorize(Roles = "admin")]
        [CustomException(Order = 1)]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id değeri negatif olamaz!");

            }
            await _categoryService.DeleteCategory(id);
            return Ok();


        }
    }
}
