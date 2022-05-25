using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataTransferObjects.Request;
using PublishingHouse_DataTransferObjects.Request.Book;
using PublishingHouse_Entities.Concrete;

namespace PublishingHouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
      
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooks();  

            return Ok(books);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetBookById(int id)
        {
            AddBookRequest book = await _bookService.GetBook(id);
            return Ok(book);
        }

       

        [HttpPost]
       
        public async Task<IActionResult> Add(AddBookRequest request)
        {
            if (ModelState.IsValid)
            {
                int BookId = await _bookService.AddBook(request);

                //Url yönlendirmesi,eklendiği zaman detay olrak istemciye yeni url veriyoruz
                return CreatedAtAction(nameof(GetBookById), routeValues: new { id = BookId }, value: null);  //nameof Nesne,metot adı kullanıyorsanız hata yapmayı engeller
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, UpdateBookRequest request)
        {
            //if (await service.IsBookExists(id))
            //{
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBook(request);
                return Ok();
            }
            return BadRequest(ModelState);
            //}
            //return NotFound(new { message = $"{id} id'li ürün bulunamadı" });
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("id değeri negatif olamaz!");

            }
            await _bookService.DeleteBook(id);
            return Ok();


        }
    }
}
