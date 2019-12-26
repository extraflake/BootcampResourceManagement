using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }

        public Role() { }

        public Role(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public virtual void Update(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
