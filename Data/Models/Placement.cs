using Data.Base;
using Data.Repositories;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_tr_placement")]
    public class Placement /*: BaseModel*/
    {
        public int id { get; set; }
        public string department { get; set; }
        public DateTime start_date { get; set; }
        public DateTime finish_date { get; set; }
        public string created_by { get; set; }
        public DateTime create_date { get; set; }
        public string updated_by { get; set; }
        public DateTime update_date { get; set; }
        public string notes { get; set; }
        public string employee { get; set; }
        public string customer { get; set; }

        public Placement() { }

        public Placement(Placement placement/*, string createdby*/)
        {
            department = placement.department;
            start_date = Convert.ToDateTime(placement.start_date);
            DateTime date = new DateTime(0001 - 01 - 01);
            finish_date = date;
            created_by = "14796";
            create_date = DateTime.Now.ToLocalTime();
            employee = placement.employee.ToString();
            customer = placement.customer;
            notes = placement.notes;
        }

        public virtual void Update(Placement placement, string updatedby)
        {
            department = placement.department;
            start_date = Convert.ToDateTime(placement.start_date);
            employee = placement.employee.ToString();
            customer = placement.customer;
            notes = placement.notes;
            update_date = DateTime.Now.ToLocalTime();
            updated_by = updatedby;
        }

        public virtual void FinishDate(Placement placement)
        {
            finish_date = Convert.ToDateTime(placement.finish_date);
            //update_date = DateTime.Now.ToLocalTime();
            //updated_by = updatedby;
        }

        //public virtual void Delete(/*[FromBody] string deletedby*/)
        //{
        //    Delete_Date = DateTime.Now.ToLocalTime();
        //    DeletedBy = deletedby;
        //    Is_Deleted = 1;
        //}
    }
}
