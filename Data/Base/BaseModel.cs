using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Base
{
    public class BaseModel
    {
        public DateTime Create_Date { get; set; }
        public string CreatedBy { get; set; }

        public DateTime Update_Date { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime Delete_Date { get; set; }
        public string DeletedBy { get; set; }

        public int Is_Deleted { get; set; }
    }
}
