using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Hiring_Location { get; set; }
        public int Birth_Place { get; set; }
        public string Phone { get; set; }
        public int Religion { get; set; }
        public int Village { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public string DeletedBy { get; set; }
    }
}
