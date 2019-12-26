using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SubDistrictRepository : ISubDistrictRepository
    {
        private readonly MyContext _myContext;
        List<Subdistrict> resultList = null;
        Subdistrict result = null;

        public SubDistrictRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Subdistrict> Get()
        {
            resultList = _myContext.Subdistricts.Include("district").ToList();
            return resultList;
        }

        public Subdistrict Get(int id)
        {
            result = _myContext.Subdistricts.Include("district").FirstOrDefault(x => x.id == id);
            return result;
        }

        public List<Subdistrict> GetParam(int param)
        {
            resultList = _myContext.Subdistricts.Where(x => x.district == param).ToList();
            return resultList;
        }
    }
}
