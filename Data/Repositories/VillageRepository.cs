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
    public class VillageRepository : IVillageRepository
    {
        private readonly MyContext _myContext;
        List<Village> resultList = null;
        Village result = null;

        public VillageRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Village> Get()
        {
            resultList = _myContext.Villages.ToList();
            return resultList;
        }

        public Village Get(int id)
        {
            result = _myContext.Villages.Include("district").FirstOrDefault(x => x.id == id);
            return result;
        }
    }
}
