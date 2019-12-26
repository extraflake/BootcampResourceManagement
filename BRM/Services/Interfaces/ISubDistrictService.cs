using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface ISubDistrictService
    {
        List<Subdistrict> Get();
        Subdistrict Get(int id);
        List<Subdistrict> GetParam(int param);
    }
}
