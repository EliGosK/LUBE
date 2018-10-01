using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public partial class tb_ActualProduction
    {
        public int DocEntry { get; set; }
        public int? DocNum { get; set; }
        public int? Series { get; set; }
        public string ItemCode { get; set; }
        public string ProductionType { get; set; }
        public decimal? PlannedQty { get; set; }
        public decimal? CompleteQty { get; set; }
        public decimal? RejectQty { get; set; }
        public DateTime? OrderDate { get; set; }
        public string UOM { get; set; }
        public string BatchNo { get; set; }
        public int? Year { get; set; }
        public int? Period { get; set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
