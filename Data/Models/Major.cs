using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_major")]
    public class Major
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
    }
}
