using Microsoft.AspNetCore.Mvc;

namespace PublishingHouse_API.Filters
{
    public class IsExistsAttribute : TypeFilterAttribute
    {
        //Attribute ler depency enjection ile olmuyor.Çünkü insteance alamıyoruz
        public IsExistsAttribute() : base(typeof(IsExistsOperation))
        {

        }
    }
}
