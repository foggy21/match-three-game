using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchThreeGame
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Show();
            Hide();
        }
    }
}
