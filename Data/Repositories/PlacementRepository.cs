using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PlacementRepository : IPlacementRepository
    {
        private readonly MyContext _myContext;
        IEnumerable<PlacementVM> resultList = null;
        Placement result = null;
        PlacementVM resultVM = null;
        bool status = false;

        public PlacementRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<PlacementVM> GetAll()
        {
            resultList = _myContext.PlacementVMs.FromSql($"call sp_retrieve_placement").ToList().OrderByDescending(y => y.employee).OrderByDescending(x => x.start_date);
            return resultList;
        }

        public Placement GetId(int id)
        {
            result = _myContext.Placements.FirstOrDefault(x => x.id == id);
            return result;
        }

        public PlacementVM GetIdVM(int id)
        {
            resultVM = _myContext.PlacementVMs.FromSql($"call sp_retrieve_placement").FirstOrDefault(x => x.id == id);
            return resultVM;
        }

        public IEnumerable<PlacementVM> GetRmandGetCus(PlacementParamVM placementParamVM)
        {
            resultList = _myContext.PlacementVMs.FromSql($"call sp_retrieve_placement_byrm_bycus({placementParamVM.relation_manager},{placementParamVM.customer})").ToList();
            return resultList;
        }

        public bool Insert(InsertPlacementVM insertPlacementVM)
        {
            var placement = new Placement()
            {
                employee = insertPlacementVM.employee,
                start_date = insertPlacementVM.start_date,
                create_date = DateTime.Now.ToLocalTime(),
                department = insertPlacementVM.department,
                customer = insertPlacementVM.customer,
                notes = insertPlacementVM.notes
            };
            _myContext.Placements.Add(placement);
            try
            {
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                
            }
            return status;
        }

        public bool Update(int id, Placement placement)
        {
            var get = GetId(id);
            get.Update(placement, placement.updated_by);
            _myContext.Entry(get).State = EntityState.Modified;
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public bool Delete(int id)
        {
            var get = GetId(id);
            _myContext.Placements.Remove(get);
            //get.Delete();
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }

        public bool FinishDate(int id, Placement placement)
        {
            var get = GetId(id);
            get.FinishDate(placement);
            _myContext.Entry(get).State = EntityState.Modified;
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }
    }
}
