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
    public class EmployeeRoleService : IEmployeeRoleService
    {
        bool status = false;
        private IEmployeeRoleRepository _employeeRoleRepository;

        public EmployeeRoleService(IEmployeeRoleRepository employeeRoleRepository)
        {
            _employeeRoleRepository = employeeRoleRepository;
        }

        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            return _employeeRoleRepository.Delete(id);
        }

        public IEnumerable<EmployeeRoleVM> Get()
        {
            return _employeeRoleRepository.Get();
        }

        public EmployeeRoleVM Get(int id)
        {
            return _employeeRoleRepository.Get(id);
        }
        public bool Insert(InsertEmployeeRoleVM insertEmployeeRoleVM)
        {
                return _employeeRoleRepository.Insert(insertEmployeeRoleVM);
        }

        public bool Update(int id, EmployeeRoleVM employeeRoleVM)
        {
            if (String.IsNullOrWhiteSpace(employeeRoleVM.id.ToString()))
            {
                return status;
            }
            else
            {
                return _employeeRoleRepository.Update(id, employeeRoleVM);
            }
        }
    }
}
