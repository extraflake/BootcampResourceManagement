using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_customer")]
    public class Customer 
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string relation_manager { get; set; }
        public int district { get; set; }

        public Customer() { }

        public Customer(string id, string name, string address, string phone, string relation_manager, int district)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.relation_manager = relation_manager;
            this.district = Convert.ToInt32(district);
        }

        public void Update(string id, string name, string address, string phone, string relation_manager, int district)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.relation_manager = relation_manager;
            this.district = Convert.ToInt32(district);
        }
    }
}
