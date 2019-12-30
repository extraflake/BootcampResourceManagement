using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class PlanRealizationReportVM
    {
        [Key]
        public string Bootcamp { get; set; }
        public int Plan { get; set; }
        public int Realization { get; set; }
        public string Class { get; set; }
        public string Trainer { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
