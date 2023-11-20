namespace MatchThreeGame
{
    abstract class Element
    {
        public Button Button { get; set; }

        public Dictionary<int, int[,]> ElementPostion { get => new Dictionary<int, int[,]>()
            {
                {0, new int[,] { { 0, 0 } } },
                {1, new int[,] { { 1, 0 } } },
                {2, new int[,] { { 2, 0 } } },
                {3, new int[,] { { 3, 0 } } },
                {4, new int[,] { { 0, 1 } } },
            };
        }

        public void SetImageRandom()
        {
            Random random = new Random();
            int randomElement = random.Next(0, 5);
            Image gems = new Bitmap(Resources.gems);
            Image gem = new Bitmap(128, 128, gems.PixelFormat);
            Graphics g = Graphics.FromImage(gem);
            int[,] position = ElementPostion[randomElement];
            Rectangle cropGem = new Rectangle(position[0, 0] * 128, position[0, 1] * 128, 128, 128);
            g.DrawImage(gems, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
            Button.BackgroundImage = gem;
        }
        public abstract void Destroy();


    }
}
