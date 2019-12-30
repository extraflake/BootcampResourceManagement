using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ChangePasswordVM
    {
        public string token { get; set; }
        public string newpassword { get; set; }
        public string oldpassword { get; set; }
    }
}
