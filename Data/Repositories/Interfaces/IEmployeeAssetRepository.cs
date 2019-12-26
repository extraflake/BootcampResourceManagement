using System.Collections.Generic;
using Data.Models;
using Data.ViewModel;
using Data.ViewModels;

namespace Data.Repositories.Interfaces
{
    public interface IEmployeeAssetRepository
    {
        List<EmployeeAsset> Get();
        IEnumerable<EmployeeAssetVM> Gets();
        EmployeeAsset Get(int id);
        bool Insert(InsertEmployeeAssetVM insertEmployeeAssetVM);
        //bool Update(int id, EmployeeAssetVM employeeAssetVM);
    }
}