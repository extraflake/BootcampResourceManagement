using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class DistributionReportVM
    {
        [Key]
        public string CustomerName { get; set; }
        public string Percentage { get; set; }
    }
}
