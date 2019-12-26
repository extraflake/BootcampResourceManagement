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
    public class BatchService : IBatchService
    {
        bool status = false;

        private IBatchRepository _batchRepository;

        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public bool Delete(string id, BatchVM batchVM)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return status;
            }
            return _batchRepository.Delete(id, batchVM);
        }

        public IEnumerable<Batch> Get()
        {
            return _batchRepository.Get();
        }

        public Batch Get(string id)
        {
            return _batchRepository.Get(id);
        }

        public bool Insert(BatchVM batchVM)
        {
            if (string.IsNullOrWhiteSpace(batchVM.Id) || 
                 string.IsNullOrWhiteSpace(batchVM.StartDate.ToString()) ||
                 string.IsNullOrWhiteSpace(batchVM.EndDate.ToString()))
            {
                return status;
            }
            else
            {
                return _batchRepository.Insert(batchVM);
            }
        }

        public bool Update(string id, BatchVM batchVM)
        {
            if (string.IsNullOrWhiteSpace(id) ||
                 string.IsNullOrWhiteSpace(batchVM.StartDate.ToString()) ||
                 string.IsNullOrWhiteSpace(batchVM.EndDate.ToString()))
            {
                return status;
            }
            else
            {
                return _batchRepository.Update(id, batchVM);
            }
        }
    }
}
