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
    public class ClassRepository : IClassRepository
    {
        private readonly MyContext _myContext;
        List<Class> resultList = null;
        Class result = null;

        public ClassRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Class> Get()
        {
            resultList = _myContext.Classes.ToList();
            return resultList;
        }

        public Class Get(int id)
        {
            result = _myContext.Classes.FirstOrDefault(x => x.id == id);
            return result;
        }
    }
}
