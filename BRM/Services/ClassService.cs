using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class ClassService : IClassService
    {
        private IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public List<Class> Get()
        {
            return _classRepository.Get();
        }

        public Class Get(int id)
        {
            return _classRepository.Get(id);
        }
    }
}
