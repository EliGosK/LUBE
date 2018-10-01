using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;
using EAP.Framework.Data;

namespace BusinessService
{
    public class ACS340_ProcessBIZ
    {
        #region Checking

        public bool CheckExistTransfer(int year)
        {
            string strRet = StoreProcedure.sp_ACS340_Check_ExistTransfer(AppEnvironment.Database, year);
            return Util.ConvertObjectToBoolean(strRet);
        }

        public string CheckExistProcess(int year)
        {
            string strRet = StoreProcedure.sp_ACS340_Check_ExistProcess(AppEnvironment.Database, year);
            return strRet;
        }

        #endregion

        #region Retrieve Data

        public DataTable RetrieveData(int year)
        {
            return StoreProcedure.sp_ACS340_RetrieveData(AppEnvironment.Database, year);
        }

        #endregion

        #region Process Calculate MOH Rate

        public tb_NewStandardMOH ProcessData(int year, string byUser)
        {
            Database db = AppEnvironment.CreateDatabase();

            try
            {
                db.BeginTrans();

                tb_NewStandardMOH result = StoreProcedure.sp_ACS340_Process_CalculateStandardMOH(db, year, byUser);

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

        public void TransferData(int year, string byUser)
        {
            Database db = AppEnvironment.CreateDatabase();

            try
            {
                db.BeginTrans();

                StoreProcedure.sp_ACS340_TransferData(db, year, byUser);

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

        #endregion
    }
}
