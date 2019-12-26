using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class ProvinceService : IProvinceService
    {
        private IProvinceRepository _provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public List<Province> Get()
        {
            var result = _provinceRepository.Get();
            return result;
        }

        public Province Get(int id)
        {
            var result = _provinceRepository.Get(id);
            return result;
        }
    }
}
