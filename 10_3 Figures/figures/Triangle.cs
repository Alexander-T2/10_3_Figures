using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _10_3_Figures.figures
{
    class Triangle: Shape
    {
        public Triangle(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        override public bool IsInside(int mx, int my) //TODO
        {
            return (mx > x - R) & (mx < x + R) & (my > y - R) & (my < y + R);
        }
        override public void Draw(Graphics g)  //TODO
        {
            Brush b = new SolidBrush(Color.Purple);
            Pen p = new Pen(b, 3);
            int r = Convert.ToInt32(Math.Sqrt(R) / 2);
            
        }
        public override void newCoords(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
    }
}
