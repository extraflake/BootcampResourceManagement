using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_village")]
    public class Village
    {
        public int id { get; set; }
        public string name { get; set; }
        public string postal_code { get; set; }
        public int sub_district { get; set; }
    }
}
