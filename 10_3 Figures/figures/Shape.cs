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
        protected static int R;
        protected bool isdragged;
        protected int x, y;
        protected static Color drawingColor;
        static Shape()
        {
            R = 60;
        }
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            isdragged = false;
        }
        public bool Isdragged
        {
            set { isdragged = value; }
            get { return isdragged; }
        }
        abstract public bool IsInside(int x, int y);
        abstract public void Draw(Graphics g);
        abstract public void newCoords(int x, int y);
    }
}
