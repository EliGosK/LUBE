using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public partial class tb_ProcessControl
    {
        public int Year { get; set; }
        public int Period { get; set; }

        public string RetrieveStatus { get; set; }
        public string CalculateStatus { get; set; }
        public string TransferStatus { get; set; }

        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
