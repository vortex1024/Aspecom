using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aspecon
{
    public partial class Form1 : Form
    {
        Aspecom aspecon;
        public Form1()
        {
            InitializeComponent();
            aspecon = new Aspecom();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                aspecon.ReadFirstSpectrum(dlgOpen.FileName);

            }

        }

        private void btnSecond_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                aspecon.ReadSecondSpectrum(dlgOpen.FileName);

            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            double s=0;
            aspecon.CompareSpectra();
foreach(SpectrumLine sl in aspecon.resultSpectrum)
            {
                txtResult.AppendText(sl.Frequency + " " + sl.Amplitude+"\n");
                s += sl.Amplitude;
            }
MessageBox.Show(s.ToString());
        }
    }
}
