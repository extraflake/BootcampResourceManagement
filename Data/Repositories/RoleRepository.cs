using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MyContext _myContext;
        bool status = false;
        List<Role> resultList = null;
        Role result = null;

        public RoleRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool Delete(int id)
        {
            var get = Get(id);
            _myContext.Roles.Remove(get);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }

        public List<Role> Get()
        {
            resultList = _myContext.Roles.ToList();
            return resultList;
        }

        public Role Get(int id)
        {
            result = _myContext.Roles.Find(id);
            return result;
        }

        public bool Insert(Role role)
        {
            var get = _myContext.Roles.Where(u => u.name == role.name).FirstOrDefault();
            if (get == null)
            {
                var push = new Role(role.id, role.name);
                _myContext.Roles.Add(push);
            }
            else
            {
                return false;
            }
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public bool Update(int id, Role role)
        {
            var pull = Get(id);
            pull.Update(id, role.name);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }
    }
}
