using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IBatchRepository
    {
        IEnumerable<Batch> Get();
        Batch Get(string id);
        bool Insert(BatchVM batchVM);
        bool Update(string id, BatchVM batchVM);
        bool Delete(string id, BatchVM batchVM);
    }
}
