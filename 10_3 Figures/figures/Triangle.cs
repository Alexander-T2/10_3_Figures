using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _10_3_Figures.figures
{
    [Serializable]
    class Triangle: Shape
    {
        Point top = new Point();
        Point left = new Point();
        Point right = new Point();
        Point mouse = new Point();
        public Triangle(int x, int y) : base(x, y)
        {
        }
        public Triangle(int x, int y, int id) : base(x, y)
        {
            this.id = id;
        }
        override public bool IsInside(int mx, int my)
        {
            mouse.X = mx;
            mouse.Y = my;
            return Math.Abs(area(dist(top, right), dist(right, left), dist(left, top)) - 
                area(dist(top, mouse), dist(mouse, left), dist(left, top)) - 
                area(dist(top, right), dist(right, mouse), dist(mouse, top)) - 
                area(dist(mouse, right), dist(right, left), dist(left, mouse))) < 1;
        }
        override public void Draw(Graphics g)
        {
            top.X = x;
            top.Y = y - R;
            right.X = x - Convert.ToInt32(R * Math.Sqrt(3) / 2);
            right.Y = y + Convert.ToInt32(R * 0.5);
            left.X = x + Convert.ToInt32(R * Math.Sqrt(3) / 2);
            left.Y = y + Convert.ToInt32(R * 0.5);
            Brush b = new SolidBrush(drawingColor);
            Pen p = new Pen(b, 3);
            g.DrawLine(p, top, right);
            g.DrawLine(p, right, left);
            g.DrawLine(p, left, top);
        }
        public override void newCoords(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
        private double dist(Point p1, Point p2) // distance between 2 points
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        private double area(double d1, double d2, double d3) // area of a triangle through sides
        {
            double p = (d1 + d2 + d3) / 2;
            return Math.Sqrt(p*(p-d1)*(p-d2)*(p-d3));
        }
    }
}
