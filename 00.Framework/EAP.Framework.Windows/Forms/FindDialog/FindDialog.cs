using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using EAP.Framework.Data;

namespace EAP.Framework.Windows.Forms
{
    public partial class FindDialog : Form
    {
        public delegate void CreateDataRequestHandler(ref DataRequest dataRequest, FindKeywordCollection keywords);

        public event CreateDataRequestHandler CreateDataRequest = null;
        public event EventHandler FinishedFillResult = null;

        #region Constants
        private const int LEFT_OFFSET = 20;
        private const int YSPACE_PANEL_OPTION = 3;
        private const int MAXIMUM_COL_WIDTH = 200;
        private const int DEFAULT_COL_WIDTH = 65;
        private const int OFFSET_COL_WIDTH = 5;
        #endregion

        #region Static Member
        private static char m_cMultiValueSeperator = ',';
        #endregion

        #region Members
        protected string m_strMainSQL = string.Empty;	// Main SQL
        protected string m_strOptionalSQL = string.Empty;	// GroupBy, Order By
        protected int m_iStartPage = 1; // String Page
        protected int m_iRowPerPage = 100; //จำนวน record ต่อหน้า
        protected int m_iShowPage = 5;
        protected string m_strThisPage = "center";
        protected string m_strParam = string.Empty;
        protected string m_OldstrParam = string.Empty;
        // Add by Wirachai T. 2007/07/12
        private string m_strFormName = string.Empty;
        // End Add
        // Add by kimmik 20070815
        private DataSet m_ds = null;
        private DataRequest m_request = null;
        // End Add
        protected CommandType m_commandType = CommandType.Text;
        protected bool m_bAlreadyHaveWhereStatement = false;
        protected IFindDialogDAO m_findDialogDAO = null;
        protected bool m_bFindOnStart = false;
        protected FindOptionCollection m_findOptions;
        protected bool m_bPreviewOnly = false;
        protected bool m_bMultiSelection = false;
        protected SelectedRecordCollection m_selectedRecords;
        //protected ResultFormatCollection m_resultFormats = new ResultFormatCollection();
        protected bool m_bIsFoundData = false;
        protected bool m_bIsDialogLoaded = false;
        protected FindOptionDirection m_optionDirection = FindOptionDirection.CrossDown;
        private Size m_szRemember;
        private EAP.Framework.Windows.Controls.PageNavigator pageNavigator;
        private FindKeywordCollection m_selectedKeywords;
        

        //ASSI Raktai add 2007/05/09
        private bool m_bIsExportExcelDialog = false;


        #endregion

        #region Properties
        public FindKeywordCollection SelectedKeywords
        {
            get
            {
                return m_selectedKeywords;
            }
        }
        public FindOptionDirection OptionDirection
        {
            get { return m_optionDirection; }
            set { m_optionDirection = value; }
        }
        public SelectedRecordCollection SelectedRecords
        {
            get { return m_selectedRecords; }
        }
        public bool MultiSelection
        {
            get { return m_bMultiSelection; }
            set { m_bMultiSelection = value; }
        }
        public bool PreviewOnly
        {
            get { return m_bPreviewOnly; }
            set { m_bPreviewOnly = value; }
        }
        public string MainSQL
        {
            get { return m_strMainSQL; }
            set { m_strMainSQL = value; }
        }
        public bool FindOnStart
        {
            get { return m_bFindOnStart; }
            set { m_bFindOnStart = value; }
        }
        public bool AlreadyHaveWhereStatement
        {
            get { return m_bAlreadyHaveWhereStatement; }
            set { m_bAlreadyHaveWhereStatement = value; }
        }
        public string OptionalSQL
        {
            get { return m_strOptionalSQL; }
            set { m_strOptionalSQL = value; }
        }
        public FindOptionCollection FindOptions
        {
            get { return m_findOptions; }
            set { m_findOptions = value; }
        }
        public CommandType SelectedCommandType
        {
            get { return m_commandType; }
            set { m_commandType = value; }
        }
        public static char MultiValueSeperator { get { return m_cMultiValueSeperator; } set { m_cMultiValueSeperator = value; } }
        public Size RememberSize
        {
            get
            {
                return m_szRemember;
            }
            set
            {
                m_szRemember = value;
            }
        }
        //public ResultFormatCollection ResultFormats {
        //    get { return m_resultFormats; }
        //    set { m_resultFormats = value; }
        //}

