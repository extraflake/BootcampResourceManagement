using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class ReportBootcampQuantityVM
    {
        [Key]
        public string label { get; set; }
        public long value { get; set; }
    }
}
