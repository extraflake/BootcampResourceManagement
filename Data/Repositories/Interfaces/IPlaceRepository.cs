using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IPlaceRepository
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
