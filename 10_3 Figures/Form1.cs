using _10_3_Figures.figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10_3_Figures
{
    public partial class Form1 : Form
    {
        List<Shape> shapes = new List<Shape>();
        int dragxst;
        int dragyst;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) 
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Shape el in shapes)
                {
                    if (el.IsInside(e.X, e.Y))
                    {
                        el.Isdragged = true;
                        dragxst = e.X;
                        dragyst = e.Y;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if(shapes.Count > 0)
                {
                    shapes.RemoveAt(shapes.Count - 1);
                }
            }
            Refresh();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e) 
        {
            foreach(Shape el in shapes)
            {
                if (el.Isdragged)
                {
                   el.newCoords(e.X - dragxst, e.Y - dragyst);
                }
            }
            dragxst = e.X;
            dragyst = e.Y;
            Refresh();
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            bool flag = true;
            if (e.Button == MouseButtons.Left)
            {
                foreach (Shape el in shapes)
                {
                    if (el.IsInside(e.X, e.Y))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    shapes.Add(new Triangle(e.X, e.Y));
                }
            }
            Refresh();
            flag = true;
            foreach (Shape el in shapes)
            {
                el.Isdragged = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Shape el in shapes)
            {
                el.Draw(g);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