        public Button BtnSearch
        {
            get { return btnSearch; }
            set { btnSearch = value; }
        }
        public Button BtnLoad
        {
            get { return btnLoad; }
            set { btnLoad = value; }
        }
        //Add By Sansanee K. 25 Mar 2011
        public Button BtnClose
        {
            get { return btnClose; }
            set { btnClose = value; }
        }
        public bool ExportExcel
        {
            get { return m_bIsExportExcelDialog; }
            set { m_bIsExportExcelDialog = value; }
        }
        public bool ShowPageNavigator
        {
            get { return pageNavigator.Visible; }
            set { pageNavigator.Visible = value; }
        }

        
        #endregion Properties

        #region FindDialog Constructor
        public FindDialog()
            : this(string.Empty, null)
        {
        }
        public FindDialog(string strCaption, IFindDialogDAO findDao)
            : base()
        {
            try
            {
                InitializeComponent();

                m_findDialogDAO = findDao;
                //m_strMainSQL = strMainSQL;
                m_szRemember = this.Size;

                this.Text = strCaption;
                this.StartPosition = FormStartPosition.CenterParent;
                

                pageNavigator.MaxPage = 1;
                pageNavigator.PagePerScreen = m_iShowPage;
                pageNavigator.CurrentPage = m_iStartPage;
                                
                panelExtTop.Visible = false;
                
                m_findOptions = new FindOptionCollection();

                grdResult.DataSource = null;
                grdResult.AutoGenerateColumns = true;

                //gcResult.DataSource = null;
                //grvResult.Columns.Clear();

                this.CancelButton = btnClose;

                FinishedFillResult += new EventHandler(FindDialog_FinishedFillResult);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(this, ex);
            }
        }

        #endregion

        #region Properties

        public DataGridView ResultGridControl
        {
            get { return grdResult; }
        }       
       
        /// <summary>
        /// The number of record that will be show for each page
        /// </summary>
        public int RowPerPage
        {
            get { return m_iRowPerPage; }
            set
            {
                if (m_iRowPerPage != value)
                {
                    m_iRowPerPage = value;
                }
            }
        }
        /// <summary>
        /// Total page will be show on page navigator
        /// </summary>
        public int ShowPage
        {
            get { return m_iShowPage; }
            set
            {
                if (m_iShowPage != value)
                {
                    m_iShowPage = value;
                    pageNavigator.PagePerScreen = m_iShowPage;
                }
            }
        }

        #endregion
        
        private void CreateFindOptions()
        {
            int iOptionNameWidth = 0;
            int iCompareWidth = 0;
            using (Graphics g = this.CreateGraphics())
            {
                // calculate width
                foreach (FindOption fOption in m_findOptions)
                {
                    int iTmp = (int) g.MeasureString(fOption.Caption + "---", this.Font).Width;
                    if (iTmp > iOptionNameWidth)
                    {
                        iOptionNameWidth = iTmp;
                    }

                    foreach (FindOperator oper in fOption.FindOperators)
                    {
                        //iTmp = (int)g.MeasureString(oper.ToString() + "-", this.Font).Width + SystemInformation.MenuButtonSize.Width;
                        iTmp = (int) g.MeasureString(oper.Display + "-", this.Font).Width + SystemInformation.MenuButtonSize.Width;
                        if (iTmp > iCompareWidth)
                        {
                            iCompareWidth = iTmp;
                        }
                    }
                }
            }

            // calculate height of panel option from textbox
            TextBox txtBoxTemp = new TextBox();
            txtBoxTemp.Text = "TEST";
            int iPanelOptionHeight = txtBoxTemp.Height;
            txtBoxTemp.Dispose();



            // create control
            panelFindOptions.Controls.Clear();
            int iTop = YSPACE_PANEL_OPTION;
            foreach (FindOption fOption in m_findOptions)
            {
                PanelOption pOption = new PanelOption(fOption, iOptionNameWidth, iCompareWidth);
                pOption.Location = new Point(LEFT_OFFSET, iTop);
                pOption.Height = iPanelOptionHeight;
                pOption.OnKeyEnterPressed += new KeyPressEventHandler(pOption_OnKeyEnterPressed);                
                panelFindOptions.Controls.Add(pOption);

                iTop += pOption.Height + YSPACE_PANEL_OPTION;
            }
            // resize find option group
            int iOffset = grpFindOptions.Height - panelFindOptions.Height;
            int iDefaultHeight = btnClear.Bottom + btnClear.Left;
            int iNewHeight = (iDefaultHeight > iTop) ? iDefaultHeight : iTop + 10;

            grpFindOptions.SizeChanged -= new EventHandler(this.grpFindOptions_Resize);
            grpFindOptions.Height = iOffset + iNewHeight;
            grpFindOptions.SizeChanged += new EventHandler(this.grpFindOptions_Resize);
        }

