using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class AssetCountVM
    {
        [Key]
        public int count { get; set; }
    }
}
