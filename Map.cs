
using System.Windows.Forms;

namespace MatchThreeGame
{
    class Map
    {
        const int sizeOfMap = 8;
        const int sizeOfImage = 128;

        public static Element[,] map = new Element[sizeOfMap, sizeOfMap];

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

        public static Map CreateMap()
        {
            var instance = getInstance();
            for (int i = 0; i < sizeOfMap; i++)
            {
                for (int j = 0; j < sizeOfMap; j++)
                {
                    Element gem = new DefaultElement();
                    gem.Button = new Button();
                    gem.Button.Size = new Size(sizeOfImage, sizeOfImage);
                    gem.Button.Location = new Point(j * sizeOfImage, i * sizeOfImage);
                    gem.SetImageRandom();
                    map[i, j] = gem;
                }
            }

            return instance;
        }
    }
}
