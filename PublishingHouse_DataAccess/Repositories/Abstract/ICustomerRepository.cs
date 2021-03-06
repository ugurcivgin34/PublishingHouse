using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataAccess.Repositories.Abstract
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Validate(string username, string password);

    }
}
