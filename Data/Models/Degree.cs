using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_degree")]
    public class Degree
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
