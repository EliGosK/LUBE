using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using EAP.Framework.Data;

namespace BusinessService
{
    public class ACS310_ProcessBIZ
    {
        #region ACS310 - Retrieve Data

        #region Get Combobox

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_ACS310_Get_PeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetSystemCombobox()
        {
            return StoreProcedure.sp_ACS310_Get_SystemCombo(AppEnvironment.Database);
        }

        #endregion

        #region Checking

        public bool CheckExistTransfer(int year, int period)
        {
            string strRet = StoreProcedure.sp_ACS310_Check_ExistTransfer(AppEnvironment.Database, year, period);
            return Util.ConvertObjectToBoolean(strRet);
        }

        public bool CheckRepeatProcess(int year, int period, int system)
        {
            string strRet = StoreProcedure.sp_ACS310_Check_RepeatProcess(AppEnvironment.Database, year, period, system);
            return Util.ConvertObjectToBoolean(strRet);
        }

        #endregion

        public DataTable GetRevision(int year, int period)
        {
            return StoreProcedure.sp_Common_Get_Revision(AppEnvironment.Database, year, period);
        }

        public void RetrieveData(int year, int period, int system, string byUser, SqlInfoMessageEventHandler infoMessageHandler = null)
        {
            SqlConnection conn = new SqlConnection(AppConfig.ConnectionString);            
            conn.FireInfoMessageEventOnUserErrors = true; // This flag allow receive message realtime.            

            SqlData db = new SqlData(conn);
            try
            {
                db.BeginTrans();

                conn.InfoMessage += infoMessageHandler;                
                StoreProcedure.sp_ACS310_Process_RetrieveData(db, year, period, system, byUser);

                conn.InfoMessage -= infoMessageHandler;
                StoreProcedure.sp_ACS310_Update_ProcessControl(db, year, period, byUser);

                db.CommitTrans();
            }
            catch (Exception ex)
            {
                if (db.HasTransaction)
                    db.RollbackTrans();

                throw;
            }  
            finally
            {
                db.Close();
            }
        }

        //public void ProcessRetrieveData(SqlInfoMessageEventHandler infoMessageHandler)
        //{
        //    using (SqlConnection conn = new SqlConnection(AppConfig.ConnectionString))
        //    {                
        //        conn.InfoMessage += infoMessageHandler;
        //        conn.FireInfoMessageEventOnUserErrors = true; // This flag allow receive message realtime.


        //        conn.Open();
                                
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {

        //            using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.Serializable))
        //            {
        //                cmd.CommandText = "sp_Test_Progress";
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandTimeout = AppConfig.SqlDefaultTimeout;
        //                cmd.Transaction = trans;


        //                try
        //                {
        //                    // Execute retrieve data.
        //                    cmd.ExecuteNonQuery();

        //                    // Execute Update Process status
        //                    conn.InfoMessage -= infoMessageHandler;
        //                    conn.FireInfoMessageEventOnUserErrors = false;
                            




        //                    // Commit transaction
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    throw;
        //                }                        
        //            }

        //        }                                                
        //    }
        //}

        #endregion
    }
}
