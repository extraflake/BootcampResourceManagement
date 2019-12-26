using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_tr_employee_education")]
    public class EmployeeEducation
    {
        [Key]
        public string id { get; set; }
        public float gpa { get; set; }
        public DateTime start_year { get; set; }
        public DateTime end_year { get; set; }
        public string education { get; set; }
    }
}
