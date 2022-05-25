using PublishingHouse_DataTransferObjects.Request.Book;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Abstract
{
    public interface IBookService
    {
        Task<IList<AddBookRequest>> GetBooks();
        Task<AddBookRequest> GetBook(int id);
        Task<int> AddBook(AddBookRequest request);
        Task UpdateBook(UpdateBookRequest request);
        Task DeleteBook(int id);
        Task<bool> IsBookExists(int id);
    }
}
