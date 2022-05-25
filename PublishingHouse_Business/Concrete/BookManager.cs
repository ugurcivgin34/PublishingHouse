using AutoMapper;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_DataTransferObjects.Request;
using PublishingHouse_DataTransferObjects.Request.Book;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper mapper;

       

        private List<Book> books;

        public BookManager(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddBook(AddBookRequest request)
        {
            var book = mapper.Map<Book>(request);
            await _bookRepository.Add(book);
            return book.Id;
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.Delete(id);
        }

        public async Task<AddBookRequest> GetBook(int id)
        {
            
            var book = await _bookRepository.GetById(id);
            var bookDisplayResponse = mapper.Map<AddBookRequest>(book);
            return bookDisplayResponse;
        }

        public async Task<IList<AddBookRequest>> GetBooks()
        {
            var books = await _bookRepository.GetAll();
            var result = mapper.Map<IList<AddBookRequest>>(books);
            return result;
        }



        public async Task<bool> IsBookExists(int id)
        {
            return await _bookRepository.IsExists(id);

        }

        public async Task UpdateBook(UpdateBookRequest request)
        {
            var product = mapper.Map<Book>(request);
            await _bookRepository.Update(product);
        }
    }




}
