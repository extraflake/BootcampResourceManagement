using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class VillageService : IVillageService
    {
        private IVillageRepository _villageRepository;

        public VillageService(IVillageRepository villageRepository)
        {
            _villageRepository = villageRepository;
        }

        public List<Village> Get()
        {
            var result = _villageRepository.Get();
            return result;
        }

        public Village Get(int id)
        {
            var result = _villageRepository.Get(id);
            return result;
        }
    }
}
