using System;

namespace Data.ViewModel
{
    public class EmployeeAssetVM
    {
        public string Id { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Note { get; set; }
        public string Asset_Id { get; set; }
        public string Employee_Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string Asset { get; internal set; }
        public string Employee { get; internal set; }
    }
}