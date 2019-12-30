using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class InsertEmployeeRoleVM
    {
        public int id { get; set; }
        public List<int> role { get; set; }
        public string nik { get; set; }
    }
}
