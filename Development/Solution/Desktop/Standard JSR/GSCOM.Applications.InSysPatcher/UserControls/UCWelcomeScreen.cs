using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSCOM.Applications.InSysPatcher.UserControls
{
    public partial class UCWelcomeScreen : ControlBase
    {
        private Forms.MainForm mform;

        public UCWelcomeScreen(Forms.MainForm mform)
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;

            this.mform = mform;
        }

        public override void LoadControl()
        {
            this.lblProduct.Text = this.mform.AppName;
            this.lblVersion.Text = this.mform.AppVersion;
            this.lblBuild.Text = this.mform.AppBuild;
        }

    }
}
