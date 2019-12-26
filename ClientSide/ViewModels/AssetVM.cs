using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide.ViewModels
{
    public class AssetVM
    {
        public string id { get; set; }
        public string nik { get; set; }
        public string idtype { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string employee { get; set; }
        public DateTime create_date { get; set; }
    }
}
