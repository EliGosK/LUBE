using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NECT_EDI.SAP.DTO
{
    public class SAPConnection
    {
        public string DB_NAME { get; set; }
        public string DB_PASSWORD { get; set; }
        public string DB_USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string SERVER { get; set; }
        public string USERNAME { get; set; }
        public string LICENSE_SERVER { get; set; }
        public bool UseTrusted { get; set; }
    }
}
