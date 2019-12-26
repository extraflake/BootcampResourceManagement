using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IChangePasswordService
    {
        GetTokenByEmailVM Get(string email);
    }
}
