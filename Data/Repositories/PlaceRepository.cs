using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private MyContext _myContext;

        public PlaceRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool FinishDate(int id, Placement placement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlacementVM> GetAll()
        {
            var get = _myContext.PlacementVMs.FromSql($"call sp_retrieve_placement").ToList().OrderByDescending(y => y.employee);
            return get;
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
