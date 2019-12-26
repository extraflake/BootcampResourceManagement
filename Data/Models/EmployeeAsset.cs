using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Base;
using Data.ViewModel;
using Data.ViewModels;

namespace Data.Models
{
    [Table("tb_tr_employee_asset")]
    public class EmployeeAsset
    {
        public string id { get; set; }
        public DateTime receive_date { get; set; }
        public DateTime return_date { get; set; }
        public string note { get; set; }
        public string asset { get; set; }
        public string employee { get; set; }

        public EmployeeAsset() { }

        public EmployeeAsset(InsertEmployeeAssetVM insertEmployeeAssetVM, string createdby)
        {
            this.id = insertEmployeeAssetVM.id;
            this.receive_date = DateTime.Now.ToLocalTime();
            this.asset = insertEmployeeAssetVM.asset;
            this.employee = insertEmployeeAssetVM.employee;
        }

        public void Update(int id, EmployeeAssetVM employeeAssetVM, string updatedby)
        {
            this.receive_date = employeeAssetVM.ReceiveDate;
            this.return_date = employeeAssetVM.ReturnDate;
            this.note = employeeAssetVM.Note;
            this.asset = employeeAssetVM.Asset_Id;
            this.employee = employeeAssetVM.Employee_Id;
        }
    }
}
