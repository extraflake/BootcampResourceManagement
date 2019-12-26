using Data.Base;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("tb_m_batch_class")]
    public class BatchClass
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public int plan { get; set; }
        public int @class { get; set; }
        public string batch { get; set; }
        public string room { get; set; }
        public string trainer { get; set; }
        public DateTime? create_date { get; set; }

        [ForeignKey("trainer")]
        public virtual Employee Employee { get; set; }

        public BatchClass() { }

        public BatchClass(BatchClassVM batchClassVM, string createdby)
        {
            id = batchClassVM.id;
            trainer = batchClassVM.trainer.ToString();
            batch = batchClassVM.batch;
            plan = 10;
            @class = batchClassVM.@class;
            room = batchClassVM.room;
            create_date = DateTime.Now.ToLocalTime();
        }

        public virtual void Update(BatchClassVM batchClassVM, string updatedby)
        {
            trainer = batchClassVM.trainer.ToString();
            batch = batchClassVM.batch;
            @class = batchClassVM.@class;
            room = batchClassVM.room;
        }
    }
}
