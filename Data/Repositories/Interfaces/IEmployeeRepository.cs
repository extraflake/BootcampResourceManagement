using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
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
    }
}
