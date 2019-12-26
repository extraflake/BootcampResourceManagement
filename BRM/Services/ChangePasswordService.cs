using BRM.Services.Interfaces;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        IChangePasswordRepository changePasswordRepository;

        public ChangePasswordService(IChangePasswordRepository changePasswordRepository)
        {
            this.changePasswordRepository = changePasswordRepository;
        }

        public ChangePasswordService() { }

        public GetTokenByEmailVM Get(string email)
        {
            return changePasswordRepository.Get(email);
        }
    }
}
