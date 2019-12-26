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

namespace Data.Repositories
{
    public class BatchClassRepository : IBatchClassRepository
    {
        private readonly MyContext _myContext;
        IEnumerable<BatchClassDisplayVM> resultBatchClass = null;
        BatchClass result = null;
        bool status = false;

        public BatchClassRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool Delete(string id)
        {
            SshClient client = new SshClient("116.254.101.228", 1282, "admin0", "M1i_5erV3r2-:D204");
            client.Connect();
            if (client.IsConnected)
            {
                var portForwarded = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                client.AddForwardedPort(portForwarded);
                portForwarded.Start();
                var get = Get(id);
                _myContext.BatchClasses.Remove(get);
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                }
            }
            client.Disconnect();
            return status;

        }

        public IEnumerable<BatchClassDisplayVM> Get()
        {
            resultBatchClass = _myContext.BatchClassDisplayVMs.FromSql($"call sp_retrieve_batch_class").ToList().OrderBy(x => x.create_date);
            return resultBatchClass;
        }

        public BatchClass Get(string id)
        {
            result = _myContext.BatchClasses.FromSql($"call sp_retrieve_batch_class_by_id({id})").FirstOrDefault();
            return result;
        }

        public bool Insert(BatchClassVM batchClassVM)
        {
            //var checkBatchClass = _myContext.BatchClasses.Where(x => (x.id == batchClassVM.id) &&
            //(x.room == batchClassVM.room && x.trainer == batchClassVM.trainer && x.@class == batchClassVM.@class)).FirstOrDefault();
            //if (checkBatchClass == null)
            //{
            var push = new BatchClass(batchClassVM, "1");
            _myContext.BatchClasses.Add(push);
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            //}
            //else
            //{
            //    return false;
            //}
            return status;
        }

        public bool Update(string id, BatchClassVM batchClassVM)
        {
            var get = Get(id);
            get.Update(batchClassVM, "1");
            _myContext.Entry(get).State = EntityState.Modified;
            var result = _myContext.SaveChanges();
            return result > 0;
        }
    }
}