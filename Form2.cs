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
        public Form2()
        {
            InitializeComponent();
            //Image gems = new Bitmap(Resources.gems);
            //Image gem = new Bitmap(128, 128, gems.PixelFormat);
            //Graphics g = Graphics.FromImage(gem);
            //Rectangle cropGem = new Rectangle(0, 0, 128, 128);
            //g.DrawImage(gems, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
            //button1.BackgroundImage = Image;

            Init();
        }

        public void Init()
        {
            Map.CreateMap();
            foreach (Element elem in Map.map)
            {
                Controls.Add(elem.Button);
            }
        }



    }
}
