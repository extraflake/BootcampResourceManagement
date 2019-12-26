using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Data.Models
{
    [Table("tb_tr_employee_role")]
    public class EmployeeRole
    {
        public int id { get; set; }
        public bool is_deleted { get; set; }
        public int role { get; set; }
        public string employee { get; set; }

        public EmployeeRole() { }

        public EmployeeRole(EmployeeRoleVM employeeRoleVM)
        {
            id = employeeRoleVM.id;
            role = employeeRoleVM.role;
            employee = employeeRoleVM.nik;
            string name = employeeRoleVM.name;
            string role_name = employeeRoleVM.role_name;
        }

        public virtual void Update(int id, EmployeeRoleVM employeeRoleVM)
        {
            role = employeeRoleVM.role;
            employee = employeeRoleVM.nik;
        }
    }



}
