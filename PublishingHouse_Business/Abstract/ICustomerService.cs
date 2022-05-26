using PublishingHouse_DataTransferObjects.Request.Customer;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Abstract
{
    public interface ICustomerService
    {
        Task<IList<AddCustomerRequest>> GetCustomers();
        Task<AddCustomerRequest> GetCustomer(int id);
        Task<int> AddCustomer(AddCustomerRequest request);
        Task UpdateCustomer(UpdateCustomerRequest request);
        Task DeleteCustomer(int id);
        Task<bool> IsCustomerExists(int id);
        Customer Validate(string username, string password);

    }
}
