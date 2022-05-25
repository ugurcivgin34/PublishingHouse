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
    public class EfShoppingRepository : IShoppingRepository
    {
        private PublishingHouseDbContext _context;

        public EfShoppingRepository(PublishingHouseDbContext context)
        {
            _context = context;
        }

        public async Task Add(Shopping entity)
        {
            await _context.AddAsync(entity); //Belleğe ekleme yapıyor sadece..Persister api paterni,yani işleri biriktirip topluca veritabanında çalıştırmak
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Shoppings.FirstOrDefaultAsync(p => p.Id == id);
            _context.Shoppings.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Shopping>> GetAll()
        {
            var Shoppings = await _context.Shoppings.ToListAsync();
            return Shoppings;
        }

        public async Task<Shopping> GetById(int id)
        {
            return await _context.Shoppings.FindAsync(id);


        }


        public async Task<bool> IsExists(int id)
        {
            return await _context.Shoppings.AnyAsync(p => p.Id == id); //var mı yok mu onu kontrol ediyor
        }

        public async Task Update(Shopping entity)
        {
            _context.Shoppings.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
