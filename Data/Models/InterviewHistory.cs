using Data.Base;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_tr_interview_history")]
    public class InterviewHistory /*: BaseModel*/
    {
        [Key]
        public int id { get; set; }
        public DateTime? interview_datetime { get; set; }
        public string pic { get; set; }
        public string note { get; set; }
        public string department { get; set; }
        public string created_by { get; set; }
        public DateTime? create_datetime { get; set; }
        public string updated_by { get; set; }
        public DateTime? update_datetime { get; set; }
        public string customer { get; set; }
        public string employee { get; set; }

        public InterviewHistory() { }

        public InterviewHistory(int id, DateTime? interview_datetime, string pic, string note, string department, string create_by, DateTime? create_datetime, string update_by, DateTime? update_datetime, string customer, string employee)
        {
            this.id = id;
            this.interview_datetime = interview_datetime;
            this.pic = pic;
            this.note = note;
            this.department = department;
            this.created_by = create_by;
            this.create_datetime = create_datetime;
            this.updated_by = update_by;
            this.update_datetime = update_datetime;
            this.customer = customer;
            this.employee = employee;
        }

        public void Update(int id, DateTime? interview_datetime, string pic, string note, string department, string create_by, DateTime? create_datetime, string update_by, DateTime? update_datetime, string customer, string employee)
        {
            this.id = id;
            this.interview_datetime = interview_datetime;
            this.pic = pic;
            this.note = note;
            this.department = department;
            this.created_by = create_by;
            this.create_datetime = create_datetime;
            this.updated_by = update_by;
            this.update_datetime = update_datetime;
            this.customer = customer;
            this.employee = employee;
        }
    }
}
