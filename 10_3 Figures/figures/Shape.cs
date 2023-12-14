using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _10_3_Figures
{
    [Serializable]
    abstract class Shape
    {
        public static int R;
        public static List<Shape> shapes;
        [NonSerialized]
        protected bool isdragged;
        [NonSerialized]
        protected bool insideBorders;
        protected int x, y;
        protected int id;
        public static Color drawingColor;
        static Shape()
        {
            R = 10;
            drawingColor = Color.Purple;
        }
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            isdragged = false;
            insideBorders = false;
        }
        public bool Isdragged
        {
            set { isdragged = value; }
            get { return isdragged; }
        }
        public bool InsideBorders
        {
            set { insideBorders = value; }
            get { return insideBorders; }
        }
        public int X
        {
            get { return this.x; }
            set {this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set {this.y = value; }
        }
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        abstract public bool IsInside(int x, int y);
        abstract public void Draw(Graphics g);
        abstract public void newCoords(int x, int y);
    }
    public class RadiusEventArgs: EventArgs
    {
        public int radius;
        public RadiusEventArgs(int rad)
        {
            radius = rad;
        }
    }
}