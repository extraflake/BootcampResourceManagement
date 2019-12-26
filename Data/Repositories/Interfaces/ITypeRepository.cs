using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        List<Models.Type> Get();
        Models.Type Get(string id);
        Models.Type GetCount(string param);
        int Push(TypeVM typeVM);
    }
}
