using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IAccountService
    {
        UserCredentialVM Get(UserCredentialVM userCredentialVM);
        UserCredentialVM Gets(string email, string password);
        Account getToken(string token);
        bool UpdateChangePassword(string token, ChangePasswordVM changePasswordVM);
    }
}
