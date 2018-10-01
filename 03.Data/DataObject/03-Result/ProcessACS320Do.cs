using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class ACS320_Process
    {
        public decimal ActMOH { get; set; }
        public decimal ActCapaUsed { get; set; }
        public decimal ActCostRate { get; set; }
        public decimal SoldLiter { get; set; }
        public decimal EndingLiter { get; set; }
        public decimal SoldLiterOEM { get; set; }
        public decimal EndingLiterOEM { get; set; }
    }
}
