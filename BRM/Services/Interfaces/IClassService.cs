using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IClassService
    {
        List<Class> Get();
        Class Get(int id);
    }
}
