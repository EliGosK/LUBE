using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAP.Framework.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
    public partial class WaitForm : WaitFormBase
    {
        #region Constructor

        public WaitForm()
        {
            InitializeComponent();
            ResizeMode = eResizeMode.GrowAndShrink;
        }        

        #endregion

        #region Overriden Method

        public override void SetCaption(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action) (() =>
                {
                    SetCaption(text);
                }));
            }
            else
            {
                lblCaption.Text = text;

                // Auto resize Width to fitting text.
                RecalcSize();
            }
        }

        public override void SetDescription(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    SetDescription(text);
                }));
            }
            else
            {
                lblDescription.Text = text;

                // Auto resize Width to fitting text.
                RecalcSize();
            }
        }

        #endregion

        #region Properties

        public eResizeMode ResizeMode { get; set; }


        #endregion


        protected void RecalcSize()
        {
            if (ResizeMode != eResizeMode.NoResize)
            {
                // Caption
                Size sizeCaption = MeasureStringWidth(lblCaption.Text, lblCaption.Font, new Size(600, 600));
                int newWidth = lblCaption.Left + sizeCaption.Width + 5;


                // Description
                Size maxSize = new Size(sizeCaption.Width, 600);
                Size sizeDescription = MeasureStringWidth(lblDescription.Text, lblDescription.Font, maxSize);

                int heightDiff = sizeDescription.Height - lblDescription.Height;
                int newHeight = this.Height + heightDiff;


                if (ResizeMode == eResizeMode.GrowAndShrink)
                {
                    this.Width = newWidth;
                    this.Height = newHeight;
                }
                else if (ResizeMode == eResizeMode.GrowOnly)
                {
                    if (this.Width < newWidth)
                        this.Width = newWidth;

                    if (this.Height < newHeight)
                        this.Height = newHeight;
                }

                SetCenterScreen();
            }
        }

        private Size MeasureStringWidth(string text, Font font)
        {
            Size maxSize = new Size(300, 600);
            return MeasureStringWidth(text, font, maxSize);
        }

        private Size MeasureStringWidth(string text, Font font, Size maxSize)
        {
            using (var gfx = this.CreateGraphics())
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Near;

                    return gfx.MeasureString(text, font, maxSize, sf).ToSize();
                }
            }
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            lblDescription.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        }    
    }
}
