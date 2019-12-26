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
    public class BatchClassService : IBatchClassService
    {
        bool status = false;

        private IBatchClassRepository _batchClassRepository;

        public BatchClassService(IBatchClassRepository batchClassRepository)
        {
            _batchClassRepository = batchClassRepository;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return status;
            }
            else
            {
                return _batchClassRepository.Delete(id);
            }
        }

        public IEnumerable<BatchClassDisplayVM> Get()
        {
            return _batchClassRepository.Get();
        }

        public BatchClass Get(string id)
        {
            return _batchClassRepository.Get(id);
        }

        public bool Insert(BatchClassVM batchClassVM)
        {
            if (string.IsNullOrWhiteSpace(batchClassVM.batch.ToString()) ||
                string.IsNullOrWhiteSpace(batchClassVM.@class.ToString()) ||
                string.IsNullOrWhiteSpace(batchClassVM.room.ToString()) ||
                string.IsNullOrWhiteSpace(batchClassVM.trainer.ToString()))
            {
                return status;
            }
            else
            {
                return _batchClassRepository.Insert(batchClassVM);
            }
        }

        public bool Update(string id, BatchClassVM batchClassVM)
        {
            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(batchClassVM.batch.ToString()) ||
                string.IsNullOrWhiteSpace(batchClassVM.@class.ToString()) ||
                string.IsNullOrWhiteSpace(batchClassVM.room.ToString()) ||
                string.IsNullOrWhiteSpace(batchClassVM.trainer.ToString()))
            {
                return status;
            }
            else
            {
                return _batchClassRepository.Update(id, batchClassVM);
            }
        }
    }
}
