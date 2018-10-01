using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public  partial class tb_Transfer
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime? TransferDate { get; set; }
        public decimal? ActualCapacity { get; set; }
        public decimal? ActualMOH { get; set; }
        public decimal? ActualMOHRate { get; set; }
        public decimal? PlannedMOH { get; set; }
        public decimal? MOHVariance { get; set; }
        public decimal? BudgetVariance { get; set; }
        public decimal? CapacityVariance { get; set; }
        public bool? BudgetAdvantage { get; set; }
        public bool? CapacityAdvantage { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
