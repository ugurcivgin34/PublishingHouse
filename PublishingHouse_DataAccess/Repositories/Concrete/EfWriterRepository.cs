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
    public class EfWriterRepository : IWriterRepository
    {
        private PublishingHouseDbContext _context;

        public EfWriterRepository(PublishingHouseDbContext context)
        {
            _context = context;
        }

        public async Task Add(Writer entity)
        {
            await _context.AddAsync(entity); //Belleğe ekleme yapıyor sadece..Persister api paterni,yani işleri biriktirip topluca veritabanında çalıştırmak
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Writers.FirstOrDefaultAsync(p => p.Id == id);
            _context.Writers.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Writer>> GetAll()
        {
            var writers = await _context.Writers.ToListAsync();
            return writers;
        }

        public async Task<Writer> GetById(int id)
        {
            return await _context.Writers.FindAsync(id);


        }


        public async Task<bool> IsExists(int id)
        {
            return await _context.Writers.AnyAsync(p => p.Id == id); //var mı yok mu onu kontrol ediyor
        }

        public async Task Update(Writer entity)
        {
            _context.Writers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public Writer Validate(string username, string password)
        {
            var writer = _context.Writers.FirstOrDefault(x=>x.UserName == username && x.Password == password);
            return writer;
        }
    }
}
