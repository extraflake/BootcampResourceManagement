using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_account")]
    public class Account /*: BaseModel*/
    {
        public string id { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string security_question { get; set; }
        public string security_answer { get; set; }
        public int status { get; set; }
    }
}
