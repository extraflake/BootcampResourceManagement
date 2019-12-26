using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IBatchClassRepository
    {
        IEnumerable<BatchClassDisplayVM> Get();
        BatchClass Get(string id);
        bool Insert(BatchClassVM batchClassVM);
        bool Update(string id, BatchClassVM batchClassVM);
        bool Delete(string id);
    }
}
