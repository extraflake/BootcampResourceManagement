using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IProvinceService
    {
        List<Province> Get();
        Province Get(int id);
    }
}
