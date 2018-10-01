using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Data
{
    public class SmtpInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Enabled { get; set; }
        public bool EnableSSL { get; set; }

        public bool UseDefaultCredential { get; set; }
        public string CredentialAccount { get; set; }
        public string CredentialPassword { get; set; }

        public string FromEmail { get; set; }
    }
}
