using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_participant")]
    public class Participant
    {
        public string id { get; set; }
        public string grade { get; set; }
        public bool is_deleted { get; set; }
        public string batch_class { get; set; }
    }
}
