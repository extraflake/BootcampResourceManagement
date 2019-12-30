using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class TopTenReportVM
    {
        [Key]
        public string CustomerName { get; set; }
        public long Quantity { get; set; }
    }
}
