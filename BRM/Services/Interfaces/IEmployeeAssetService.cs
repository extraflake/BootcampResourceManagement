using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModel;
using Data.ViewModels;

namespace BRM.Services.Interfaces
{
    public interface IEmployeeAssetService
    {
        List<EmployeeAsset> Get();
        IEnumerable<EmployeeAssetVM> Gets();
        EmployeeAsset Get(int id);
        bool Insert(InsertEmployeeAssetVM insertEmployeeAssetVM);
        //bool Update(int id, EmployeeAssetVM employeeAssetVM);
    }
}
