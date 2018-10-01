using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.Runtime.InteropServices;


namespace EAP.Framework.Windows.Controls.ComboResource
{
    //delegate for notify when user selected item in listview
    //public delegate void SelectedItemEventHandler(object sender,DataRow drow);
    public delegate void SelectedItemHandler(object sender, ComboEvent e);

    //implements IComboForm interface for general 
    public class ListViewForm : System.Windows.Forms.Form, IComboForm {
        private System.ComponentModel.Container components = null;
        private DataTable table;
        private Graphics ruler;
        private System.Windows.Forms.ListView listView1;
        private String[] headers;

        public event SelectedItemHandler SelectedItemHandler;

        public ListViewForm() {
            this.ruler = this.CreateGraphics();
            InitializeComponent();
        }

        /** implements IComboForm Interface **/
        public void InitializeData(DataTable table) {
            this.table = table;
            this.headers = new String[table.Columns.Count];
            int[] maxWidth = new int[table.Columns.Count];

            /** Add data into ListView **/
            for (int row = 0; row < this.table.Rows.Count; row++) {
                DataRow dataRow = this.table.Rows[row];
                ListViewItem item = new ListViewItem();
                for (int col = 0; col < this.table.Columns.Count; col++) {
                    DataColumn dataColumn = this.table.Columns[col];

                    /** find maxWidth **/
                    int width = StringWidth(dataRow[dataColumn].ToString());
                    if (maxWidth[col] < width)
                        maxWidth[col] = width;

                    /** first column **/
                    if (0 == col)
                        item.Text = dataRow[dataColumn].ToString();
                    else
                        item.SubItems.Add(dataRow[dataColumn].ToString());

                    /** header **/
                    if (0 == row) {
                        this.headers[col] = dataColumn.ToString();
                        maxWidth[col] = StringWidth(dataColumn.ToString()) + 10;
                    }

                }
                this.listView1.Items.Add(item);
            }

            /** set header name. i variable is column **/
            for (int i = 0; i < this.headers.Length; i++) {
                this.listView1.Columns.Add(this.headers[i], maxWidth[i], HorizontalAlignment.Center);
            }

        }//end InitializeData

        public void Expand(int iSelected) {
            if (0 <= iSelected && iSelected < this.listView1.Items.Count) {
                this.listView1.Items[iSelected].Selected = true;
                this.listView1.Items[iSelected].Focused = true;
            }
            this.Show();
        }

        public void SetLocation(Point point) {
            this.Location = point;
        }

        public void SetSize(Size size) {
            this.Size = size;
        }

        public void SetNotify(SelectedItemHandler listener) {
            this.SelectedItemHandler += new SelectedItemHandler(listener);
        }

        public void SetColumnWidth(String colName, int width) {
            if (this.headers != null) {
                for (int i = 0; i < this.headers.Length; i++) {
                    if (this.headers[i].Equals(colName))
                        SetColumnWidth(i, width);
                }
            }
        }

        public void SetColumnWidth(int colIndex, int width) {
            if (colIndex >= 0 && colIndex < this.headers.Length)
                this.listView1.Columns[colIndex].Width = width;
        }

        public String SearchString(String message, int colIndex, ref int iSelected) {
            for (int i = 0; i < this.listView1.Items.Count; i++) {
                if (this.listView1.Items[i].SubItems[colIndex].Text.StartsWith(message)) {
                    iSelected = i;
                    return this.listView1.Items[i].SubItems[colIndex].Text;
                }
            }
            return message;
        }

        public void SetAlignment(int colIndex, HorizontalAlignment align) {
            this.listView1.Columns[colIndex].TextAlign = align;
        }

        public object GetObjectInArea() {
            return this.listView1;
        }
        /** end implements IComboForm interface **/

        //Measure string
        private int StringWidth(String message) {
            return (int)this.ruler.MeasureString(message, this.Font).Width;
        }

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
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(232, 248);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            this.listView1.Leave += new System.EventHandler(this.ListViewForm_Leave);
            // 
            // ListViewForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(232, 248);
            this.ControlBox = false;
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListViewForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewForm_MouseDown);
            this.Leave += new System.EventHandler(this.ListViewForm_Leave);
            this.Deactivate += new System.EventHandler(this.ListViewForm_Deactivate);
            this.ResumeLayout(false);

        }
        #endregion

        //Hide this form when click out of this form
        private void ListViewForm_Leave(object sender, System.EventArgs e) {
            this.Hide();
            this.SelectedItemHandler(sender, new ComboEvent(null, -1, EventType.FORM_HIDED));
        }

        //Hide this form if deactivate
        private void ListViewForm_Deactivate(object sender, System.EventArgs e) {
            this.Hide();
            this.SelectedItemHandler(sender, new ComboEvent(null, -1, EventType.FORM_HIDED));
        }

        /** if key is enter key or ESC key will hide this form
         *  if arrow key (up-down) focus row in listview and notify item to listener **/
        private void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            if (this.listView1.SelectedItems.Count > 0) {

                //Debug by Mr.Fuangwith Sopharath @ 05/06/2006
                //DataRow selectedRow = this.table.Rows[this.listView1.SelectedIndices[0]];
                int iSelectedRow = getRowIndex(e.KeyData);
                DataRow selectedRow = null;
                if (0 <= iSelectedRow && iSelectedRow < this.table.Rows.Count) selectedRow = this.table.Rows[getRowIndex(e.KeyData)];
                ComboEvent cbe = new ComboEvent(selectedRow, iSelectedRow, EventType.SELECTED);
                if (this.SelectedItemHandler != null)
                    this.SelectedItemHandler(sender, cbe);
            }
            if (Keys.Enter == e.KeyData || Keys.Escape == e.KeyData || Keys.F4 == e.KeyData) {
                this.Hide();
                this.SelectedItemHandler(sender, new ComboEvent(null, -1, EventType.FORM_HIDED));
            }
        }

        private int getRowIndex(Keys eKey) {
            int iSelectedIndex = this.listView1.SelectedIndices[0];
            if (eKey == Keys.Up) {
                if (iSelectedIndex - 1 >= 0) {
                    iSelectedIndex--;
                }
            } else if (eKey == Keys.Down) {
                if (iSelectedIndex + 1 < this.listView1.Items.Count) {
                    iSelectedIndex++;
                }
            }
            return iSelectedIndex;
        }

        //notify item to listener and hide this form
        private void listView1_Click(object sender, System.EventArgs e) {
            if (this.listView1.SelectedItems.Count > 0) {
                int iSelectedRow = this.listView1.SelectedIndices[0];
                DataRow selectedRow = this.table.Rows[iSelectedRow];
                ComboEvent cbe = new ComboEvent(selectedRow, iSelectedRow, EventType.SELECTED);
                if (this.SelectedItemHandler != null)
                    this.SelectedItemHandler(sender, cbe);
            }
            this.Hide();
            this.SelectedItemHandler(sender, new ComboEvent(null, -1, EventType.FORM_HIDED));
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int iMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        private void ListViewForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }

        }

        public void SetFont(Font font) {
            this.listView1.Font = font;
        }

    }
}
