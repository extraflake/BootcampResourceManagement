using Data.Context;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ChangePasswordRepository : IChangePasswordRepository
    {
        MyContext myContext = new MyContext();

        public GetTokenByEmailVM Get(string email)
        {
            var get = myContext.GetTokenByEmailVM.FromSql($"call sp_retrieve_token_by_email({email})").SingleOrDefault();
            return get;
        }
    }
}
