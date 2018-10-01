using System;
using System.Windows.Forms;
using System.Collections;

namespace EAP.Framework.Windows.Forms
{
	public interface IValuePopup{
		bool AllowMultiValue{get;	set;}
		void SetListValues(ArrayList arValues);
		object []GetValues();
		void SetValues(params object []value);
	}

}
