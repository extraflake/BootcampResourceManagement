using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Base;
using Data.ViewModels;

namespace Data.Models
{
    [Table("tb_m_type")]
    public class Type
    {
        public string id { get; set; }
        public string name { get; set; }

        public Type() { }

        public Type(TypeVM typeVM)
        {
            this.id = typeVM.id;
            this.name = typeVM.name;
        }
    }
}
