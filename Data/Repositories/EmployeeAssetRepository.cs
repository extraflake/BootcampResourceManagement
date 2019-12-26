using System;
using System.Collections.Generic;
using System.Linq;
using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;

namespace Data.Repositories
{
    public class EmployeeAssetRepository : IEmployeeAssetRepository
    {
        MyContext myContext = new MyContext();
        List<EmployeeAsset> resultList = null;
        IEnumerable<EmployeeAssetVM> resultListVM = null;
        EmployeeAsset result = null;
        bool status = false;

        public List<EmployeeAsset> Get()
        {
            resultList = myContext.EmployeeAssets.ToList();
            return resultList;
        }

        public IEnumerable<EmployeeAssetVM> Gets()
        {
            resultListVM = myContext.EmployeeAssetVMs.FromSql($"call sp_employee_assets()").ToList();
            return resultListVM;
        }

        public EmployeeAsset Get(int id)
        {
            result = myContext.EmployeeAssets.Find(id);
            return result;
        }

        public bool Insert(InsertEmployeeAssetVM insertEmployeeAssetVM)
        {
            try
            {
                var push = new EmployeeAsset(insertEmployeeAssetVM, "1");
                myContext.EmployeeAssets.Add(push);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return status;
        }
    }
}