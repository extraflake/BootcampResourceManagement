using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        UserCredentialVM Get(string email, string password);
        bool UpdateForgotPassword(string email);
        bool UpdateChangePassword(string token, ChangePasswordVM changePasswordVM);
        Account GetToken(string token);
        bool UpdateToken(string email, string defaultToken);
        UserCredentialVM Get(UserCredentialVM userCredentialVM);
        UserCredentialVM Gets(string email, string password);
    }
}
