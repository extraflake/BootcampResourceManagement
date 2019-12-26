using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IPlacementService
    {
        IEnumerable<PlacementVM> GetAll();
        Placement GetId(int id);
        PlacementVM GetIdVM(int id);
        //List<PlacementVM> GetCus(PlacementParamVM placementParamVM);
        //List<PlacementVM> GetRm(PlacementParamVM placementParamVM);
        IEnumerable<PlacementVM> GetRmandGetCus(PlacementParamVM placementParamVM);
        //bool Insert(Placement placement);
        bool Insert(InsertPlacementVM insertPlacementVM);
        bool Update(int id, Placement placement);
        bool FinishDate(int id, Placement placement);
        bool Delete(int id);
    }
}
