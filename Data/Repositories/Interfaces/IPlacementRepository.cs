using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IPlacementRepository
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
