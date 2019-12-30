using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class BeforeInsertInterviewHistoryVM
    {
        public int id { get; set; }
        public string nik { get; set; }
        public List<string> employee { get; set; }
        public DateTime? interview_datetime { get; set; }
        public string pic { get; set; }
        public string note { get; set; }
        public string department { get; set; }
        public string customer { get; set; }
        public string create_by { get; set; }
        public DateTime? create_datetime { get; set; }
        public string update_by { get; set; }
        public DateTime? update_datetime { get; set; }
    }
}
