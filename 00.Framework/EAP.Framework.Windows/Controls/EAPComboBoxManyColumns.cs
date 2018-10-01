using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using EAP.Framework.Windows.Controls.ComboResource;

namespace EAP.Framework.Windows.Controls
{
    /// <summary>
    /// </summary>
    public class EAPComboBoxManyColumns : ComboBox {
        [DllImport("USER32.DLL", EntryPoint = "SendMessageW", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        private static extern long SendMessage(IntPtr Handle, Int32 msg, IntPtr wParam, IntPtr lParam);

        	
        private const int WM_KEYDOWN = 0x0100;        

        private DataTable table;
        private ComboFormMode mode = ComboFormMode.NORMAL;
        private IComboForm comboForm;
        private Size comboFormSize;
        private bool setSized = false;
        private String display = String.Empty;
        private int displayIndex = 0;
        private int counter = -1;
        
        private bool _isValidating = false;//debug when use event SelectedIndexChanged then text on control will had selection.

        public delegate void SelectedItemEventHandler(object sender, DataRow drow);

        public event SelectedItemEventHandler SelectedRowHandler;
        private Point areaLocation = Point.Empty;

        public EAPComboBoxManyColumns() {
        }

        //Set ComboBox arean location
        public Point AreaLocation {
            get {
                return this.areaLocation;
            }
            set {
                this.areaLocation = value;
            }
        }

        protected override void OnDropDown(EventArgs e) {
            //default ComboBox area location.
            if (this.areaLocation == Point.Empty)
                this.areaLocation = new Point(0, 0);

            Form parent = this.FindForm();
            Point cp = parent.PointToScreen(this.Location);
            Point pareaLocation = new Point(this.areaLocation.X + cp.X, this.areaLocation.Y + cp.Y + this.Height);
            //}
            if (this.comboForm != null) {
                /** clear Items because data in Items disallow show **/
                this.Items.Clear();
                this.comboForm.SetNotify(new SelectedItemHandler(ComboForm_SelectedItemEvent));
                this.comboForm.SetSize(this.comboFormSize);
                this.comboForm.SetLocation(pareaLocation);
                this.comboForm.SetFont(this.Font);
                this.comboForm.Expand(this.counter);
            }
            base.OnDropDown(e);
        }

        /// <summary>
        /// DataTable for show in listview
        /// </summary>
        public DataTable Table {
            get {
                return this.table;
            }
            set {
                this.table = value;
                CreateComboForm();
            }
        }

        //Set ComboBox mode
        public ComboFormMode Mode {
            get {
                return this.mode;
            }
            set {
                this.mode = value;
                CreateComboForm();
            }
        }

        private void CreateComboForm() {
            this.comboForm = ComboFormFactory.CreateForm(this.mode);
            if (this.comboForm != null && this.table != null)
                this.comboForm.InitializeData(this.table);
            else if (this.table == null && this.mode != ComboFormMode.NORMAL)
                this.comboForm = null;
        }

        /// <summary>
        /// ComboBox area size
        /// </summary>
        public Size AreaSize {
            set {
                if (value.Width > this.Size.Width && value.Height > this.ItemHeight)
                    this.comboFormSize = value;
                setSized = true;
            }
        }

        /** column name. Note: Case Sensitive **/
        public String Display {
            get {
                return this.display;
            }
            set {
                this.display = value;
                this.DisplayIndex = FindIndex(value);
            }
        }

        /** display index count from 0 **/
        public int DisplayIndex {
            get {
                return this.displayIndex;
            }
            set {
                this.displayIndex = value;
            }
        }

        //Get object in combobox area example Listview, tree etc.
        public object GetObjectInArea() {
            return this.comboForm.GetObjectInArea();
        }

        //All Columns name
        public String[] GetColumnsName() {
            String[] temp;

            if (this.table != null)
                temp = new String[this.table.Columns.Count];
            else
                temp = new String[0];

            for (int i = 0; i < temp.Length; i++) {
                temp[i] = this.table.Columns[i].ToString();
            }

            return temp;
        }

        private int FindIndex(String display) {
            if (this.table != null && this.display != null) {
                for (int i = 0; i < this.table.Columns.Count; i++) {
                    if (display.Equals(this.table.Columns[i].ToString()))
                        return i;
                }
                return 0;//for default value show first column.
            }
            return -1;
        }

        /** Note: Case Sensitive **/
        public void SetColumnWidth(String colName, int width) {
            if (this.comboForm != null)
                this.comboForm.SetColumnWidth(colName, width);
        }

        public void SetColumnWidth(int colIndex, int width) {
            if (this.comboForm != null)
                this.comboForm.SetColumnWidth(colIndex, width);
        }

        public void SetColumnAlignment(int colIndex, HorizontalAlignment align) {
            this.comboForm.SetAlignment(colIndex, align);
        }

        /** wait notify from Form **/
        private void ComboForm_SelectedItemEvent(object sender, ComboEvent e) {
            if (e.Type == EventType.SELECTED) {
                DataRow drow = e.Row;
                this.counter = e.SelectedRow;
                if (this.displayIndex < drow.Table.Columns.Count && this.displayIndex >= 0)
                    this.Text = drow[this.displayIndex].ToString();
                else
                    this.Text = drow[this.display].ToString();

                if (this.SelectedRowHandler != null)
                    this.SelectedRowHandler(this, e.Row);
            } else if (e.Type == EventType.FORM_HIDED) {
                //this.FindForm().Refresh(); 
                SendKeys.Send("{ESC}");
            }
        }

        /** set default value for area size **/
        protected override void OnSizeChanged(EventArgs e) {
            if (false == this.setSized) {
                this.comboFormSize.Height = this.ItemHeight * 2 + SystemInformation.HorizontalScrollBarHeight;
                this.comboFormSize.Width = this.Size.Width;
            }
            base.OnSizeChanged(e);
        }

        /** use arrow key for show data **/
        protected override void OnKeyDown(KeyEventArgs e) {
            if (ComboFormMode.NORMAL != this.Mode) {
                if (this.table.Rows.Count > 0) {
                    //debug by Mr.Fuangwith Sopharath @ 05/06/2006
                    if (e.KeyData == Keys.Down && this.counter < this.table.Rows.Count - 1) {
                        this.counter++;
                    } else if (e.KeyData == Keys.Up && this.counter > 0) {
                        this.counter--;
                    }
                    if (e.KeyData == Keys.Down || e.KeyData == Keys.Up)
                        if (0 <= this.counter && this.counter < this.table.Rows.Count) {
                            ComboEvent cbe = new ComboEvent(this.table.Rows[this.counter], this.counter, EventType.SELECTED);
                            ComboForm_SelectedItemEvent(this, cbe);
                        }
                }
                if (e.KeyData == Keys.Enter) {
                    this.counter = 0;
                    SendKeys.Send("{ESC}");
                }
            }

            //Add By Boonlert F. 28/07/2005
            IntPtr pHandle = Parent.FindForm().Handle;

            SendMessage(pHandle, WM_KEYDOWN, new IntPtr((int)e.KeyCode), IntPtr.Zero);

            //End Add

            //Add By Boonlert F. 11/12/2005
            _isValidating = false;

            base.OnKeyDown(e);
        }

        //Auto Complete
        protected override void OnKeyPress(KeyPressEventArgs e) {
            if (!Char.IsControl(e.KeyChar)) {
                String strFind = this.Text.Substring(0, this.SelectionStart) + e.KeyChar;
                int index = -1;
                if (ComboFormMode.NORMAL == this.Mode) {
                    if (false == this.DroppedDown) {
                        this.DroppedDown = true;
                    }
                    index = this.FindStringExact(strFind);
                    if (-1 == index) {
                        index = this.FindString(strFind);
                    }
                    if (-1 == index) {
                        return;
                    }
                    if (index != -1) this.SelectedIndex = index;
                    //Add By Boonlert F. 11/12/2005
                    if (!_isValidating) {
                        this.SelectionStart = strFind.Length;
                        this.SelectionLength = this.Text.Length - strFind.Length;
                    }
                    e.Handled = true;
                } else {
                    this.Text = this.comboForm.SearchString(strFind, this.DisplayIndex, ref index);
                    if (!_isValidating) {
                        this.SelectionStart = strFind.Length;
                        if (this.Text.Length - strFind.Length > 0)
                            this.SelectionLength = this.Text.Length - strFind.Length;
                    }
                    e.Handled = true;
                }
                //debug by Mr.Fuangwith Sopharath @ 05/06/2006
                if (index != -1) {
                    ComboEvent cbe = new ComboEvent(this.table.Rows[index], index, EventType.SELECTED);
                    ComboForm_SelectedItemEvent(this, cbe);
                }
            }
            base.OnKeyPress(e);
        }//end key_Press

        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);
        }

        //Add By Boonlert F. 11/12/2005
        protected override void OnValidating(System.ComponentModel.CancelEventArgs e) {
            _isValidating = true; //Add By Boonlert F. 11/12/2005
            base.OnValidating(e);
        }




    }//end class
}//end namespace
