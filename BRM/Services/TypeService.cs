using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class TypeService : ITypeService
    {
        bool status = false;

        private ITypeRepository _typeRepository;

        public TypeService(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public List<Data.Models.Type> Get()
        {
            return _typeRepository.Get();
        }

        public Data.Models.Type Get(string id)
        {
            return _typeRepository.Get(id);
        }

        public Data.Models.Type GetCount(string param)
        {
            return _typeRepository.GetCount(param);
        }

        public int Push(TypeVM typeVM)
        {
            return _typeRepository.Push(typeVM);
        }
    }
}
