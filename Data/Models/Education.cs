using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public string id { get; set; }
        public bool is_deleted { get; set; }
        public string degree { get; set; }
        public string major { get; set; }
        public string university { get; set; }
    }
}
