using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Numbers.IService;
using Numbers.Service;

namespace MyChequeApp
{
    public partial class frmMyCheque : Form
    {
        public frmMyCheque()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            generateWords();
        }

        private void chkCurrency_CheckedChanged(object sender, EventArgs e)
        {
            generateWords();
        }

        private void generateWords()
        {
            IUtils loUtils = new Utils();
            string lsWords = string.Empty;

            loUtils.NumberToWords(txtInput.Text, out lsWords, chkCurrency.Checked);
            lblOutput.Text = lsWords.ToUpper();
        }
    }
}
