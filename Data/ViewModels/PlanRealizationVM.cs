using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class PlanRealizationVM
    {
        [Key]
        public string name { get; set; }
        public int quantity { get; set; }
        public int plan { get; set; }
        public string start_date { get; set; }
    }
}
