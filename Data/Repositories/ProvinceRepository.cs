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
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly MyContext _myContext;
        List<Province> resultList = null;
        Province result = null;

        public ProvinceRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Province> Get()
        {
            resultList = _myContext.Provinces.ToList();
            return resultList;
        }

        public Province Get(int id)
        {
            result = _myContext.Provinces.FirstOrDefault(x => x.id == id);
            return result;
        }
    }
}
