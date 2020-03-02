using GSCOM.Applications.InSysPatcher.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSCOM.Applications.InSysPatcher.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //int CurIndex = 0;
        //int MaxIndex = 1;

        public string AppName { get; private set; }
        public string AppVersion { get; private set; }
        public string AppBuild { get; private set; }
        public string AppPath { get; private set; }
        public string AppServer { get; private set; }
        

        private List<Control> mList = new List<Control>();

        private UCWelcomeScreen m1;
        private UCProcess m2;

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
              
                InitParameters();
                InitList();
                SelectIndex(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitParameters()
        {
            foreach (string str in Environment.GetCommandLineArgs())
            {
                switch (str.Substring(0, 2))
                {
                    case "-n": this.AppName = str.Substring(2); break;
                    case "-v": this.AppVersion = str.Substring(2); break;
                    case "-b": this.AppBuild =  (str.Substring(2) == "") ? "(unavailable)" : str.Substring(2); break;
                    case "-p": this.AppPath = str.Substring(2); break;
                    case "-s": this.AppServer = str.Substring(2); break;
                }
            }
        }

        void InitList()
        {
            m1 = new UCWelcomeScreen(this);
            m2 = new UCProcess(this);

            mList.AddRange(new Control[] { m1, m2 });
        }

        void SelectIndex(int index)
        {
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(mList[index]);

            this.btnBack.Visible = (index == 0) ? false : true;
            this.btnNext.Text = (index == mList.Count() - 1) ? "Finish" : "Next";

            ((ControlBase)mList[index]).LoadControl();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to cancel?", Application.ProductName, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int CurIndex = 0;
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Text == "Finish")
            {
                Application.Exit();
            }
            else
            {
                CurIndex++;
                SelectIndex(CurIndex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CurIndex--;
            SelectIndex(CurIndex);
        }

    }
}
