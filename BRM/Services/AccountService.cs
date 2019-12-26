using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class AccountService : IAccountService
    {
        bool status = false;

        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }

        public UserCredentialVM Get(UserCredentialVM userCredentialVM)
        {
            return _accountRepository.Get(userCredentialVM);
        }

        public UserCredentialVM Gets(string email, string password)
        {
            return _accountRepository.Gets(email, password);
        }

        public Account getToken(string token)
        {
            return _accountRepository.GetToken(token);
        }

        public bool UpdateChangePassword(string id, ChangePasswordVM changePasswordVM)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()) ||
                string.IsNullOrWhiteSpace(changePasswordVM.oldpassword)
               )
            {
                return status;
            }
            else
            {
                return _accountRepository.UpdateChangePassword(id, changePasswordVM);
            }
        }
    }
}
