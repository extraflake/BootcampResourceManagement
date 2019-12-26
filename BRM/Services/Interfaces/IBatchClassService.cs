using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IBatchClassService
    {
        IEnumerable<BatchClassDisplayVM> Get();
        BatchClass Get(string id);
        bool Insert(BatchClassVM batchClassVM);
        bool Update(string id, BatchClassVM batchClassVM);
        bool Delete(string id);
    }
}
