namespace MatchThreeGame
{
    abstract class Element : ICloneable
    {
        public Button Button { get; set; }

        protected Image tileSet;
        protected Image imageOfButton;
        protected Graphics graphics;
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

        public virtual void SetImage()
        {
            SetInstanceOfImage(out tileSet, out imageOfButton, out graphics);

            ElementForm = GetRandomTypeOfElement(0, TypesOfElements.Count() - 1);
            int[,] position = TypesOfElements[ElementForm];

            Rectangle cropGem = new Rectangle(position[0, 0] * 128, position[0, 1] * 128, 128, 128);
            graphics.DrawImage(tileSet, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
            Button.BackgroundImage = imageOfButton;
        }

        public void SetImage(TypeOfElement typeOfElement)
        {
            SetInstanceOfImage(out tileSet, out imageOfButton, out graphics);

            ElementForm = typeOfElement;
            int[,] position = TypesOfElements[ElementForm];

            if (position != null)
            {
                Rectangle cropGem = new Rectangle(position[0, 0] * 128, position[0, 1] * 128, 128, 128);
                graphics.DrawImage(tileSet, new Rectangle(0, 0, 128, 128), cropGem, GraphicsUnit.Pixel);
                Button.BackgroundImage = imageOfButton;
            } 
            else
            {
                Button.BackgroundImage = null;
            }
        }

        protected void SetInstanceOfImage(out Image tileSet, out Image image, out Graphics graphics)
        {
            tileSet = new Bitmap(Resources.gems);
            image = new Bitmap(128, 128, tileSet.PixelFormat);
            graphics = Graphics.FromImage(image);
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
        public enum TypeOfBonus
        {
            Line,
            Bomb,
            None,
        }
    }
}
