using System;
using System.Collections;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
	public class DefaultValuePopup : BaseValuePopup, IValuePopup{
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lstvValues;
		private System.Windows.Forms.Button btnCancel;
		private bool m_bAllowMultiValue = false;
		private void InitializeComponent() {
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lstvValues = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(24, 170);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 20);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(88, 170);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 20);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lstvValues
			// 
			this.lstvValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstvValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstvValues.CheckBoxes = true;
			this.lstvValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnHeader1});
			this.lstvValues.FullRowSelect = true;
			this.lstvValues.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstvValues.Location = new System.Drawing.Point(0, 0);
			this.lstvValues.Name = "lstvValues";
			this.lstvValues.Size = new System.Drawing.Size(144, 160);
			this.lstvValues.TabIndex = 6;
			this.lstvValues.View = System.Windows.Forms.View.Details;
			this.lstvValues.Resize += new System.EventHandler(this.lstvValues_Resize);
			this.lstvValues.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstvValues_ItemCheck);
			// 
			// DefaultValuePopup
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(144, 192);
			this.Controls.Add(this.lstvValues);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.Name = "DefaultValuePopup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.ResumeLayout(false);

		}
	
		public DefaultValuePopup() {
			InitializeComponent();
			lstvValues.Items.Clear();
		}
		public bool AllowMultiValue{
			get{	return m_bAllowMultiValue;}
			set{	m_bAllowMultiValue = value;}
		}

		public void SetListValues(ArrayList arValues){
			lstvValues.Items.Clear();
			if (arValues != null){
				for (int i=0;i<arValues.Count;++i){
					ListViewItem lv = new ListViewItem(arValues[i].ToString());
					lv.Tag = arValues[i];
					lstvValues.Items.Add(lv);
				}
			}
		}
		private void btnOK_Click(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}
		

		public object []GetValues(){
			ArrayList arValues = new ArrayList();
			for (int i=0;i<lstvValues.Items.Count;++i){
				if (lstvValues.Items[i].Checked){
					arValues.Add(lstvValues.Items[i].Tag);
				}
			}
			object []oValues = new object[arValues.Count];
			for (int i=0;i<arValues.Count;++i){
				oValues[i] = arValues[i];
			}
			return oValues;
		}
		public void SetValues(params object []value){
			ArrayList arValues = new ArrayList();
			arValues.AddRange(value);
			for (int i=0;i<lstvValues.Items.Count;++i){
				bool bChecked = (arValues.Contains(lstvValues.Items[i].Tag));
				lstvValues.Items[i].Checked = bChecked;
			}			
		}

		private void lstvValues_Resize(object sender, System.EventArgs e) {
			lstvValues.Columns[0].Width = lstvValues.Width - SystemInformation.BorderSize.Width;
		}

		private void lstvValues_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e) {
			if (m_bAllowMultiValue == false){
				for (int i=0;i<lstvValues.Items.Count;++i){
					if (i != e.Index){
						lstvValues.Items[i].Checked = false;
					}
				}
			}
		}

	}
}


