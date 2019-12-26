using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IDistrictService
    {
        List<District> Get();
        District Get(int id);
        List<District> GetParam(int param);
    }
}
