using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _10_3_Figures.figures
{
    class Square: Shape
    {
        public Square(int x, int y) :base(x, y)
        {
        }
        override public bool IsInside(int mx, int my)
        {
            return (mx > x - R) & (mx < x + R) & (my > y - R) & (my < y + R);
        }
        override public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Purple);
            Pen p = new Pen(b, 3);
            g.DrawRectangle(p, x - R, y - R, R*2, R*2);
        }
        public override void newCoords(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
    }
}
