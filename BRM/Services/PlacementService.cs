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
    public class PlacementService : IPlacementService
    {
        bool status = false;

        private IPlacementRepository _placementRepository;

        public PlacementService(IPlacementRepository placementRepository)
        {
            _placementRepository = placementRepository;
        }

        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return _placementRepository.Delete(id);
            }
        }

        public IEnumerable<PlacementVM> GetAll()
        {
            return _placementRepository.GetAll();
        }

        //public List<PlacementVM> GetCus(PlacementParamVM placementParamVM)
        //{
        //    return _placementRepository.GetCus(placementParamVM);
        //}

        //public List<PlacementVM> GetRm(PlacementParamVM placementParamVM)
        //{
        //    return _placementRepository.GetRm(placementParamVM);
        //}

        public Placement GetId(int id)
        {
            return _placementRepository.GetId(id);
        }

        public PlacementVM GetIdVM(int id)
        {
            return _placementRepository.GetIdVM(id);
        }

        public IEnumerable<PlacementVM> GetRmandGetCus(PlacementParamVM placementParamVM)
        {
            return _placementRepository.GetRmandGetCus(placementParamVM);
        }

        public bool Insert(InsertPlacementVM insertPlacementVM)
        {
            if (
                //string.IsNullOrWhiteSpace(insertPlacementVM.relation_name) ||
                //string.IsNullOrWhiteSpace(insertPlacementVM.relation_phone) ||
                string.IsNullOrWhiteSpace(insertPlacementVM.department) ||
                string.IsNullOrWhiteSpace(insertPlacementVM.start_date.ToString()) ||
                string.IsNullOrWhiteSpace(insertPlacementVM.employee.ToString()) ||
                string.IsNullOrWhiteSpace(insertPlacementVM.customer.ToString())

               )
            {
                return status;
            }
            else
            {
                return _placementRepository.Insert(insertPlacementVM);
            }


        }
        public bool FinishDate(int id, Placement placement)
        {
            if (string.IsNullOrWhiteSpace(placement.finish_date.ToString()))
            {
                return status;
            }
            else
            {
                return _placementRepository.FinishDate(id, placement);
            }
        }


        public bool Update(int id, Placement placement)
        {
            if (
                 string.IsNullOrWhiteSpace(placement.id.ToString()) ||
                 string.IsNullOrWhiteSpace(placement.department) ||
                 string.IsNullOrWhiteSpace(placement.start_date.ToString()) ||
                 string.IsNullOrWhiteSpace(placement.employee.ToString()) ||
                 string.IsNullOrWhiteSpace(placement.customer.ToString()) ||
                 string.IsNullOrWhiteSpace(placement.updated_by.ToString())

              )
            {
                return status;
            }
            else
            {
                return _placementRepository.Update(id, placement);
            }
        }
    }
}
