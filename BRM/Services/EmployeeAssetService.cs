using System.Collections.Generic;
using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModel;
using Data.ViewModels;

namespace BRM.Services
{
    public class EmployeeAssetService : IEmployeeAssetService
    {
        bool status = false;

        private IEmployeeAssetRepository _employeeAssetRepository;

        public EmployeeAssetService(IEmployeeAssetRepository employeeAssetRepository)
        {
            _employeeAssetRepository = employeeAssetRepository;
        }

        public List<EmployeeAsset> Get()
        {
            return _employeeAssetRepository.Get();
        }

        public IEnumerable<EmployeeAssetVM> Gets()
        {
            return _employeeAssetRepository.Gets();
        }

        public EmployeeAsset Get(int id)
        {
            return _employeeAssetRepository.Get(id);
        }

        public bool Insert(InsertEmployeeAssetVM insertEmployeeAssetVM)
        {
            if (string.IsNullOrWhiteSpace(insertEmployeeAssetVM.id.ToString()))
            {
                return status;
            }
            else
            {
                return _employeeAssetRepository.Insert(insertEmployeeAssetVM);
            }
        }

        //public bool Update(int id, EmployeeAssetVM employeeAssetVM)
        //{
        //    if (string.IsNullOrWhiteSpace(employeeAssetVM.Id.ToString()) || string.IsNullOrWhiteSpace(employeeAssetVM.Note))
        //    {
        //        return status;
        //    }
        //    else
        //    {
        //        return _employeeAssetRepository.Update(id, employeeAssetVM);
        //    }
        //}
    }
}