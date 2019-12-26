using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_parameter")]
    public class Parameter
    {
        public string id { get; set; }
        public string value { get; set; }
        public string note { get; set; }
    }
}
