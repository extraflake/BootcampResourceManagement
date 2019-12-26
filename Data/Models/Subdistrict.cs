using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_sub_district")]
    public class Subdistrict
    {
        public int id { get; set; }
        public string name { get; set; }
        public int district { get; set; }
    }
}
