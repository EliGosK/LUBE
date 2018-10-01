using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
    [ToolboxItem(false)]
    public class CustomTopFormBase : TopFormBase
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle |= 2048;
                if (this.HasSystemShadow && (Environment.OSVersion.Version.Major > 5 || Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor > 0))
                    createParams.ClassStyle |= 131072;
                return createParams;
            }
        }

        protected virtual bool HasSystemShadow
        {
            get
            {
                return true;
            }
        }
    }
}
