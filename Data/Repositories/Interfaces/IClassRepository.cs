using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IClassRepository
    {
        List<Class> Get();
        Class Get(int id);
    }
}