        private void StartSearch(int Curpage)
        {
            //splashScreenManager.ShowWaitForm();
            try
            {
                // Add by Wirachai T. 2007 09 26
                DateTime dtProcessTime = DateTime.Now;
                // =============================

                DataRequest request = new DataRequest(string.Empty);
                FindKeywordCollection keywords = CreateFindKeyWord();
                m_selectedKeywords = keywords;
                if (this.CreateDataRequest != null)
                {
                    CreateDataRequest(ref request, keywords);	// Create request command from outside
                }
                else
                {
                    request = CreateReqeustCommand(keywords);	// Create request command by FindDialog
                }
                // kim change 20070815
                //DataSet ds = m_findDialogDAO.GetDataSet(request);
                if (!IsPropertyEqual(request, m_request))
                {
                    m_ds = m_findDialogDAO.GetDataSet(request);
                    m_request = request;
                }
                //End -------------------------------------------------------

                // Modify by Wirachai T. 2007 09 26 : move from DisplaySearchResult for show real query time
                statusBar1.Panels[1].Text = "Query times " + Convert.ToString(DateTime.Now.Subtract(dtProcessTime));
                // ===========================================================================================

                Curpage = (IsChangeParam(request) ? 1 : Curpage);
                pageNavigator.CurrentPage = Curpage;

                DisplaySearchResult(m_ds, Curpage);

                if (FinishedFillResult != null)
                {
                    FinishedFillResult(grdResult, new EventArgs());
                }                

            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(this, ex);
            }
            finally
            {
            }
            
        }


        #region Event Handlers
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                foreach (Control c in panelFindOptions.Controls)
                {
                    if (c is PanelOption)
                    {
                        PanelOption po = (PanelOption)c;

                        po.ResetState();
                    }
                }

