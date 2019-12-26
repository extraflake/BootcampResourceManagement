using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IEmployeeRoleService
    {
        IEnumerable<EmployeeRoleVM> Get();
        EmployeeRoleVM Get(int id);
        bool Insert(InsertEmployeeRoleVM insertEmployeeRoleVM);
        bool Update(int id, EmployeeRoleVM employeeRoleVM);
        bool Delete(int id);
    }
}
