using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly MyContext _myContext;
        List<District> resultList = null;
        District result = null;

        public DistrictRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<District> Get()
        {
            resultList = _myContext.Districts.ToList();
            return resultList;
        }

        public District Get(int id)
        {
            result = _myContext.Districts.FirstOrDefault(x => x.id == id);
            return result;
        }

        public List<District> GetParam(int param)
        {
            resultList = _myContext.Districts.Where(x => x.province == param).ToList();
            return resultList;
        }
    }
}
