using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyContext _myContext;
        IEnumerable<CustomerVM> resultListVM = null;
        Customer result = null;
        CustomerVM resultVM = null;
        bool status = false;

        public CustomerRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<CustomerVM> GetAll()
        {
            resultListVM = _myContext.CustomerVMs.FromSql($"call sp_retrieve_customer").ToList();
            return resultListVM;
        }

        public Customer Get(string id)
        {
            result = _myContext.Customers.FirstOrDefault(x => x.id == id);
            return result;
        }

        public bool Insert(Customer customer)
        {
            var push = new Customer(customer.id, customer.name, customer.address, customer.phone, customer.relation_manager, customer.district);
            _myContext.Customers.Add(push);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
            
        }

        public CustomerVM GetVM(string id)
        {
            resultVM = _myContext.CustomerVMs.FromSql($"call sp_retrieve_customer").SingleOrDefault(x => x.id == id);
            return resultVM;
        }

        public bool Update(string id, Customer customer)
        {
            var get = Get(id);
            get.Update(customer.id, customer.name, customer.address, customer.phone, customer.relation_manager, customer.district);
            _myContext.Entry(get).State = EntityState.Modified;
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public bool Delete(string id)
        {
            var get = Get(id);
            _myContext.Customers.Remove(get);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }
    }
}
