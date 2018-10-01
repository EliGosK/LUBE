using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP.Framework.Windows
{
    public partial class InformationMessage : Form
    {
        public InformationMessage()
        {
            InitializeComponent();

            this.ptbIcon.Image = SystemIcons.Information.ToBitmap();

            this.StartPosition = FormStartPosition.Manual;
            Point start = new Point();
            start.X = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            start.Y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;

            this.Location = start;

        }

        public string Title { set { this.lblTitle.Text = value; } }
        public string Message { set { this.lblMessage.Text = value; } }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrCloseWindows_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InformationMessage_Load(object sender, EventArgs e)
        {
            tmrCloseWindows.Start();
        }

    }
}
