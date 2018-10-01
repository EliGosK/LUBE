using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAP.Framework.Data;

namespace DataAccess
{
    public class SystemDAO
    {
        public static bool TestConnection(Database db)
        {
            try
            {                
                db.Open();

                // ถ้าไม่ระบุ DatabaseName ทาง SqlServer จะ Default ไปที่ Master 
                // ซึ่งจะทำให้เข้าได้ทุกกรณี
                db.ExecuteCommand(new DataRequest("SELECT GetDate()"));

                // เช็คตาราง ถ้าไม่พบ แสดงว่าไม่ใช่ Database ที่ต้องการ
                //db.ExecuteCommand(new DataRequest("SELECT TOP 1 1 FROM tb_Msg"));

                db.Close();
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
