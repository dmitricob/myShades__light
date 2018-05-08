using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace myShades
{
    class Gradient
    {
        private int ColorsCount = 7;
        int ShadesCount = 7;
        int[] ShadesQueue = new int[2];
        Color[,] AvailableColors ;
        Color[] ShadesList ;
        Color[] CurentGradient = new Color[510];
        makeColorsList[] makeColors ;

        public void makeRed()
        {
            int j = 0;
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, 255, i, i);
                j += 1;
            }
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, i, 0, 0);
                j += 1;
            }
        }

        public void makeGreen()
        {
            int j = 0;
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, i, 255, i);
                j += 1;
            }
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, 0, i, 0);
                j += 1;
            }
        }

        public void makeBlue()
        {
            int j = 0;
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, i, i, 255);
                j += 1;
            }
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, 0, 0, i);
                j += 1;
            }
        }

        public void makeYellow()
        {
            int j = 0;
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, 255, 255, i);
                j += 1;
            }
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, i, i, 0);
                j += 1;
            }
        }

        public void makeViolet()
        {
            int j = 0;
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, 255, i, 255);
                j += 1;
            }
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, i, 0, i);
                j += 1;
            }
        }

        public void makeLightBlue()
        {
            int j = 0;
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, i, 255, 255);
                j += 1;
            }
            for (byte i = 255; i > 0; i--)
            {
                CurentGradient[j] = Color.FromArgb(255, 0, i, i);
                j += 1;
            }
        }

        public void makeRaidow()
        {
            CurentGradient[50]  = Colors.Red;// Color.FromArgb(255, 255, 0   , 0);       //red
            CurentGradient[100] = Colors.Orange;//Color.FromArgb(255, 255, 165 , 0);   //orange
            CurentGradient[150] = Colors.Yellow;//Color.FromArgb(255, 255, 255 , 0);     //yellow
            CurentGradient[329] = Colors.Green;//Color.FromArgb(255, 0  , 255 , 0);     //green
            CurentGradient[384] = Colors.Cyan;//Color.FromArgb(255, 0  , 255 , 255);   //cyan
            CurentGradient[429] = Colors.Blue;//Color.FromArgb(255, 0  , 0   , 255);   //blue
            CurentGradient[474] = Colors.BlueViolet; //Color.FromArgb(255, 148, 0 , 210);   //violet
        }

        private delegate void makeColorsList();


        public void setCurenGradient()
        {
            ShadesList[0] = CurentGradient[50];
            ShadesList[1] = CurentGradient[100];
            ShadesList[2] = CurentGradient[150];
            ShadesList[3] = CurentGradient[329];     //200 - 332
            ShadesList[4] = CurentGradient[384];
            ShadesList[5] = CurentGradient[429];
            ShadesList[6] = CurentGradient[474];
        }

        public void makeGradients()
        {
            for(int i = 0;i < ColorsCount; i++)
            {
                setColor(i);
                for (int j = 0; j < ShadesCount; j++)
                {
                    AvailableColors[i,j] = ShadesList[j];
                }
            }
        }

        public Color[,] getAvailableColors()
        {
            return AvailableColors;
        }

        public void setColor(int i)
        {
            makeColors[i]();
            setCurenGradient();
        }

        public Gradient()
        {
            ShadesList = new Color[ShadesCount];
            AvailableColors = new Color[ColorsCount, ShadesCount];
            makeColors = new makeColorsList[ColorsCount];
            makeColors[0] = makeRed;
            makeColors[1] = makeGreen;
            makeColors[2] = makeBlue;
            makeColors[3] = makeYellow;
            makeColors[4] = makeViolet;
            makeColors[5] = makeLightBlue;
            makeColors[6] = makeRaidow;

            makeGradients();


            ShadesQueue[0] = new Random().Next(0, ShadesCount);
            ShadesQueue[0] = new Random().Next(0, ShadesCount);


            setColor(4);
            
        }

        public Color[] getShadesList()
        {
            return ShadesList;
        }

        //no use
        public Color getRandomShade()
        {
            return ShadesList[new Random().Next(0, ShadesCount)];
        }

        public int swapShade(int color)
        {
            int curent = ShadesQueue[0];
            ShadesQueue[0] = color;
            return curent;
        }

        public int getColorsCount()
        {
            return ColorsCount;
        }

        public int[] getShadesQueue()
        {
            return ShadesQueue;
        }

        /// <summary>
        /// get random color and make qeueu
        /// </summary>
        /// <returns></returns>
        public int[] getRandomShadeNumber()
        {
            int[] queue = new int[2];
            queue[0] = ShadesQueue[0];
            queue[1] = ShadesQueue[1];
            ShadesQueue[0] = ShadesQueue[1];
            ShadesQueue[1] = new Random().Next(0, ShadesCount);
            return queue;
        }

        //first version
        /*public int getRandomShadeNumber()
        {
            return new Random().Next(0,ShadesCount);
        }*/

        public int getShadesCount()
        {            
            return ShadesCount;
        }
    }
}
