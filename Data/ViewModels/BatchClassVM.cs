using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class BatchClassVM
    {
        public string id { get; set; }
        public string trainer { get; set; }
        public string batch { get; set; }
        public int @class { get; set; }
        public string room { get; set; }
    }
}
