using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Data
{
    public class ACS350_ImportData
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public decimal ActCapaUsed { get; set; }
        public decimal EndingLiter { get; set; }
        public decimal UnSoldBudgetAll { get; set; }
        public decimal UnSoldCapaAll { get; set; }
        public decimal AdjInv { get; set; }
        public DateTime PostingDate { get; set; }
        public string BatchNo { get; set; }
        public string ItemCode { get; set; }
        public string BOM3 { get; set; }
        public string DocumentNo { get; set; }
        public string BaseRef { get; set; }
        public decimal Quantity { get; set; }
        public decimal QTYPPKG { get; set; }
        public decimal Liter { get; set; }
        public DateTime DueDate { get; set; }
        public string Lot1 { get; set; }
        public string Ref2 { get; set; }
        public decimal RM_Cost_B1_Display { get; set; }
        public decimal StdOH { get; set; }
        public decimal Variance_BOM1 { get; set; }
        public decimal BOM1Cost { get; set; }
        public decimal RM_Cost_Cal { get; set; }
        public decimal ContainerCost { get; set; }
        public decimal LabelCost { get; set; }
        public decimal ActualCostFG { get; set; }
        public decimal Variance_BOM2 { get; set; }
        public decimal ReceiptQty { get; set; }
        public decimal CalPrice { get; set; }
        public decimal TransValue { get; set; }
        public string WhsCode { get; set; }
        public decimal SoldQuantity { get; set; }
        public decimal SoldLiter { get; set; }
        public decimal EndingBalQuantity { get; set; }
        public decimal EndingBalLiter { get; set; }
        public decimal Uprice { get; set; }
        public decimal EndingBalAmount { get; set; }
        public decimal COGS { get; set; }
        public decimal Effect { get; set; }
        public decimal NewAmount { get; set; }
        public decimal NewPrice { get; set; }
        public decimal DisAssemQty { get; set; } //Modified by Pachara S.
    }
}
