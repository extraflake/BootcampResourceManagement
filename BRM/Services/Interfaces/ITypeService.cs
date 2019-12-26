using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface ITypeService
    {
        List<Data.Models.Type> Get();
        Data.Models.Type Get(string id);
        Data.Models.Type GetCount(string param);
        int Push(TypeVM typeVM);
    }
}
