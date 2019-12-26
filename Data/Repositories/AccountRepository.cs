using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext _myContext;
        UserCredentialVM result = null;
        Account resultAccount = null;
        bool status = false;

        public AccountRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public UserCredentialVM Get(string email, string password)
        {
            result = _myContext.UserCredentialVMs.FromSql($"call sp_retrieve_user_credential({email},{password})").FirstOrDefault();
            return result;
        }

        public UserCredentialVM Get(UserCredentialVM userCredentialVM)
        {
            result = _myContext.UserCredentialVMs.FromSql($"call sp_retrieve_user_credential({userCredentialVM.Email},{userCredentialVM.Password})").SingleOrDefault();
            return result;
        }

        public UserCredentialVM Gets(string email, string password)
        {
            result = _myContext.UserCredentialVMs.FromSql($"call sp_retrieve_user_credential({email},{password})").SingleOrDefault();
            return result;
        }

        public bool UpdateToken(string email, string defaultToken)
        {
            var employeAccount = _myContext.Employees.Join(
                _myContext.Accounts,
                employee => employee.id, account => account.id,
                (employee, account) => new { employee, account }).Where(z => z.employee.email == email).FirstOrDefault();
            if (employeAccount == null) return false;
            employeAccount.account.token = defaultToken;

            int result = _myContext.SaveChanges();
            var get = _myContext.Accounts.FirstOrDefault(x => x.token == defaultToken);
            Console.WriteLine("Update forgot password result = " + result);
            if (employeAccount.account.token == defaultToken)
            {
                status = result > 0;
            }
            return status;
        }

        public Account GetToken(string token)
        {
            resultAccount = _myContext.Accounts.FirstOrDefault(x => x.token == token);
            return resultAccount;
        }

        public bool UpdateChangePassword(string token, ChangePasswordVM changePasswordVM)
        {
            var get = _myContext.Accounts.Where(x => x.token == token).FirstOrDefault();
            if(get.password == changePasswordVM.oldpassword)
            {
                get.password = changePasswordVM.newpassword;
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                }
            }
            return status;
            
        }

        public bool UpdateForgotPassword(string email)
        {
            string defaultPassword = "mitrainformatika1";
            var employeAccount = _myContext.Employees.Join(
                _myContext.Accounts,
                employee => employee.id, account => account.id,
                (employee, account) => new { employee, account }).Where(z => z.employee.email == email).FirstOrDefault();
            //if user not found
            if (employeAccount == null) return false;

            //if password already the default password then return true
            //else try change password in database and get update result(if>0 means update success)
            employeAccount.account.password = defaultPassword;
            int result = _myContext.SaveChanges();
            Console.WriteLine("Update forgor password result = " + result);
            if (employeAccount.account.password == defaultPassword)
            {
                status = result > 0;
            }
            return status;
        }
    }
}
