using AutoMapper;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_DataTransferObjects.Request.Customer;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper mapper;
        private List<Customer> categories;

        public CustomerManager(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddCustomer(AddCustomerRequest request)
        {
            var customer = mapper.Map<Customer>(request);
            await _customerRepository.Add(customer);
            return customer.Id;
        }

        public async Task DeleteCustomer(int id)
        {
            await _customerRepository.Delete(id);
        }

        public async Task<AddCustomerRequest> GetCustomer(int id)
        {

            var customer = await _customerRepository.GetById(id);
            var customerDisplayResponse = mapper.Map<AddCustomerRequest>(customer);
            return customerDisplayResponse;
        }

        public async Task<IList<AddCustomerRequest>> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();
            var result = mapper.Map<IList<AddCustomerRequest>>(customers);
            return result;
        }



        public async Task<bool> IsCustomerExists(int id)
        {
            return await _customerRepository.IsExists(id);

        }

        public async Task UpdateCustomer(UpdateCustomerRequest request)
        {
            var customer = mapper.Map<Customer>(request);
            await _customerRepository.Update(customer);
        }

        
    }
}
