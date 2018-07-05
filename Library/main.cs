using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class main : Form
    {
        private Form wm1 = null;
        private Form wm2 = null;
        private Form wm3 = null;
        private Form wm4 = null;
        static public int userid;
        static public int rolenum;
        public main()
        {
            InitializeComponent();
            if(rolenum == 1) { toolStripButton2.Visible = false; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (wm1 == null)
            {
                wm1 = new bookSearch();
                wm1.MdiParent = this;
            }
                wm1.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (wm2 == null) {
                wm2 = new bookManage();
                wm2.MdiParent = this;
            }
            wm2.Show();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (wm3 == null) {
                wm3 = new bookRent();
                wm3.MdiParent = this;
            }
            wm3.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (wm4 == null) {
                wm4 = new bookReturn();
                wm4.MdiParent = this;
            }
            wm4.Show();
        }
    }
}
