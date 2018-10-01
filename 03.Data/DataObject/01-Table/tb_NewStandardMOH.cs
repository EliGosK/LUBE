using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public partial class tb_NewStandardMOH
    {
        public int Year { get; set; }
        public decimal? TotalMOH { get; set; }
        public decimal? TotalCapacity { get; set; }
        public decimal? MOHRate { get; set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
