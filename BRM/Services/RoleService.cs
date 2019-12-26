using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class RoleService : IRoleService
    {
        bool status = false;

        private IRoleRepository _roleRepository; 

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return _roleRepository.Delete(id);
            }
        }

        public List<Role> Get()
        {
            return _roleRepository.Get();
        }

        public Role Get(int id)
        {
            return _roleRepository.Get(id);
        }

        public bool Insert(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.name))
            {
                return status;
            }
            else
            {
                return _roleRepository.Insert(role);
            }
        }

        public bool Update(int id, Role role)
        {
            if (string.IsNullOrWhiteSpace(role.name) ||
                string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return _roleRepository.Update(id, role);
            }
        }
    }
}
