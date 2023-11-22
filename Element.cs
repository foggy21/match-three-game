namespace MatchThreeGame
{
    abstract class Element : ICloneable
    {
        public Button Button { get; set; }

        public TypeOfElement ElementForm { get; set; }

        private Dictionary<TypeOfElement, int[,]> TypesOfElements { get => new Dictionary<TypeOfElement, int[,]>()
            {
                {TypeOfElement.Square, new int[,] { { 0, 0 } } },
                {TypeOfElement.Circle, new int[,] { { 1, 0 } } },
                {TypeOfElement.Rhombus, new int[,] { { 2, 0 } } },
                {TypeOfElement.Star, new int[,] { { 3, 0 } } },
                {TypeOfElement.Hexagon, new int[,] { { 0, 1 } } },
                {TypeOfElement.None, null },
            };
        }

        public void SetRandomImage()
        {
            Image imageGems = new Bitmap(Resources.gems);
            Image imageGem = new Bitmap(128, 128, imageGems.PixelFormat);
            Graphics g = Graphics.FromImage(imageGem);

            ElementForm = GetRandomTypeOfElement(0, 5);
            int[,] position = TypesOfElements[ElementForm];

            Rectangle cropGem = new Rectangle(position[0, 0] * 128, position[0, 1] * 128, 128, 128);
            g.DrawImage(imageGems, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
            Button.BackgroundImage = imageGem;
        }

        public void SetImageOfType(TypeOfElement typeOfElement)
        {
            Image imageGems = new Bitmap(Resources.gems);
            Image imageGem = new Bitmap(128, 128, imageGems.PixelFormat);
            Graphics g = Graphics.FromImage(imageGem);

            ElementForm = typeOfElement;
            int[,] position = TypesOfElements[ElementForm];
            if (position != null)
            {
                Rectangle cropGem = new Rectangle(position[0, 0] * 128, position[0, 1] * 128, 128, 128);
                g.DrawImage(imageGems, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
                Button.BackgroundImage = imageGem;
            }
            else
            {
                Button.BackgroundImage = null;
            }

            
        }

        private TypeOfElement GetRandomTypeOfElement(int minValue, int maxValue)
        {
            Random random = new Random();
            TypeOfElement randomElementPosition = (TypeOfElement)random.Next(minValue, maxValue);
            return randomElementPosition;
        }
        
        public abstract void Destroy();

        public object Clone() => MemberwiseClone();

        public enum TypeOfElement
        {
            Square,
            Circle,
            Rhombus,
            Star,
            Hexagon,
            None,
        }
    }
}
