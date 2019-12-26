using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_university")]
    public class University
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int district { get; set; }
    }
}
