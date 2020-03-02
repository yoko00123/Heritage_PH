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
    public partial class UCProcess : ControlBase
    {
        private Forms.MainForm mform;

        public UCProcess(Forms.MainForm mform)
        {
            InitializeComponent();
            this.mform = mform;
        }

        public override void LoadControl()
        {
            
        }
    }
}
