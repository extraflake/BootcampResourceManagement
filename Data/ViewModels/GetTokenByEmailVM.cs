using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class GetTokenByEmailVM
    {
        [Key]
        public string token { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
