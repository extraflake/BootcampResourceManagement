using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IDistrictRepository
    {
        List<District> Get();
        District Get(int id);
        List<District> GetParam(int param);
    }
}
