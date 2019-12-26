using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IProvinceRepository
    {
        List<Province> Get();
        Province Get(int id);
    }
}
