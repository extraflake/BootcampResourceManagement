using System;
using Data.Models;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class UserCredentialVM
    {
        public string Role { get; set; }
        [Key]
        public string Email { get; set; }
        public string RoleDescription { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