                ResultGridControl.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(this, ex);
            }
            this.Cursor = Cursors.Default;
        }
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            OnUserClickLoadButton(this);
        }       

        private SelectedRecord RowToSelectedRecord(int iRow)
        {
            SelectedRecord dict = new SelectedRecord();

            for (int i = 0; i < grdResult.Columns.Count; ++i)
            {
                string fieldName = grdResult.Columns[i].DataPropertyName;

                object objValue = grdResult.Rows[iRow].Cells[i].Value;
                dict.Add(fieldName, objValue);
            }
            return dict;
        }

        protected virtual void OnUserClickLoadButton(FindDialog sender)
        {
            m_selectedRecords = new SelectedRecordCollection();
            
            if (grdResult.RowCount == 0 || grdResult.SelectedRows.Count <= 0)
            {
                return;
            }

            if (m_bIsExportExcelDialog == true)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    System.Windows.Forms.SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                    SaveFileDialog1.Filter = "Microsoft Office Excel Workbook (*.xls)|*.xls";
                    SaveFileDialog1.InitialDirectory = @"C:/";
                    if (SaveFileDialog1.ShowDialog() == DialogResult.OK && SaveFileDialog1.FileName != String.Empty)
                    {
                        if (File.Exists(SaveFileDialog1.FileName))
                        {
                            File.Delete(SaveFileDialog1.FileName);
                        }

#warning หาวิธี Export Excel
                        //grvResult.ExportToXls(SaveFileDialog1.FileName);

                        MessageDialog.ShowInformationMsg("Export completed");
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog.ShowSystemErrorMsg(this, ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            //end
            
            
            foreach (DataGridViewRow row in grdResult.SelectedRows)
            {                
                m_selectedRecords.Add(RowToSelectedRecord(row.Index));
            }

            if (btnLoad.Visible == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        private void InitialScreen()
        {
            
            //RefreshButtonLoad();

            // set button Load
            btnLoad.Visible = (m_bPreviewOnly == false) && (this.MdiParent == null);
            btnClose.Visible = btnLoad.Visible;
            btnLoad.Enabled = false;
            btnSelectAll.Visible = m_bMultiSelection;
            btnUnSelectAll.Visible = m_bMultiSelection;
        }

        private void RefreshFindConditionLayout()
        {
            int width1 = 0;
            using (Graphics g = this.CreateGraphics())
            {
                Font font = grpFindOptions.Font;
                foreach (Control c in panelFindOptions.Controls)
                {
                    if (c is PanelOption)
                    {                        
                        PanelOption pOption = (PanelOption) c;

                        int iTmpWidth = pOption.ValueType1.GetWidth(g, font);
                        if (iTmpWidth > width1)
                        {
                            width1 = iTmpWidth;
                        }
                    }
                }                
            }


            foreach (Control c in panelFindOptions.Controls)
            {
                if (c is PanelOption)
                {
                    PanelOption po = (PanelOption)c;

                    int iNewWidth = po.SetInputWidth(width1, width1);

                    if (po.ValueType1 != null)
                    {
                        if (po.ValueType1 is DateTimeValueType)
                        {
                            if (po.SelectedValue1 == null)
                            {
                                DateTime dtMinTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                                po.ValueType1.SetValue(dtMinTime);
                            }
                        }
                    }
                    if (po.ValueType2 != null)
                    {
                        if (po.ValueType2 is DateTimeValueType)
                        {
                            if (po.SelectedValue2 == null)
                            {
                                DateTime dtMaxTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                                po.ValueType2.SetValue(dtMaxTime);
                            }
                        }
                    }
                    po.Width = iNewWidth;
                }
            }


            // Re Location for each PanelFindOption by using FindOptionDirection
            ReLocationPanelFindOption();
        }      

        private void ReLocationPanelFindOptionColumns(PanelOption[] pColumn1, PanelOption[] pColumn2, int iColumn1Width)
        {
            try
            {
                // Start to Re-Location for PanelOption
                // Re-Location for First Column
                int iXPosition = LEFT_OFFSET;
                int iYPosition = YSPACE_PANEL_OPTION;
                int iHeightColumn1 = 0, iHeightColumn2 = 0;
                for (int i = 0; i < pColumn1.Length; ++i)
                {
                    if (pColumn1[i] != null)
                    {
                        pColumn1[i].Location = new Point(iXPosition, iYPosition);
                        iYPosition += pColumn1[i].Height + YSPACE_PANEL_OPTION;
                    }
                }
                iHeightColumn1 = iYPosition;
                // Re-Location for Second Column
                iXPosition = LEFT_OFFSET * 2 + iColumn1Width;
                iYPosition = YSPACE_PANEL_OPTION;
                for (int i = 0; i < pColumn2.Length; ++i)
                {
                    if (pColumn2[i] != null)
                    {
                        pColumn2[i].Location = new Point(iXPosition, iYPosition);
                        iYPosition += pColumn2[i].Height + YSPACE_PANEL_OPTION;
                    }
                }

                iHeightColumn2 = iYPosition;
                int iMaxHeight = (iHeightColumn1 > iHeightColumn2) ? iHeightColumn1 : iHeightColumn2;
                // Resize FindOptionGroup
                int iOffset = grpFindOptions.Height - panelFindOptions.Height;
                int iDefaultHeight = btnClear.Bottom + btnClear.Left;
                int iNewHeight = (iDefaultHeight > iMaxHeight) ? iDefaultHeight : iMaxHeight + 10;

                grpFindOptions.SizeChanged -= new EventHandler(this.grpFindOptions_Resize);
                grpFindOptions.Height = iOffset + iNewHeight;
                grpFindOptions.SizeChanged += new EventHandler(this.grpFindOptions_Resize);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(this, ex);
            }
        }      

        /// <summary>
        /// สร้างใหม่
        /// </summary>
        private void ReLocationPanelFindOptionCrossDown()
        {
            /* จัดลำดับ PanelOption แบบซ้าย-ขวา ตามลำดับ และคำนวณความกว้างสูงสุดของคอลัมน์ */
            int iCountControl = 0;
            int iCol1MaxWidth = 0;
            int iCol2MaxWidth = 0;

            List<PanelOption> lstColumn1 = new List<PanelOption>();
            List<PanelOption> lstColumn2 = new List<PanelOption>();
            foreach (Control c in panelFindOptions.Controls)
            {
                if (!(c is PanelOption))  // If not PanelOption will ignore.
                {
                    continue;
                }

                iCountControl++;

                PanelOption op = (PanelOption) c;
                if (iCountControl % 2 == 1)
                {
                    lstColumn1.Add(op);

                    if (iCol1MaxWidth < op.Width)
                        iCol1MaxWidth = op.Width;
                }
                else
                {
                    lstColumn2.Add(op);

                    if (iCol2MaxWidth < op.Width)
                        iCol2MaxWidth = op.Width;
                }
            }

            // จัดวาง Option ให้ตรงตำแหน่งใน Panel
            // แต่ยังไม่มีการขยายขนาดของ Form ให้พอดีกับที่แสดง
            ReLocationPanelFindOptionColumns(lstColumn1.ToArray(), lstColumn2.ToArray(), iCol1MaxWidth);

            this.Width = (iCol1MaxWidth + iCol2MaxWidth) + (LEFT_OFFSET * 2) + panelFindOptionsButton.Width + (SystemInformation.FrameBorderSize.Width * 2);
        }

        private void ReLocationPanelFindOptionCrossDown_old()
        {
            //find date type option
            int iCountMaxWidthPO = 0;
            //Modified by Wirachai T. 2008 10 31
            //int iMaxWidth = 350;
            int iMaxWidth = panelFindOptions.Width / 2;
            // End
            foreach (Control c in panelFindOptions.Controls)
            {
                if (c is PanelOption)
                {
                    if (iMaxWidth < c.Width)
                    {
                        iMaxWidth = c.Width;
                    }
                }
            }
            foreach (Control c in panelFindOptions.Controls)
            {
                if (c is PanelOption)
                {
                    if (iMaxWidth == c.Width)
                    {
                        iCountMaxWidthPO++;
                    }
                }
            }

            // calculate number of control foreach column
            int iCountColumn1 = (int)(m_findOptions.Count / 2) + (int)(m_findOptions.Count % 2) + iCountMaxWidthPO;
            int iCountColumn2 = iCountColumn1;// (int)(m_findOptions.Count / 2);
            // Create array of PanelOption control
            PanelOption[] pColumn1 = new PanelOption[iCountColumn1];
            PanelOption[] pColumn2 = new PanelOption[iCountColumn2];
            int iCountControl = 0;
            int iCol1MaxWidth = 0;
            int iCol2MaxWidth = 0;
            iCountColumn1 = 0;
            iCountColumn2 = 0;
            foreach (Control c in panelFindOptions.Controls)
            {
                if (c is PanelOption)
                {
                    iCountControl++;
                    PanelOption op = (PanelOption)c;
                    if (iCountControl % 2 == 1)
                    {	// column 1
                        pColumn1[iCountColumn1] = op;
                        iCountColumn1++;
                        if (iCol1MaxWidth < op.Width)
                        {
                            iCol1MaxWidth = op.Width;
                        }
                        // Add by Wirachai T. 2007/07/11
                        //iCol1MaxWidth += 20;
                        // End Add
                    }
                    else
                    {	// column 2
                        if (op.Width == iMaxWidth)
                        {
                            pColumn1[iCountColumn1] = op;
                            iCountColumn1++;
                            iCountControl--;
                            continue;
                        }

                        pColumn2[iCountColumn2] = op;
                        iCountColumn2++;
                        if (iCol2MaxWidth < op.Width)
                        {
                            iCol2MaxWidth = op.Width;
                        }
                    }

                }
            }
            // Check is PanelOption can Re-Location with selected FindOptionDirection by calculate with of column1 + column2 must <= panelFindOptions.Width (caculate with OFFSET too)
            int iSumColumnWidth = iCol1MaxWidth + iCol2MaxWidth + LEFT_OFFSET;
            if (false)
            {//(iSumColumnWidth > panelFindOptions.Width) {	// If use 2 column, It can't not place in panelFindOptions. Because some part of control will be invisible. So do nothing in this case
                //Modify by Wirachai T. 20070823
                //PanelOption[] po = new PanelOption[pColumn1.Length + pColumn2.Length];
                //pColumn1.CopyTo(po, 0);
                //pColumn2.CopyTo(po, pColumn1.Length);
                //ReLocationPanelFindOptionColumns(po, new PanelOption[0], iCol1MaxWidth);
                PanelOption[] po = new PanelOption[pColumn1.Length + pColumn2.Length];
                for (int i = 0; i < m_findOptions.Count; i++)
                {
                    if (i % 2 == 0)//even
                    {
                        po[i] = pColumn1[i / 2];
                    }
                    else
                    {
                        po[i] = pColumn2[i / 2];
                    }
                }
                ReLocationPanelFindOptionColumns(po, new PanelOption[0], iCol1MaxWidth);
                //====================================================================
            }
            else
            {
                ReLocationPanelFindOptionColumns(pColumn1, pColumn2, iCol1MaxWidth);
            }
        }
        private void ReLocationPanelFindOption()
        {
            switch (m_optionDirection)
            {
                case FindOptionDirection.Down:	// default, so do nothing
                    break;
                case FindOptionDirection.CrossDown:
                    ReLocationPanelFindOptionCrossDown();
                    break;
            }
        }
        private void FindDialog_Load(object sender, System.EventArgs e)
        {                        
            if (m_bIsDialogLoaded == false)
            {
                this.Size = m_szRemember;

                InitialScreen();

                // create find Options
                CreateFindOptions();
                RefreshFindConditionLayout();

                this.MinimumSize = this.Size;

                btnClose.Height = 23;
                btnLoad.Height = btnClose.Height;
                btnClear.Height = btnClose.Height;
                btnSearch.Height = btnClose.Height;
                btnSelectAll.Height = btnClose.Height;
                btnUnSelectAll.Height = btnClose.Height;
                //if (this.Owner != null)
                //{
                    
                //    Size szClient = this.Owner.ClientSize;
                //    int x = (int)(szClient.Width - this.Width) / 2;
                //    int y = (int)(szClient.Height - this.Height) / 2;
                //    this.Location = new Point(x, y);
                //}
                m_bIsDialogLoaded = true;
            }

            // Find on start
            if (m_bFindOnStart == true)
            {
                StartSearch(m_iStartPage);
                m_bFindOnStart = false;
            }
            //LoadLanguage();
        }

        public void btnSearch_Click(object sender, System.EventArgs e)
        {
            StartSearch(m_iStartPage);
        }
        private void pOption_OnKeyEnterPressed(object sender, KeyPressEventArgs e)
        {

            StartSearch(m_iStartPage);
        }
        
        #endregion

        #region Search Functions
        private FindKeywordCollection CreateFindKeyWord()
        {
            FindKeywordCollection keys = new FindKeywordCollection();
            foreach (Control c in panelFindOptions.Controls)
            {
                if (c is PanelOption)
                {
                    PanelOption po = (PanelOption)c;
                    //Fix and Add by Raktai 2007/05/21----------------------------------------------
                    //if (po.ValueType2 is AFDDateTimeValueType)
                    //{
                    //    //po.ValueType2.SetValue(((DateTime)po.ValueType2.GetValue()[0]).AddDays(1).AddSeconds(-1));
                    //    keys.Add(new FindKeyword(po.KeyName, po.Checked, po.FieldMap, po.SelectedOperator, po.SelectedValue1, new object[] { (((DateTime)po.ValueType2.GetValue()[0]).AddDays(1).AddSeconds(-1)) }));
                    //}
                    //else
                    {
                        keys.Add(new FindKeyword(po.KeyName, po.Checked, po.FieldMap, po.SelectedOperator, po.SelectedValue1, po.SelectedValue2));
                    }
                    //End fix and add---------------------------------------------------------------
                }
            }
            return keys;
        }

        private DataRequest CreateReqeustCommand(FindKeywordCollection keys)
        {
            DataRequest req = null;
            switch (this.SelectedCommandType)
            {
                case CommandType.StoredProcedure:
                    req = m_findDialogDAO.CreateRequestStoreCommand(this.MainSQL, keys);
                    break;
                case CommandType.Text:
                    req = m_findDialogDAO.CreateRequestTextCommand(this.MainSQL, this.AlreadyHaveWhereStatement, this.OptionalSQL, keys);
                    break;
            }
            return req;
        }







        #endregion

        #region Display Search Result
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns>if input = "HelloWorld" it will return as "Hello World"</returns>
        private string ReTextLayout(string strText)
        {
            try
            {
                char chBefore = char.MinValue;
                string strResult = "";
                for (int i = 0; i < strText.Length; ++i)
                {
                    if (chBefore == char.MinValue)
                    {
                        chBefore = strText[i];
                    }
                    else
                    {
                        if (chBefore != ' ' && char.IsUpper(chBefore) == false && char.IsUpper(strText[i]) == true)
                        {
                            strResult += " ";
                        }
                        chBefore = strText[i];
                    }
                    strResult += chBefore;
                }
                return strResult;
            }
            catch (Exception)
            {
                return strText;
            }
        }
        private int[] FillResult(DataTable dt, int Curpage)
        {
            grdResult.MultiSelect = m_bMultiSelection;
            grdResult.DataSource = null;

            //grvResult.OptionsSelection.MultiSelect = m_bMultiSelection;
            //gcResult.DataSource = null;

            DataTable dtPage = dt.Clone();


            // fill data
            int mStart = m_iRowPerPage * (Curpage - 1);
            int mEnd = (Curpage * m_iRowPerPage) - 1;
            if (mEnd > (dt.Rows.Count - 1))
                mEnd = dt.Rows.Count - 1;

            DisplayPage(dt, Curpage);

            for (int i = mStart; i <= mEnd; ++i)
            {
                dtPage.ImportRow(dt.Rows[i]);
            }

            grdResult.DataSource = dtPage;
            //gcResult.DataSource = dtPage;

            // set cell type of data [now support for datetime]
            int iCountColumn = 0;
            foreach (DataColumn cl in dt.Columns)
            {
                if (cl.DataType == typeof(DateTime))
                {
                    grdResult.Columns[iCountColumn].DefaultCellStyle.Format = "dd/MM/yyyy";
                    //grvResult.Columns[iCountColumn].ColumnEdit = GetDateTimeRepositoryItem();
                }
                iCountColumn++;
            }

            // calculate column width
            int[] colwidth = new int[dt.Columns.Count];
            // fill default column width
            for (int i = 0; i < colwidth.Length; ++i)
            {
                colwidth[i] = grdResult.Columns[i].GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true);
                //colwidth[i] = grvResult.CalcColumnBestWidth(grvResult.Columns[i]);
            }

            for (int i = 0; i < dt.Columns.Count; ++i)
            {
                //Modify by Wirachai T. 2007/07/12
                string label = dt.Columns[i].ColumnName;
                //string label = Util.GetFormText(this.FormName, dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                // End Modify

                label = ReTextLayout(label);
                //shtResult.Columns[i + (m_bMultiSelection ? 1 : 0)].Label = label;

                grdResult.Columns[i].HeaderText = label;
                //grvResult.Columns[i].Caption = label;
            }

            // return column width
            return colwidth;
        }

        private void DisplaySearchResult(DataSet ds, int Curpage)
        {
            //DateTime dtProcessTime = DateTime.Now;
            int[] colwidth = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                colwidth = FillResult(ds.Tables[0], Curpage);

            }
            else
            {
                ResultGridControl.DataSource = null;
            }
            // if not have data in result spread then show "data not found" message
            if (grdResult.RowCount <= 0)
            {
                if (m_bFindOnStart == false)
                {	// ถ้าเป็นการ search ตอนที่ form load ขึ้นมา ก็ไม่ต้อง show message ในกรณีที่ไม่เจอข้อมูล
                    MessageDialog.ShowBusinessErrorMsg(this, "Data not found", "Try to change find option and try to find data again");
                }
                m_bIsFoundData = false;
                btnLoad.Enabled = false;
                this.Cursor = Cursors.Default;

                return;
            }
            else
            {
                m_bIsFoundData = true;
                btnLoad.Enabled = true;
                
                grdResult.Focus();
                grdResult.ClearSelection();
                grdResult.Rows[0].Selected = true;
            }


            // Set Auto Column Size
            if (btnSearch.Tag == null)
            {
                for (int i = 0; i < colwidth.Length; ++i)
                {
                    //grvResult.Columns[i].Width = colwidth[i];
                    grdResult.Columns[i].Width = colwidth[i];
                }
                btnSearch.Tag = true;
            }

            // Set Result Format
            SetResultFormat(grdResult.Columns);



            statusBar1.Panels[0].Text = "Records" + Convert.ToString(grdResult.RowCount);
            // Modify : Move to StartSearch Function for show real query time
            //statusBar1.Panels[1].Text = "Query times " + Convert.ToString(DateTime.Now.Subtract(dtProcessTime));
            //=============================

            if (grdResult.RowCount > 0)
            {
                grdResult.Focus();
                grdResult.ClearSelection();
                grdResult.Rows[0].Selected = true;
                btnLoad.Enabled = true;
            }
            else
            {
                btnLoad.Enabled = false;
            }
        }

        protected virtual void SetResultFormat(DataGridViewColumnCollection columns)
        {            
            if (columns == null)
            {
                return;
            }
        }

        #endregion

        protected virtual void FindDialog_FinishedFillResult(object sender, EventArgs e)
        {

        }

        

        protected virtual void grvResult_DoubleClick(object sender, EventArgs e)
        {
            if (grdResult.RowCount <= 0)
            {
                return;
            }
            if (m_bMultiSelection == false)
            {
                if (btnLoad.Enabled == true)
                {
                    OnUserClickLoadButton(this);
                }
            }
        }

        protected virtual void grvResult_Click(object sender, EventArgs e)
        {
            //if (m_bMultiSelection == false) {
            //    if (btnLoad.Enabled == true) {
            //        OnUserClickLoadButton();
            //    }
            //}
        }
        
        private void grvResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (btnLoad.Enabled == true)
                {
                    OnUserClickLoadButton(this);
                }
            }
        }

        private void btnSelectAll_Click(object sender, System.EventArgs e)
        {
            grdResult.SelectAll();
        }

        private void btnUnSelectAll_Click(object sender, System.EventArgs e)
        {
            grdResult.ClearSelection();
            if (grdResult.RowCount > 0)
            {
                grdResult.Rows[0].Selected = true;
            }
        }

        
        // Change Event to Form ResizeEnd
        private void grpFindOptions_Resize(object sender, System.EventArgs e)
        {            
            //if (m_optionDirection == FindOptionDirection.CrossDown) {
            //    ReLocationPanelFindOption();
            //}
            // End
        }

        private void FindDialog_ResizeEnd(object sender, EventArgs e)
        {
            if (m_optionDirection == FindOptionDirection.CrossDown)
            {
                ReLocationPanelFindOption();
                pageNavigator.Invalidate();
            }
        }        

        private void FindDialog_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void DisplayPage(DataTable dt, int iCurpage)
        {
            int iNumpage; // จำนวน page

            iNumpage = (int)(dt.Rows.Count / m_iRowPerPage);
            iNumpage += ((dt.Rows.Count % m_iRowPerPage) == 0 ? 0 : 1);

            pageNavigator.MaxPage = iNumpage;

        }

        private bool IsChangeParam(DataRequest request)
        {
            m_strParam = string.Empty;
            foreach (DataRequest.Parameter parm in request.Parameters)
            {
                m_strParam += parm.Value.ToString();
            }
            if (!m_OldstrParam.Equals(m_strParam))
            {
                m_OldstrParam = m_strParam;
                return true;
            }
            m_OldstrParam = m_strParam;
            return false;
        }

      
        private void pageNavigator_OnPageClick(int pageNumber)
        {
            StartSearch(pageNumber);
        }

        //kimmik add 20070815---------------------------------
        /// <summary>
        /// เปรียบเทียบ 2 obj โดยใช้ค่าใน Property แต่ละตัว
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        private bool IsPropertyEqual(DataRequest req1, DataRequest req2)
        {

            if (req1 == null && req2 == null)
            {
                return true;
            }

            if ((req1 == null && req2 != null) || (req1 != null && req2 == null))
            {
                return false;
            }

            if (req1.CommandText != req2.CommandText)
            {
                return false;
            }
            if (req1.CommandType != req2.CommandType)
            {
                return false;
            }
            if (req1.Parameters.Count != req2.Parameters.Count)
            {
                return false;
            }
            for (int index = 0; index < req1.Parameters.Count; index++)
            {
                if (req1.Parameters[index].Value != req2.Parameters[index].Value)
                {
                    return false;
                }
                if (req1.Parameters[index].Name != req2.Parameters[index].Name)
                {
                    return false;
                }
                if (req1.Parameters[index].ParameterType != req2.Parameters[index].ParameterType)
                {
                    return false;
                }
                if (req1.Parameters[index].Direction != req2.Parameters[index].Direction)
                {
                    return false;
                }
            }
            return true;
        }        
                        
        //END        
    }
}
