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
    public class BatchRepository : IBatchRepository
    {
        private readonly MyContext _myContext;
        bool status = false;
        IEnumerable<Batch> resultList = null;
        Batch result = null;

        public BatchRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool Delete(string id, BatchVM batchVM)
        {
            var get = Get(id);
            _myContext.Batches.Remove(get);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }

        public IEnumerable<Batch> Get()
        {
            resultList = _myContext.Batches.ToList().OrderByDescending(x => x.Id);
            return resultList;
        }

        public Batch Get(string id)
        {
            result = _myContext.Batches.SingleOrDefault(x => x.Id == id);
            return result;
        }

        public bool Insert(BatchVM batchVM)
        {
            var get = Get(batchVM.Id);
            if (get != null)
            {
                return false;
            }
            else
            {
                var push = new Batch(batchVM, "1");
                _myContext.Batches.Add(push);
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                }
            }
            return status;

        }

        public bool Update(string id, BatchVM batchVM)
        {
            var pull = Get(id);
            pull.Update(batchVM, "1");
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }
    }
}
