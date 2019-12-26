using Data.Base;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_batch")]
    public class Batch 
    {
        public string Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        public Batch() { }

        public Batch(BatchVM batchVM, string createdby)
        {
            Id = batchVM.Id;
            Start_Date = batchVM.StartDate;
            End_Date = batchVM.EndDate;
        }

        public virtual void Update(BatchVM batchVM, string updatedby)
        {
            Start_Date = batchVM.StartDate;
            End_Date = batchVM.EndDate;
        }
    }
}
