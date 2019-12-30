using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ParticipantVM
    {
        public string id { get; set; }
        public string name { get; set; }
        public string grade { get; set; }
        public bool is_deleted { get; set; }
        public string batch_class { get; set; }
    }
}
