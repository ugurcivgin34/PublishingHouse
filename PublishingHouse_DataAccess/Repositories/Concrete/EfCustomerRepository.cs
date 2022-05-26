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
    public class EfCustomerRepository : ICustomerRepository
    {
        private PublishingHouseDbContext _context;

        public EfCustomerRepository(PublishingHouseDbContext context)
        {
            _context = context;
        }

        public async Task Add(Customer entity)
        {
            await _context.AddAsync(entity); //Belleğe ekleme yapıyor sadece..Persister api paterni,yani işleri biriktirip topluca veritabanında çalıştırmak
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
            _context.Customers.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Customer>> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FindAsync(id);


        }


        public async Task<bool> IsExists(int id)
        {
            return await _context.Categories.AnyAsync(p => p.Id == id); //var mı yok mu onu kontrol ediyor
        }

        public async Task Update(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public Customer Validate(string username, string password)
        {
            var customer = _context.Customers.FirstOrDefault(p => p.UserName == username && p.Password == password);
            return customer;
        }
    }
}
