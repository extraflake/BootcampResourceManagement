using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IEmployeeRoleRepository
    {
        IEnumerable<EmployeeRoleVM> Get();
        EmployeeRoleVM Get(int id);
        EmployeeRole GetId(int id);
        bool Insert(InsertEmployeeRoleVM insertEmployeeRoleVM);
        bool Update(int id, EmployeeRoleVM employeeRoleVM);
        bool Delete(int id);
    }
}
