using AutoMapper;
using PublishingHouse_DataTransferObjects.Request;
using PublishingHouse_DataTransferObjects.Request.Book;
using PublishingHouse_DataTransferObjects.Request.Category;
using PublishingHouse_DataTransferObjects.Request.Customer;
using PublishingHouse_DataTransferObjects.Request.Shopping;
using PublishingHouse_DataTransferObjects.Request.Writer;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Mapping
{
    public class MapProfile :Profile
    {
        public MapProfile()
        {
            CreateMap<AddBookRequest, Book>().ReverseMap();
            CreateMap<AddCategoryRequest, Category>().ReverseMap();
            CreateMap<AddCustomerRequest, Customer>().ReverseMap();
            CreateMap<AddShoppingRequest, Shopping>().ReverseMap();
            CreateMap<AddWriterRequest, Writer>().ReverseMap();

            CreateMap<UpdateBookRequest, Book>().ReverseMap();
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();
            CreateMap<UpdateCustomerRequest, Customer>().ReverseMap();
            CreateMap<UpdateShoppingRequest, Shopping>().ReverseMap();
            CreateMap<UpdateWriterRequest, Writer>().ReverseMap();
        }
    }
}
