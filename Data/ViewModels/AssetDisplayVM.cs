using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class AssetDisplayVM
    {
        public string id { get; set; }
        public string nik { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string employee { get; set; }
        public DateTime create_date { get; set; }
    }
}
