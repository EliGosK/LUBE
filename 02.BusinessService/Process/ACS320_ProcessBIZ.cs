using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;
using EAP.Framework.Data;

namespace BusinessService
{
    public class ACS320_ProcessBIZ
    {
        #region ACS320 - Actual Cost Calculation

        #region Get Combobox

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_ACS310_Get_PeriodCombo(AppEnvironment.Database);
        }

        #endregion

        #region Get Data

        public tb_Transfer GetTransferData(int year, int period)
        {
            return StoreProcedure.sp_ACS320_Get_TransferData(AppEnvironment.Database, year, period);
        }


        #endregion

        #region Checking

        public bool CheckExistTransfer(int year, int period)
        {
            string strRet = StoreProcedure.sp_ACS320_Check_ExistTransfer(AppEnvironment.Database, year, period);
            return Util.ConvertObjectToBoolean(strRet);
        }

        public bool CheckRetrieve(int year, int period)
        {
            string strRet = StoreProcedure.sp_ACS320_Check_Retrieve(AppEnvironment.Database, year, period);
            return Util.ConvertObjectToBoolean(strRet);
        }

        public bool CheckRepeatProcess(int year, int period)
        {
            string strRet = StoreProcedure.sp_ACS320_Check_RepeatProcess(AppEnvironment.Database, year, period);
            return Util.ConvertObjectToBoolean(strRet);
        }

        #endregion

        #region Process - Calculate

        public ACS320_Process CalculateData(int year, int period, string byUser, SqlInfoMessageEventHandler infoMessageHandler = null)
        {
            SqlConnection conn = new SqlConnection(AppConfig.ConnectionString);
            conn.FireInfoMessageEventOnUserErrors = true; // This flag allow receive message realtime.            

            SqlData db = new SqlData(conn);
            try
            {
                db.BeginTrans();

                //#####################
                //# Process Calcualte 
                //#  While processing, reponse progress periodically.
                //#####################
                conn.InfoMessage += infoMessageHandler;
                ACS320_Process result = StoreProcedure.sp_ACS320_Process_CalculateData(db, year, period, byUser);

                //#####################
                //# Update Calculation Status
                //#####################
                conn.InfoMessage -= infoMessageHandler;
                StoreProcedure.sp_ACS320_Update_ProcessControl(db, year, period, byUser);

                db.CommitTrans();

                return result;
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

        public ACS320_Process ReCalculateData(int year, int period, decimal pActMOH, decimal pActCapaUsed, decimal pSoldLiter, decimal pEndingLiter, decimal pSoldLiterOEM, decimal pEndingLiterOEM, string byUser)
        {
            ACS320_Process result = StoreProcedure.sp_ACS320_Process_ReCal(AppEnvironment.Database, year, period, pActMOH, pActCapaUsed, pSoldLiter, pEndingLiter, pSoldLiterOEM, pEndingLiterOEM, byUser);
            return result;
        }

        #endregion

        #endregion
    }
}
