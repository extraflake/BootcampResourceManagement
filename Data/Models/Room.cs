using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_room")]
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
