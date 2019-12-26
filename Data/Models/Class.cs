using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_class")]
    public class Class
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
