using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("tb_m_district")]
    public class District
    {
        public int id { get; set; }
        public string kabcity { get; set; }
        public string name { get; set; }
        public int province { get; set; }
    }
}