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
    public class EmployeeRoleRepository : IEmployeeRoleRepository
    {
        private readonly MyContext _myContext;
        IEnumerable<EmployeeRoleVM> resultListVM = null;
        EmployeeRoleVM resultVM = null;
        EmployeeRole result = null;
        bool status = false;

        public EmployeeRoleRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool Delete(int id)
        {
            var delete = _myContext.EmployeeRoles.Find(id);
            _myContext.EmployeeRoles.Remove(delete);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public IEnumerable<EmployeeRoleVM> Get()
        {
            resultListVM = _myContext.EmployeeRoleVMs.FromSql($"call sp_retrieve_employee_role").ToList().OrderByDescending(x => x.create_date);
            return resultListVM;
        }

        public EmployeeRoleVM Get(int id)
        {
            resultVM = _myContext.EmployeeRoleVMs.FromSql($"call sp_retrieve_employee_role_id({id})").FirstOrDefault();
            return resultVM;
        }

        public EmployeeRole GetId(int id)
        {
            result = _myContext.EmployeeRoles.Find(id);
            return result;
        }

        public bool Insert(InsertEmployeeRoleVM insertEmployeeRoleVM)
        {
            for (int i = 0; i < insertEmployeeRoleVM.role.ToArray().Length; i++)
            {
                var employee_role = new EmployeeRole()
                {
                    role = insertEmployeeRoleVM.role[i],
                    employee = insertEmployeeRoleVM.nik
                };
                _myContext.EmployeeRoles.Add(employee_role);
            }
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public bool Update(int id, EmployeeRoleVM employeeRoleVM)
        {
            var get = GetId(id);
            get.Update(id, employeeRoleVM);
            _myContext.Entry(get).State = EntityState.Modified;
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }
    }
}
