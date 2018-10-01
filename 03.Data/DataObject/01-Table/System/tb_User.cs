using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public partial class tb_User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }        
        public string Email { get; set; }   
        public string CompanyName { get; set; }     
        public string DepartmentName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }        
        public string UpdateBy { get; set; }
        public DateTime PassUpdateDate { get; set; }        
    }
}
