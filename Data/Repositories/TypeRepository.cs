using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly MyContext _myContext;
        List<Models.Type> resultList = null;
        Models.Type result = null;

        public TypeRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Models.Type> Get()
        {
            resultList = _myContext.Types.ToList();
            return resultList;
        }

        public Models.Type Get(string id)
        {
            result = _myContext.Types.Find(id);
            return result;
        }

        public Models.Type GetCount(string param)
        {
            result = _myContext.Types.Find(param);
            return result;
        }

        public int Push(TypeVM typeVM)
        {
            var push = new Data.Models.Type(typeVM);
            _myContext.Types.Add(push);
            var result = _myContext.SaveChanges();
            return result;
        }
    }
}
