using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Controls
{



	public class EAPCheckBox : System.Windows.Forms.CheckBox {

		private System.ComponentModel.Container components = null;
		private BindedControlCollection m_arBindedControls = new BindedControlCollection();
		
		public EAPCheckBox() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			UpdateBindedControlLook();
		}


		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		[Category("Behavior"),
		Description("Specify \"Binded Contrl\" with this control."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public BindedControlCollection BindedControls{
			get{	return m_arBindedControls;}
		}


		#region Component Designer generated code
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
		}
		#endregion
		
		public void UpdateBindedControlLook() {
			bool bFocused = false;
			if (this.DesignMode == false){
				foreach (BindedControl bc in m_arBindedControls){
					if (bc.Control != null){
						ControlUtil.EnabledControl(this.Checked && this.Enabled, bc.Control);
						if (bc.Control.CanFocus && !bFocused && this.Checked && this.Enabled){
							bc.Control.Focus();
							if (bc.Control is TextBoxBase){
								((TextBoxBase)bc.Control).SelectAll();
							}
							bFocused = true;
						}
					}
				}
			}
		}

		protected override void OnCreateControl() {
			UpdateBindedControlLook();
			base.OnCreateControl ();
		}

		protected override void OnEnabledChanged(EventArgs e) {
			if (this.DesignMode == false){
				UpdateBindedControlLook();
				base.OnEnabledChanged (e);
			}
		}

		
		protected override void OnCheckedChanged(System.EventArgs e) {
			if (this.DesignMode == false){
				UpdateBindedControlLook();
				base.OnCheckedChanged(e);
			}
		}
		
	}
}
