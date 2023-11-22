namespace MatchThreeGame
{
    abstract class Element : ICloneable
    {
        public Button Button { get; set; }

        public Dictionary<int, int[,]> TilesetPositions { get => new Dictionary<int, int[,]>()
            {
                {0, new int[,] { { 0, 0 } } },
                {1, new int[,] { { 1, 0 } } },
                {2, new int[,] { { 2, 0 } } },
                {3, new int[,] { { 3, 0 } } },
                {4, new int[,] { { 0, 1 } } },
            };
        }

        public void SetRandomImage()
        {
            Image imageGems = new Bitmap(Resources.gems);
            Image imageGem = new Bitmap(128, 128, imageGems.PixelFormat);
            Graphics g = Graphics.FromImage(imageGem);

            int positionOfImage = GetRandomPositionForTileset(0, 5);
            int[,] position = TilesetPositions[positionOfImage];

            Rectangle cropGem = new Rectangle(position[0, 0] * 128, position[0, 1] * 128, 128, 128);
            g.DrawImage(imageGems, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
            Button.BackgroundImage = imageGem;
        }

        private int GetRandomPositionForTileset(int minValue, int maxValue)
        {
            Random random = new Random();
            int randomElementPosition = random.Next(minValue, maxValue);
            return randomElementPosition;
        }
        
        public abstract void Destroy();

        public object Clone() => MemberwiseClone();
    }
}
