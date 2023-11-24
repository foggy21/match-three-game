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
    public partial class Form2 : Form
    {
        private int remainingSeconds = 61;
        public Form2()
        {
            InitializeComponent();
            
            Init();

        }

        public void Init()
        {
            Map.CreateMap();
            Map.scoreHandler += DisplayScores;
            foreach (Element elem in Map.map)
            {
                Controls.Add(elem.Button);
            }

        }

        private void DisplayScores(int scores)
        {
            score.Text = "Score: " + scores.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            timerLabel.Text = $"Remaining seconds: {remainingSeconds} sec.";

            if (remainingSeconds == 0)
            {
                var frm = new Form3();
                frm.FormClosed += CloseMain;
                frm.Location = Location;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Show();
                Hide();
            }
        }

        private void CloseMain(object? sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}
