using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IPlaceService
    {
        IEnumerable<PlacementVM> GetAll();
        List<Placement> GetRmandGetCus(PlacementParamVM placementParamVM);
        Placement GetId(int id);
        PlacementVM GetIdVM(int id);
        bool Insert(InsertPlacementVM insertPlacementVM);
        bool Update(int id, Placement placement);
        bool FinishDate(int id, Placement placement);
    }
}
