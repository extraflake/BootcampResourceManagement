using Data.Base;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime? birth_date { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        //public byte[] photo { get; set; }
        public bool? is_deleted { get; set; }
        public int? hiring_location { get; set; }
        public DateTime? create_date { get; set; }
        
        public virtual ICollection<BatchClass> BatchClasses { get; set; }

        public Employee() { }

        public Employee(EmployeeVM employeeVM, string createdby)
        {
            id = employeeVM.Id;
            first_name = employeeVM.FirstName;
            last_name = employeeVM.LastName;
            phone = employeeVM.Phone;
            email = employeeVM.Email;
            hiring_location = employeeVM.Hiring_Location;
            create_date = DateTime.Now.ToLocalTime();
            //CreatedBy = createdby;
            is_deleted = false;
        }

        public virtual void Update(EmployeeVM employeeVM, string updatedby)
        {
            id = employeeVM.Id;
            first_name = employeeVM.FirstName;
            last_name = employeeVM.LastName;
            phone = employeeVM.Phone;
            email = employeeVM.Email;
            hiring_location = employeeVM.Hiring_Location;
            //Update_Date = DateTime.Now.ToLocalTime();
            //UpdatedBy = updatedby;
        }

        public virtual void Delete(string deletedby)
        {
            //Delete_Date = DateTime.Now.ToLocalTime();
            //DeletedBy = deletedby;
            is_deleted = true;
        }
    }
}
