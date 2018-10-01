using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public partial class tb_ActualProdDetail
    {
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public decimal? BaseQty { get; set; }
        public decimal? PlannedQty { get; set; }
        public decimal? IssueQty { get; set; }
        public decimal? AdditQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
