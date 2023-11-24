using System.Collections;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Windows.Forms;

namespace MatchThreeGame
{
    class Map
    {
        const int sizeOfMap = 8;
        const int sizeOfImage = 128;
        
        public static Element[,] map = new Element[sizeOfMap, sizeOfMap];
        private static List<Element> matchedElements;
        private static bool match;

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
            matchedElements = new List<Element>();
            match = false;
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
            element.SetImage();
        }

        public static void OnElementPress(object sender, EventArgs e)
        {
            Button? pressedButton = sender as Button;

            if (prevPressedButton != null)
            {
                prevPressedButton.BackColor = SystemColors.ControlLight;
                if (IsButtonsNear(prevPressedButton, pressedButton))
                {
                    ChangeElements(map[prevPressedButton.Location.Y / 128, prevPressedButton.Location.X / 128], 
                        map[pressedButton.Location.Y / 128, pressedButton.Location.X / 128]);
                    if (!FindMatches())
                    {
                        ChangeElements(map[prevPressedButton.Location.Y / 128, prevPressedButton.Location.X / 128], 
                            map[pressedButton.Location.Y / 128, pressedButton.Location.X / 128]);
                    }
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

        private static bool FindMatches()
        {
            bool foundVerticalMatches = FindVerticalMatches();
            bool foundHorizontalMatches = FindHorizontalMatches();
            return foundVerticalMatches || foundHorizontalMatches;
        }

        private static bool FindVerticalMatches()
        {
            bool foundVerticalMatches = false;
            for (int j = 0; j < sizeOfMap; j++)
            {
                int countOfSameElements = 1;
                CheckOnMatchCount(matchedElements.Count, ref foundVerticalMatches);
                for (int i = 1; i < sizeOfMap; i++)
                {
                    Element prevElement = map[i - 1, j];
                    Element currentElement = map[i, j];

                    if (prevElement.ElementForm == currentElement.ElementForm && (prevElement.ElementForm != Element.TypeOfElement.None &&
                        currentElement.ElementForm != Element.TypeOfElement.None))
                    {
                        if (countOfSameElements == 1)
                            matchedElements.Add(prevElement);
                        matchedElements.Add(currentElement);
                        countOfSameElements++;
                    }
                    else
                    {
                        CheckOnMatchCount(countOfSameElements, ref foundVerticalMatches);
                        countOfSameElements = 1;
                    }
                }
            }
            return foundVerticalMatches;
        }

        private static bool FindHorizontalMatches()
        {
            bool foundHorizontalMatches = false;
            
            for (int j = 0; j < sizeOfMap; j++)
            {
                int countOfSameElements = 1;
                CheckOnMatchCount(matchedElements.Count, ref foundHorizontalMatches);
                for (int i = 1; i < sizeOfMap; i++)
                {
                    Element prevElement = map[j, i - 1];
                    Element currentElement = map[j, i];

                    if (prevElement.ElementForm == currentElement.ElementForm && (prevElement.ElementForm != Element.TypeOfElement.None &&
                        currentElement.ElementForm != Element.TypeOfElement.None))
                    {
                        if (countOfSameElements == 1)
                            matchedElements.Add(prevElement);
                        matchedElements.Add(currentElement);
                        countOfSameElements++;
                    }
                    else
                    {
                        CheckOnMatchCount(countOfSameElements, ref foundHorizontalMatches);
                        countOfSameElements = 1;
                    }
                }
            }
            return foundHorizontalMatches;
        }

        private async static void LowerElementsToPosition(int posY, int posX)
        {
            for (int i = posY; i > 0; i--)
            {
                ChangeElements(map[i, posX], map[i-1, posX]);
            }
            // Set upper elements random image
            for (int i = 0; i < sizeOfMap; i++)
            {
                if (map[i, posX].ElementForm != Element.TypeOfElement.None)
                {
                    break;
                }
                await Task.Delay(1000);
                map[i, posX].SetImage();
            }
        }

        private async static void ChangeElements(Element firstElement,  Element secondElement)
        {
            Element.TypeOfElement tempType = firstElement.ElementForm;
            firstElement.ElementForm = secondElement.ElementForm;
            secondElement.ElementForm = tempType;
            await Task.Delay(500);
            firstElement.SetImage(firstElement.ElementForm);
            await Task.Delay(500);
            secondElement.SetImage(secondElement.ElementForm);
        }

        private static void CheckOnMatchCount(int matchCount, ref bool matched)
        {
            if (matchCount < 3)
            {
                matchedElements.Clear();
            }
            else
            {
                DeleteMatch();
                AddScores(matchCount);
                matched = true;
            }
        }

        private static void DeleteMatch()
        {
            foreach (Element element in matchedElements)
            {
                element.Destroy();
                LowerElementsToPosition(element.Button.Location.Y / 128,
                        element.Button.Location.X / 128);
            }
            matchedElements.Clear();
        }

        private static void AddScores(int countOfMatchedElements)
        {
            Score += countOfMatchedElements;
            scoreHandler.Invoke(Score);
        }
    }
}
