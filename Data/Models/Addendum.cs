using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_addendum")]
    public class Addendum
    {
        [Key]
        public string id { get; set; }
        public DateTime start_contract { get; set; }
        public DateTime end_contract { get; set; }
        public DateTime addendum1 { get; set; }
        public DateTime addendum2 { get; set; }
    }
}
