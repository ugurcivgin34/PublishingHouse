using Microsoft.EntityFrameworkCore;
using PublishingHouse_DataAccess.Data;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataAccess.Repositories.Concrete
{
    public class EfBookRepository : IBookRepository
    {
        private PublishingHouseDbContext _context;

        public EfBookRepository(PublishingHouseDbContext context)
        {
            _context = context;
        }

        public async Task Add(Book entity)
        {
            await _context.AddAsync(entity); //Belleğe ekleme yapıyor sadece..Persister api paterni,yani işleri biriktirip topluca veritabanında çalıştırmak
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Books.FirstOrDefaultAsync(p => p.Id == id);
            _context.Books.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Book>> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books.FindAsync(id);


        }


        public async Task<bool> IsExists(int id)
        {
            return await _context.Books.AnyAsync(p => p.Id == id); //var mı yok mu onu kontrol ediyor
        }

        public async Task Update(Book entity)
        {
            _context.Books.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
