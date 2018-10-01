using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

/** author: Fuangwith Sopharath
 *  department: PI-RD
 *  date: 27/05/2005
 */

namespace EAP.Framework.Windows.Controls.ComboResource
{
    /// <summary>
    /// Summary description for TreeForm.
    /// </summary>
    public class TreeForm : System.Windows.Forms.Form, IComboForm {
        private System.Windows.Forms.TreeView treeView1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public TreeForm() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public void InitializeData(System.Data.DataTable Table) {
        }

        public void SetLocation(Point point) {
            this.Location = point;
        }

        public void Expand(int iSelected) {
            this.Show();
        }

        public void SetSize(Size size) {
            this.Size = size;
        }

        public void SetNotify(SelectedItemHandler listener) {

        }

        public void SetColumnWidth(String colName, int width) { }
        public void SetColumnWidth(int colIndex, int width) { }
        public String SearchString(String message, int colIndex, ref int iSelected) { return null; }
        public void SetAlignment(int colIndex, HorizontalAlignment align) { }
        public object GetObjectInArea() { return null; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
																																									 new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
																																																														new System.Windows.Forms.TreeNode("Node2")})})});
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new System.Drawing.Size(292, 273);
            this.treeView1.TabIndex = 1;
            // 
            // TreeForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TreeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TreeForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TreeForm_KeyPress);
            this.Leave += new System.EventHandler(this.TreeForm_Leave);
            this.Deactivate += new System.EventHandler(this.TreeForm_Deactivate);
            this.ResumeLayout(false);

        }
        #endregion

        private void TreeForm_Leave(object sender, System.EventArgs e) {
            this.Hide();
        }

        private void TreeForm_Deactivate(object sender, System.EventArgs e) {
            this.Hide();
        }

        private void TreeForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
            //To do something
            this.Hide();
        }

        public void SetFont(Font font) {
            this.treeView1.Font = font;
        }
    }
}
