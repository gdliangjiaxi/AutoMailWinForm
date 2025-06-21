using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMailWinForm
{
    public partial class FrmTest : Form
    {


        int a = 10;
        public FrmTest()
        {
            InitializeComponent();
        }



        private void FrmTest_Load(object sender, EventArgs e)
        {
           String a = EncryptAndDecrypt.Encode("Server=IZ7DTQV1EY4OFCZ;Database=AutoMailDB;Trusted_Connection=True");
            this.label1.Text = a;
        }
    }
}
