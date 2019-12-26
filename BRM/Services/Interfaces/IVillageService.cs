using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IVillageService
    {
        List<Village> Get();
        Village Get(int id);
    }
}
