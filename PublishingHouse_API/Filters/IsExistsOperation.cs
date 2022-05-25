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
                if (!await _bookService.IsBookExists(id) || !await _categoryService.IsCategoryExists(id) || !await _customerService.IsCustomerExists(id)
                    || !await _shoppingService.IsShoppingExists(id) || !await _writerService.IsWriterExists(id))
                {
                    context.Result = new NotFoundObjectResult(new { message = $"{id} id'li ürün bulunamadı" }); //böyle ürün yoksa 
                }
                else
                {
                    await next.Invoke(); //varsa invoke et

                }
            }


        }
    }
}
