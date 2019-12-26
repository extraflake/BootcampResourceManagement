using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ISubDistrictRepository
    {
        List<Subdistrict> Get();
        Subdistrict Get(int id);
        List<Subdistrict> GetParam(int param);
    }
}
