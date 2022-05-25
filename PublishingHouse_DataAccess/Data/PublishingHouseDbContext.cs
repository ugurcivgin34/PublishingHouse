using Microsoft.EntityFrameworkCore;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataAccess.Data
{
    public class PublishingHouseDbContext : DbContext
    {
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Shopping> Shoppings { get; set; }

        public PublishingHouseDbContext(DbContextOptions<PublishingHouseDbContext> options) : base(options)
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connection string'in burada olması açık bir risktir.Hem güvensiz hem de maaliyetlidir...
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
        }
      
    }
}
