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
    public class EfCategoryRepository : ICategoryRepository
    {
        private PublishingHouseDbContext _context;

        public EfCategoryRepository(PublishingHouseDbContext context)
        {
            _context = context;
        }

        public async Task Add(Category entity)
        {
            await _context.AddAsync(entity); //Belleğe ekleme yapıyor sadece..Persister api paterni,yani işleri biriktirip topluca veritabanında çalıştırmak
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
            _context.Categories.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Category>> GetAll()
        {
            var Categorys = await _context.Categories.ToListAsync();
            return Categorys;
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);


        }


        public async Task<bool> IsExists(int id)
        {
            return await _context.Categories.AnyAsync(p => p.Id == id); //var mı yok mu onu kontrol ediyor
        }

        public async Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
