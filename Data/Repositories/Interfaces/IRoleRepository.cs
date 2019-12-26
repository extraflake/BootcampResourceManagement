using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> Get();
        Role Get(int id);
        bool Insert(Role role);
        bool Update(int id, Role role);
        bool Delete(int id);
    }
}
