using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class BatchClassDisplayVM
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string batch { get; set; }
        public string @class { get; set; }
        public string room { get; set; }
        public string trainer { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? start_date { get; set; }
    }
}
