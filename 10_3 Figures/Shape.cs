using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_3_Figures
{
    abstract class Shape
    {
        protected static int R = 15;
        protected int x, y;
        protected static string drawingColor;
        static Shape()
        {
            // fill up?
        }
        public Shape(int x, int y)
        {
            // fill up?
        }
        abstract public bool IsInside();
        abstract public void Draw();
    }
}
