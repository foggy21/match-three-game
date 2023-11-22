using System.Drawing;
using System.Windows.Forms;

namespace MatchThreeGame
{
    class Map
    {
        const int sizeOfMap = 8;
        const int sizeOfImage = 128;

        public static Element[,] map = new Element[sizeOfMap, sizeOfMap];

        private static Button prevPressedButton;


        private static Map? instance;
        private Map() { }

        private static Map getInstance()
        {
            if (instance == null)
            {
                instance = new Map();
            }
            return instance;
        }

        public static Map? CreateMap()
        {
            var instance = getInstance();
            if (instance != null)
            {
                for (int i = 0; i < sizeOfMap; i++)
                {
                    for (int j = 0; j < sizeOfMap; j++)
                    {
                        Element element = new DefaultElement();
                        SetUpElement(element, j, i);

                        map[i, j] = element;
                    }
                }
            }
            return instance;
        }

        private static void SetUpElement(Element element, int posX, int posY)
        {
            element.Button = new Button();
            element.Button.Size = new Size(sizeOfImage, sizeOfImage);
            element.Button.Location = new Point(posX * sizeOfImage, posY * sizeOfImage);
            element.Button.Click += new EventHandler(OnElementPress);
            element.SetRandomImage();

        }

        public static void OnElementPress(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if (prevPressedButton != null)
            {
                prevPressedButton.BackColor = SystemColors.ControlLight;
                if (IsButtonsNear(prevPressedButton, pressedButton))
                {
                    Image tempImage = (Image)map[pressedButton.Location.Y / 128, pressedButton.Location.X / 128].Button.BackgroundImage.Clone();
                    
                    map[pressedButton.Location.Y / 128, pressedButton.Location.X / 128].Button.BackgroundImage = (Image)map[prevPressedButton.Location.Y / 128, prevPressedButton.Location.X / 128].Button.BackgroundImage.Clone();
                    map[prevPressedButton.Location.Y / 128, prevPressedButton.Location.X / 128].Button.BackgroundImage = (Image)tempImage.Clone();
                }
            }
            prevPressedButton = pressedButton;
            pressedButton.BackColor = Color.Red;

        }


        private static bool IsButtonsNear(Button fisrtButton, Button secondButton)
        {
            if ((fisrtButton.Location.X - 128 == secondButton.Location.X || 
                fisrtButton.Location.X + 128 == secondButton.Location.X)
                && fisrtButton.Location.Y == secondButton.Location.Y)
            {
                return true;
            } else if ((fisrtButton.Location.Y - 128 == secondButton.Location.Y ||
                fisrtButton.Location.Y + 128 == secondButton.Location.Y)
                && fisrtButton.Location.X == secondButton.Location.X)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
