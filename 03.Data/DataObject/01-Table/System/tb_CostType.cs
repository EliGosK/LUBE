using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class tb_CostType
    {
        public string CostTypeCode { get; set; }
        public string Description { get; set; }
        public string CostGroup { get; set; }
        public int Process { get; set; }
        public int Active { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
