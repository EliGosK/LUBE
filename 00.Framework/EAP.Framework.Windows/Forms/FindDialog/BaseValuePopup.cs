using System;
using System.Windows.Forms;


namespace EAP.Framework.Windows.Forms
{
	public class BaseValuePopup : System.Windows.Forms.Form
    {
		private bool m_bIsActivated = false;
		private void InitializeComponent()
        {
			// 
			// BaseValuePopup
			// Utils
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(168, 232);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BaseValuePopup";
			this.Closed += new System.EventHandler(this.BaseValuePopup_Closed);

		}
	
		public BaseValuePopup() {
			InitializeComponent();
			m_bIsActivated = false;
		}
		protected override void WndProc(ref System.Windows.Forms.Message m) {
			switch (m.Msg){
				case WindowMessage.WM_ACTIVATE:
					m_bIsActivated = true;
					break;
				case WindowMessage.WM_CREATE:
					m_bIsActivated = false;
					break;
				case WindowMessage.WM_NCACTIVATE:
					if (m_bIsActivated){
						m_bIsActivated = false;
						this.DialogResult = DialogResult.OK;
						this.Close();
					}
					break;
				case WindowMessage.WM_SHOWWINDOW:
					m_bIsActivated = false;
					break;
			}
			base.WndProc (ref m);
		}

		private void BaseValuePopup_Closed(object sender, System.EventArgs e) {
			m_bIsActivated = false;
		}
	}
}