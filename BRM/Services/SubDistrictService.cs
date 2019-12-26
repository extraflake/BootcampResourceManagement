using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class SubDistrictService : ISubDistrictService
    {
        private ISubDistrictRepository _subDistrictRepository;

        public SubDistrictService(ISubDistrictRepository subDistrictRepository)
        {
            _subDistrictRepository = subDistrictRepository;
        }

        public List<Subdistrict> Get()
        {
            var result = _subDistrictRepository.Get();
            return result;
        }

        public Subdistrict Get(int id)
        {
            var result = _subDistrictRepository.Get(id);
            return result;
        }

        public List<Subdistrict> GetParam(int param)
        {
            var result = _subDistrictRepository.GetParam(param);
            return result;
        }
    }
}
