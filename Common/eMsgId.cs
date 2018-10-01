using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum eMsgId
    {
        #region Common

        /// <summary>
        /// Do you want to save?
        /// </summary>
        COM0001,
        /// <summary>
        /// Do you want to cancel?
        /// </summary>
        COM0002,
        /// <summary>
        /// Do you want to delete?
        /// </summary>
        COM0003,
        /// <summary>
        /// Can't close screen while editing data. Please cancel or save data before close screen.
        /// </summary>
        COM0004,
        /// <summary>
        /// Delete data completely.
        /// </summary>
        COM0005,
        /// <summary>
        /// There is no data to perform operation. Please input data first.
        /// </summary>
        COM0006,
        /// <summary>
        /// There is no data with you criteria.
        /// </summary>
        COM0007,
        /// <summary>
        /// There is no data for export!
        /// </summary>
        COM0008,
        /// <summary>
        /// Successfully Export Process.
        /// </summary>
        COM0009,
        /// <summary>
        /// Unsuccessful Export Process.
        /// </summary>
        COM0010,
        /// <summary>
        /// Save completed.
        /// </summary>
        COM0011,
        /// <summary>
        /// This File has been uncomplete Imported.  Do you want to import again?
        /// </summary>
        COM0012,
        /// <summary>
        /// This File has been imported completely already.
        /// </summary>
        COM0013,
        /// <summary>
        /// Import Process Successful. Click OK to see error report.
        /// </summary>
        COM0014,
        /// <summary>
        /// Import Process Succesful.
        /// </summary>
        COM0015,
        /// <summary>
        /// Un-Import Process Successful.
        /// </summary>
        COM0016,
        /// <summary>
        /// Same Part No for this Delivery Location.\nDo you want to change to
        /// </summary>
        COM0017,
        /// <summary>
        /// Please select at least one criteria.
        /// </summary>
        COM0018,
        /// <summary>
        /// Your password is already expired!!!\nYou have to change now!!!
        /// </summary>
        COM0019,
        /// <summary>
        /// Your password is nearly expired.\nDo you want to change now?
        /// </summary>
        COM0020,
        /// <summary>
        /// Please check your user or password is correct. And try again.
        /// </summary>
        COM0021,
        /// <summary>
        /// Cancel already.
        /// </summary>
        COM0022,
        /// <summary>
        /// You have not selected any criteria
        /// </summary>
        COM0023,
        /// <summary>
        /// {0} cannot be empty. Please enter value.
        /// </summary>
        COM0024,
        /// <summary>
        /// {0} not found. Please enter new value.
        /// </summary>
        COM0025,
        /// <summary>
        /// {0} is already existed. Please input new value.
        /// </summary>
        COM0026,
        /// <summary>
        /// {0} cannot be empty or zero. Please enter new value.
        /// </summary>
        COM0027,
        /// <summary>
        /// Please input data in {0}
        /// </summary>
        COM0028,
        /// <summary>
        /// Do you want to export?
        /// </summary>
        COM0029,
        /// <summary>
        /// Process Successfully
        /// </summary>
        COM0030,
        /// <summary>
        /// {0} is too long length (Max: {1})
        /// </summary>
        COM0031,
        /// <summary>
        /// Do you want to logout?
        /// </summary>
        COM0032,
        /// <summary>
        /// Please close all screen before
        /// </summary>
        COM0033,
        /// <summary>
        /// Can't connect file server
        /// </summary>
        COM0034,
        /// <summary>
        /// Please check input value.
        /// </summary>
        COM0035,
        /// <summary>
        /// Data can't delete because it has been referenced.
        /// </summary>
        COM0036,        
        /// <summary>
        /// Do you want to exit?
        /// </summary>
        COM0038,

        /// <summary>
        /// test message
        /// </summary>
        MSG0001,
        #endregion
    }
}
