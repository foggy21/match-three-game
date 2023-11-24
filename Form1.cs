namespace MatchThreeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form2();
            frm.FormClosed += CloseMain;
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Show();
            Hide();
        }

        private void CloseMain(object? sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}