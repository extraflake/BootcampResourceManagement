using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Base;

namespace Data.Models
{
    [Table("tb_m_asset")]
    public class Asset
    {
        public string id { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string employee { get; set; }
        public DateTime create_date { get; set; }

        public Asset() { }

        public Asset(string id, string number, string type, string employee)
        {
            this.id = id;
            this.number = number;
            this.type = type;
            this.employee = employee;
            this.create_date = DateTime.Now.ToLocalTime();
        }
    }
}
