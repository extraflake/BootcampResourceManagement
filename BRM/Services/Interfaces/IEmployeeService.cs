using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> Get();
        IEnumerable<EmployeeDisplayVM> GetDisplay();
        Employee Get(string email);
        EmployeeDisplayVM GetByIdDisplay(string email);
        IEnumerable<TrainerVM> GetTrainer(int id);
        bool Insert(EmployeeVM employeeVM);
        bool Update(string email, EmployeeVM employeeVM);
        bool UpdatePrimary(string email, EmployeeVM employeeVM);
        bool Delete(string id, EmployeeVM employeeVM);
        bool ForgotPassword(string email);
        bool UpdateToken(string email, string defaultToken);
    }
}
