using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class EmployeeRoleVM
    {
        public int id { get; set; }
        public int @role { get; set; }
        public string nik { get; set; }
        public string name { get; set; }
        public string role_name { get; set; }
        public DateTime create_date { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public string DeletedBy { get; set; }

        public virtual void Update(int id, EmployeeRoleVM employeeRoleVM)
        {
            @role = employeeRoleVM.role;
            nik = employeeRoleVM.nik;
        }
    }
}
