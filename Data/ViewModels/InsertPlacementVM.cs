using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class InsertPlacementVM
    {
        public int id { get; set; }
        public string employee { get; set; }
        public string relation_phone { get; set; }
        public DateTime start_date { get; set; }
        public string department { get; set; }
        public string customer { get; set; }
        public string relation_name { get; set; }
        public string notes { get; set; }
        public string created_by { get; set; }
    }
}
