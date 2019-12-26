using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerVM> GetAll();
        Customer Get(string id);
        CustomerVM GetVM(string id);
        bool Insert(Customer customer);
        bool Update(string id, Customer customer);
        bool Delete(string id);

    }
}
