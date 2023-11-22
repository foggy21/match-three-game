using System.Drawing;
using System.Windows.Forms;

namespace MatchThreeGame
{
    class Map
    {
        const int sizeOfMap = 8;
        const int sizeOfImage = 128;
        
        public static Element[,] map = new Element[sizeOfMap, sizeOfMap];

        private static Button? prevPressedButton;
        public static int Score { get; private set; }

        public delegate void ScoreHandler(int score);
        public static event ScoreHandler scoreHandler;

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
            Score = 0;
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
            Button? pressedButton = sender as Button;

            if (prevPressedButton != null)
            {
                prevPressedButton.BackColor = SystemColors.ControlLight;
                if (IsButtonsNear(prevPressedButton, pressedButton))
                {
                    ChangeTypeOfElements(map[prevPressedButton.Location.Y / 128, prevPressedButton.Location.X / 128], map[pressedButton.Location.Y / 128, pressedButton.Location.X / 128]);
                    FindMatches();
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

        private static void FindMatches()
        {
            for (int j = 0; j < sizeOfMap; j++)
            {
                int countOfSameElements = 1;
                for (int i = 1; i < sizeOfMap; i++)
                {
                    Element prevElement = map[i-1, j];
                    Element currentElement = map[i, j];

                    if (prevElement.ElementForm == currentElement.ElementForm && (prevElement.ElementForm != Element.TypeOfElement.None &&
                        currentElement.ElementForm != Element.TypeOfElement.None))
                    {
                        countOfSameElements++;
                        if (countOfSameElements == 3) 
                        {
                            map[i - 2, j].Destroy();
                            LowerElementsToPosition(i - 2, j);
                            prevElement.Destroy();
                            LowerElementsToPosition(i - 1, j);
                            currentElement.Destroy();
                            LowerElementsToPosition(i, j);
                            Score += countOfSameElements;
                            scoreHandler.Invoke(Score);
                            countOfSameElements = 1;
                        }
                    }
                    else
                    {
                        countOfSameElements = 1;
                    }
                }
            }

            for (int j = 0; j < sizeOfMap; j++)
            {
                int countOfSameElements = 1;
                for (int i = 1; i < sizeOfMap; i++)
                {
                    Element prevElement = map[j, i - 1];
                    Element currentElement = map[j, i];

                    if (prevElement.ElementForm == currentElement.ElementForm && (prevElement.ElementForm != Element.TypeOfElement.None &&
                        currentElement.ElementForm != Element.TypeOfElement.None))
                    {
                        countOfSameElements++;
                        if (countOfSameElements == 3)
                        {
                            map[j, i - 2].Destroy();
                            LowerElementsToPosition(j, i - 2);
                            prevElement.Destroy();
                            LowerElementsToPosition(j, i - 1);
                            currentElement.Destroy();
                            LowerElementsToPosition(j, i);
                            Score += countOfSameElements;
                            scoreHandler.Invoke(Score);
                            countOfSameElements = 1;
                        }
                    }
                    else
                    {
                        countOfSameElements = 1;
                    }
                }
            }
        }

        private static void LowerElementsToPosition(int posY, int posX)
        {
            for (int i = posY; i > 0; i--)
            {
                ChangeTypeOfElements(map[i, posX], map[i-1, posX]);
            }
            for (int i = 0; i < sizeOfMap; i++)
            {
                if (map[i, posX].ElementForm != Element.TypeOfElement.None)
                {
                    break;
                }
                map[i, posX].SetRandomImage();
            }
        }

        private static void ChangeTypeOfElements(Element firstElement,  Element secondElement)
        {
            Element.TypeOfElement tempType = firstElement.ElementForm;
            firstElement.ElementForm = secondElement.ElementForm;
            secondElement.ElementForm = tempType;

            firstElement.SetImageOfType(firstElement.ElementForm);
            secondElement.SetImageOfType(secondElement.ElementForm);
        }
    }
}
