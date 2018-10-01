using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public partial class tb_ActualExpense
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string AccountCode { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? BalanceAmount { get; set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
