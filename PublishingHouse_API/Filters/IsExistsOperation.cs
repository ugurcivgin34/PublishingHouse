using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PublishingHouse_Business.Abstract;

namespace PublishingHouse_API.Filters
{
    public class IsExistsOperation : IAsyncActionFilter
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        private readonly IShoppingService _shoppingService;
        private readonly IWriterService _writerService;

        public IsExistsOperation(IBookService bookService, ICategoryService categoryService,
            ICustomerService customerService, IShoppingService shoppingService, IWriterService writerService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _customerService = customerService;
            _shoppingService = shoppingService;
            _writerService = writerService;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">Controller içinde bir action var,o actionun bütün bilgilerini context ile ulaşabiliriz .Yani controllerde neyin üstünde arttibute şeklinde yazdıysak herşeyi context tutar</param>
        /// <param name="next">İşlem tamamlandıktan sonra ne yapmak istiyoruz</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
            //metod prametrlerinden containskey id yoksa
            if (!context.ActionArguments.ContainsKey("id"))
            {
                context.Result = new BadRequestObjectResult("Id is required");
            }
            else
            {
                //Böyle bir id varsa o zaman bu id değerini al
                var id = (int)context.ActionArguments["id"];
                var result = context.ActionDescriptor.RouteValues.Values;
                
                if (result.Contains("Books") && !await _bookService.IsBookExists(id))
                {
                    context.Result = new NotFoundObjectResult(new { message = $"{id} id'li kitap bulunamadı" }); //böyle ürün yoksa 
                }
                else if (result.Contains("Categories") && !await _categoryService.IsCategoryExists(id))
                {
                    context.Result = new NotFoundObjectResult(new { message = $"{id} id'li kategori bulunamadı" }); //böyle ürün yoksa 
                }
                else if (result.Contains("Customers") && !await _customerService.IsCustomerExists(id))
                {
                    context.Result = new NotFoundObjectResult(new { message = $"{id} id'li müşteri bulunamadı" }); //böyle ürün yoksa 
                }
                else if (result.Contains("Shoppings") && !await _shoppingService.IsShoppingExists(id))
                {
                    context.Result = new NotFoundObjectResult(new { message = $"{id} id'li alışveriş bulunamadı" }); //böyle ürün yoksa 
                }
                else if (result.Contains("Writers") && !await _writerService.IsWriterExists(id))
                {
                    context.Result = new NotFoundObjectResult(new { message = $"{id} id'li yazar bulunamadı" }); //böyle ürün yoksa 
                }
                else
                {
                    await next.Invoke(); //varsa invoke et

                }
            }


        }

      
    }
}
