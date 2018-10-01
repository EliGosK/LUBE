using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BusinessService
{
    public class ProcessBIZ
    {
        #region ACS310 - Retrieve Data

        public void ProcessRetrieveData(SqlInfoMessageEventHandler infoMessageHandler)
        {
            using (SqlConnection conn = new SqlConnection(AppConfig.ConnectionString))
            {                
                conn.InfoMessage += infoMessageHandler;
                conn.FireInfoMessageEventOnUserErrors = true; // This flag allow receive message realtime.


                conn.Open();
                
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable))
                    {
                        cmd.CommandText = "sp_Test_Progress";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = AppConfig.SqlDefaultTimeout;
                        cmd.Transaction = trans;


                        try
                        {

                            cmd.ExecuteNonQuery();

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }

                }                

                conn.Close();
            }
        }

        #endregion
    }
}
