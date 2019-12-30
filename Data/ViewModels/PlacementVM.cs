using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class PlacementVM
    {
        public int id { get; set; }
        public string employee { get; set; }
        public string rm_phone { get; set; }
        public string name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime finish_date { get; set; }
        public string department { get; set; }
        public string customer { get; set; }
        public string customer_name { get; set; }
        public string rm_id { get; set; }
        public string relation_manager { get; set; }
        public string notes { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
    }
}
