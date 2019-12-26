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
    public class PlaceService : IPlaceService
    {
        private IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placementRepository)
        {
            _placeRepository = placementRepository;
        }

        public bool FinishDate(int id, Placement placement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlacementVM> GetAll()
        {
            return _placeRepository.GetAll();
        }

        public Placement GetId(int id)
        {
            throw new NotImplementedException();
        }

        public PlacementVM GetIdVM(int id)
        {
            throw new NotImplementedException();
        }

        public List<Placement> GetRmandGetCus(PlacementParamVM placementParamVM)
        {
            throw new NotImplementedException();
        }

        public bool Insert(InsertPlacementVM insertPlacementVM)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Placement placement)
        {
            throw new NotImplementedException();
        }
    }
}
