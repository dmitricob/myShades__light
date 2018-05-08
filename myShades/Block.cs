using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace myShades
{
    class Block
    {
        private int[] Coords ;
        private Color color;
        private Rectangle Rect;

        public Block(int[] coord, Color color)
        {
            this.Coords = new int[2];
            this.Coords = coord;
            this.Rect = new Rectangle();
            this.Rect.Fill=new SolidColorBrush(color);
            this.Rect.Width = 100;
            this.Rect.Height = 33;
            Canvas.SetLeft(Rect, Coords[1] * 100);

        }

        public void setColor(Color color)
        {
            this.Rect.Fill = new SolidColorBrush(color);
            this.color = color;
        }

        public Color getColors()
        {
            return this.color;
        }

        public void setCoords(int[] coords)
        {
            this.Coords = coords;
            Canvas.SetLeft(Rect, Coords[1] * 100);
            Canvas.SetTop(Rect, Coords[0] * 33);

        }

        public int[] getCoords()
        {
            return this.Coords;
        }

        public Rectangle getRect()
        {
            return this.Rect;
        }
    }
}
