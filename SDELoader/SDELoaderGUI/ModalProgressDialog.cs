using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDELoader
{
    public partial class ModalProgressDialog : Form
    {
        public ModalProgressDialog()
        {       
            InitializeComponent();            
            this.ultraProgressBar1.Text = "";
            
        }

        public void Reset(string description)
        {
            this.Show();
            this.ultraProgressBar1.Maximum = 100;            
            this.ultraProgressBar1.Step = 1;
            this.ultraLabel1.Text = description;
        }
        public void UpdateProgress(int newValue)
        {
            this.ultraProgressBar1.Value = newValue;
            this.ultraProgressBar1.Text = ultraProgressBar1.Value + " of " + ultraProgressBar1.Maximum;
        }

    }
}