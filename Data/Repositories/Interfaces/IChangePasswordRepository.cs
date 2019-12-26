using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IChangePasswordRepository
    {
        GetTokenByEmailVM Get(string email);
    }
}
