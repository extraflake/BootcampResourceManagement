using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class CustomerService : ICustomerService
    {
        bool status = false;
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerVM> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer Get(string id)
        {
            return _customerRepository.Get(id);
        }

        public bool Insert(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.id) ||
                string.IsNullOrWhiteSpace(customer.name) ||
                string.IsNullOrWhiteSpace(customer.address) ||
                string.IsNullOrWhiteSpace(customer.phone) ||
                string.IsNullOrWhiteSpace(customer.relation_manager) ||
                string.IsNullOrWhiteSpace(customer.district.ToString()))
            {
                return status;
            }
            else
            {
                return _customerRepository.Insert(customer);
            }
        }

        public CustomerVM GetVM(string id)
        {
            return _customerRepository.GetVM(id);
        }

        public bool Update(string id, Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.id) ||
                string.IsNullOrWhiteSpace(customer.name) ||
                string.IsNullOrWhiteSpace(customer.address) ||
                string.IsNullOrWhiteSpace(customer.phone) ||
                string.IsNullOrWhiteSpace(customer.relation_manager) ||
                string.IsNullOrWhiteSpace(customer.district.ToString()))
            {
                return status;
            }
            else
            {
                return _customerRepository.Update(id, customer);
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return _customerRepository.Delete(id);
            }
        }
    }
}
