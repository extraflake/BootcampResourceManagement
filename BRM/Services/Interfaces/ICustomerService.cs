using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerVM> GetAll();
        Customer Get(string id);
        CustomerVM GetVM(string id);
        bool Insert(Customer customer);
        bool Update(string id, Customer customer);
        bool Delete(string id);
    }
}
