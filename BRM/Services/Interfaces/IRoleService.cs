using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IRoleService
    {
        List<Role> Get();
        Role Get(int id);
        bool Insert(Role role);
        bool Update(int id, Role role);
        bool Delete(int id);
    }
}
