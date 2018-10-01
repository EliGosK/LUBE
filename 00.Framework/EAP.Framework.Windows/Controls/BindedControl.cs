using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace EAP.Framework.Windows.Controls
{


	[TypeConverter(typeof(BindedControlConverter))]
	public class BindedControl {

		private Control m_control = null;

		public BindedControl() {}
		public BindedControl(Control control){
			m_control = control;
		}

		public System.Windows.Forms.Control Control{
			get{	return m_control;}
			set{	m_control = value;}
		}
		public override bool Equals(object obj) {
			if (this.Control != null){
				return this.Control.Equals(((BindedControl)obj).Control);
			}else{
				return false;
			}
		}
		public override int GetHashCode() {
			return base.GetHashCode ();
		}
		public override string ToString() {
			if (m_control != null){
				return m_control.Name;
			}else{
				return base.ToString();
			}
		}


	}
	
}

