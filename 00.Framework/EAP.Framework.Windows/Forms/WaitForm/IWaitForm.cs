using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Windows.Forms
{
    public interface IWaitForm
    {
        void ShowWaitForm();
        void CloseWaitForm();
        void SetCaption(string text);
        void SetDescription(string text);
        void SetProgressValue(int percent);
    }
}
