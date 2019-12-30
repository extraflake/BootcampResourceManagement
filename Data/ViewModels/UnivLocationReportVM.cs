using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class UnivLocationReportVM
    {
        [Key]
        public string Location { get; set; }
        public long Quantity { get; set; }
    }
}
