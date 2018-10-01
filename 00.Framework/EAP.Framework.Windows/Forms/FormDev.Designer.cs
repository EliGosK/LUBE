using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
    public partial class FormDev
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDev));
            this.m_toolBar = new System.Windows.Forms.ToolStrip();
            this.m_toolBarClose = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarFind = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBarAdd = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarEdit = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarDelete = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBarSave = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarCancel = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBarRefresh = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBarPrint = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarImport = new System.Windows.Forms.ToolStripButton();
            this.m_toolBarExport = new System.Windows.Forms.ToolStripButton();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolBar
            // 
            this.m_toolBar.AutoSize = false;
            this.m_toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolBarClose,
            this.m_toolBarFind,
            this.m_toolBarSep1,
            this.m_toolBarAdd,
            this.m_toolBarEdit,
            this.m_toolBarDelete,
            this.m_toolBarSep2,
            this.m_toolBarSave,
            this.m_toolBarCancel,
            this.m_toolBarSep3,
            this.m_toolBarRefresh,
            this.m_toolBarSep4,
            this.m_toolBarPrint,
            this.m_toolBarImport,
            this.m_toolBarExport});
            this.m_toolBar.Location = new System.Drawing.Point(0, 0);
            this.m_toolBar.Name = "m_toolBar";
            this.m_toolBar.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.m_toolBar.Size = new System.Drawing.Size(1028, 47);
            this.m_toolBar.TabIndex = 0;
            // 
            // m_toolBarClose
            // 
            this.m_toolBarClose.AutoSize = false;
            this.m_toolBarClose.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarClose;
            this.m_toolBarClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarClose.Name = "m_toolBarClose";
            this.m_toolBarClose.Size = new System.Drawing.Size(67, 35);
            this.m_toolBarClose.Text = " Close";
            this.m_toolBarClose.Click += new System.EventHandler(this.m_toolBarClose_Click);
            // 
            // m_toolBarFind
            // 
            this.m_toolBarFind.AutoSize = false;
            this.m_toolBarFind.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarFind;
            this.m_toolBarFind.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarFind.Name = "m_toolBarFind";
            this.m_toolBarFind.Size = new System.Drawing.Size(56, 35);
            this.m_toolBarFind.Text = " Find ";
            this.m_toolBarFind.Click += new System.EventHandler(this.m_toolBarFind_Click);
            // 
            // m_toolBarSep1
            // 
            this.m_toolBarSep1.AutoSize = false;
            this.m_toolBarSep1.Name = "m_toolBarSep1";
            this.m_toolBarSep1.Size = new System.Drawing.Size(10, 47);
            // 
            // m_toolBarAdd
            // 
            this.m_toolBarAdd.AutoSize = false;
            this.m_toolBarAdd.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarAdd;
            this.m_toolBarAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarAdd.Name = "m_toolBarAdd";
            this.m_toolBarAdd.Size = new System.Drawing.Size(63, 35);
            this.m_toolBarAdd.Text = " Add ";
            this.m_toolBarAdd.Click += new System.EventHandler(this.m_toolBarAdd_Click);
            // 
            // m_toolBarEdit
            // 
            this.m_toolBarEdit.AutoSize = false;
            this.m_toolBarEdit.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarEdit;
            this.m_toolBarEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarEdit.Name = "m_toolBarEdit";
            this.m_toolBarEdit.Size = new System.Drawing.Size(61, 35);
            this.m_toolBarEdit.Text = " Edit ";
            this.m_toolBarEdit.Click += new System.EventHandler(this.m_toolBarEdit_Click);
            // 
            // m_toolBarDelete
            // 
            this.m_toolBarDelete.AutoSize = false;
            this.m_toolBarDelete.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarDelete;
            this.m_toolBarDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarDelete.Name = "m_toolBarDelete";
            this.m_toolBarDelete.Size = new System.Drawing.Size(74, 35);
            this.m_toolBarDelete.Text = " Delete ";
            this.m_toolBarDelete.Click += new System.EventHandler(this.m_toolBarDelete_Click);
            // 
            // m_toolBarSep2
            // 
            this.m_toolBarSep2.AutoSize = false;
            this.m_toolBarSep2.Name = "m_toolBarSep2";
            this.m_toolBarSep2.Size = new System.Drawing.Size(10, 47);
            // 
            // m_toolBarSave
            // 
            this.m_toolBarSave.AutoSize = false;
            this.m_toolBarSave.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarSave;
            this.m_toolBarSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarSave.Name = "m_toolBarSave";
            this.m_toolBarSave.Size = new System.Drawing.Size(65, 35);
            this.m_toolBarSave.Text = " Save ";
            this.m_toolBarSave.Click += new System.EventHandler(this.m_toolBarSave_Click);
            // 
            // m_toolBarCancel
            // 
            this.m_toolBarCancel.AutoSize = false;
            this.m_toolBarCancel.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarCancel;
            this.m_toolBarCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarCancel.Name = "m_toolBarCancel";
            this.m_toolBarCancel.Size = new System.Drawing.Size(77, 35);
            this.m_toolBarCancel.Text = " Cancel ";
            this.m_toolBarCancel.Click += new System.EventHandler(this.m_toolBarCancel_Click);
            // 
            // m_toolBarSep3
            // 
            this.m_toolBarSep3.AutoSize = false;
            this.m_toolBarSep3.Name = "m_toolBarSep3";
            this.m_toolBarSep3.Size = new System.Drawing.Size(10, 47);
            // 
            // m_toolBarRefresh
            // 
            this.m_toolBarRefresh.AutoSize = false;
            this.m_toolBarRefresh.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarRefresh;
            this.m_toolBarRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarRefresh.Name = "m_toolBarRefresh";
            this.m_toolBarRefresh.Size = new System.Drawing.Size(80, 35);
            this.m_toolBarRefresh.Text = " Refresh ";
            this.m_toolBarRefresh.Click += new System.EventHandler(this.m_toolBarRefresh_Click);
            // 
            // m_toolBarSep4
            // 
            this.m_toolBarSep4.AutoSize = false;
            this.m_toolBarSep4.Name = "m_toolBarSep4";
            this.m_toolBarSep4.Size = new System.Drawing.Size(10, 47);
            // 
            // m_toolBarPrint
            // 
            this.m_toolBarPrint.AutoSize = false;
            this.m_toolBarPrint.Image = global::EAP.Framework.Windows.Properties.Resources.Printer;
            this.m_toolBarPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarPrint.Name = "m_toolBarPrint";
            this.m_toolBarPrint.Size = new System.Drawing.Size(66, 35);
            this.m_toolBarPrint.Text = " Print ";
            this.m_toolBarPrint.Click += new System.EventHandler(this.m_toolBarPrint_Click);
            // 
            // m_toolBarImport
            // 
            this.m_toolBarImport.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarImport;
            this.m_toolBarImport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarImport.Name = "m_toolBarImport";
            this.m_toolBarImport.Size = new System.Drawing.Size(74, 44);
            this.m_toolBarImport.Text = " Import";
            this.m_toolBarImport.Click += new System.EventHandler(this.m_toolBarImport_Click);
            // 
            // m_toolBarExport
            // 
            this.m_toolBarExport.AutoSize = false;
            this.m_toolBarExport.Image = global::EAP.Framework.Windows.Properties.Resources.ToolbarExport;
            this.m_toolBarExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBarExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBarExport.Name = "m_toolBarExport";
            this.m_toolBarExport.Size = new System.Drawing.Size(74, 35);
            this.m_toolBarExport.Text = " Export ";
            this.m_toolBarExport.Click += new System.EventHandler(this.m_toolBarExport_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 47);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1028, 699);
            this.pnlContainer.TabIndex = 1;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // FormDev
            // 
            this.ClientSize = new System.Drawing.Size(1028, 746);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.m_toolBar);
            this.Name = "FormDev";
            this.Text = "Form Dev";
            this.Shown += new System.EventHandler(this.FormDev_Shown);
            this.m_toolBar.ResumeLayout(false);
            this.m_toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Window Control Variables
        public ToolStrip m_toolBar;
        public ToolStripButton m_toolBarClose;
        public ToolStripSeparator m_toolBarSep1;
        public ToolStripButton m_toolBarFind;
        public ToolStripButton m_toolBarAdd;
        public ToolStripButton m_toolBarEdit;
        public ToolStripButton m_toolBarDelete;
        public ToolStripSeparator m_toolBarSep2;
        public ToolStripButton m_toolBarSave;
        public ToolStripButton m_toolBarCancel;
        public ToolStripSeparator m_toolBarSep3;
        public ToolStripButton m_toolBarRefresh;
        public ToolStripButton m_toolBarPrint;
        public ToolStripButton m_toolBarExport;
        #endregion

        public Panel pnlContainer;
        protected ErrorProvider errorProvider;
        public ToolStripButton m_toolBarImport;
        public ToolStripSeparator m_toolBarSep4;
    }
}
