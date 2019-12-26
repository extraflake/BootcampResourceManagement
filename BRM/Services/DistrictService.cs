using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class DistrictService : IDistrictService
    {
        private IDistrictRepository _districtRepository;

        public DistrictService(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public List<District> Get()
        {
            var result = _districtRepository.Get();
            return result;
        }

        public District Get(int id)
        {
            var result = _districtRepository.Get(id);
            return result;
        }

        public List<District> GetParam(int param)
        {
            var result = _districtRepository.GetParam(param);
            return result;
        }
    }
}
