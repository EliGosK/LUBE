using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class tb_SecurityMatch
    {
        public int SecurityID { get; set; }
        public string UserID { get; set; }
        public int GroupID { get; set; }
        public int ScreenID { get; set; }
        public string Permission { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
