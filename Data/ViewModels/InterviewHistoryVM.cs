using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.ViewModel
{
    public class InterviewHistoryVM
    {
        [Key]
        public int id { get; set; }
        public string nik { get; set; }
        public string employee { get; set; }
        public DateTime? interview_datetime { get; set; }
        public string pic { get; set; }
        public string note { get; set; }
        public string customer { get; set; }
        public string department { get; set; }
        //public string create_by { get; set; }
        //public DateTime? create_datetime { get; set; }
        //public string InterviewTime { get; set; }
        //public string Interviewer { get; set; }
        //public string Customer { get; set; }
        //public string Employee { get; set; }
        //public string DeletedBy { get; set; }
        //public string update_by { get; set; }
        //public DateTime? update_datetime { get; set; }
    }
}
