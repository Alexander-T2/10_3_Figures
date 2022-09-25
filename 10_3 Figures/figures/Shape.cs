using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _10_3_Figures
{
    abstract class Shape
    {
        protected static int R = 60;
        protected int x, y;
        protected static string drawingColor;
        static Shape()
        {
            // fill up?
        }

        abstract public bool IsInside(int x, int y);
        abstract public void Draw(Graphics g);
        abstract public void newCoords(int x, int y);
    }
}
