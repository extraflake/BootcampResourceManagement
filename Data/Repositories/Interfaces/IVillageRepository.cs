using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IVillageRepository
    {
        List<Village> Get();
        Village Get(int id);
    }
}
