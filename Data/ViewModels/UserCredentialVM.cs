using System;
using Data.Models;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class UserCredentialVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
