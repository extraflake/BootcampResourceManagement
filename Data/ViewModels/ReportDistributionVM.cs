using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class ReportDistributionVM
    {
        [Key]
        public string label { get; set; }
        public string value { get; set; }
    }
}
