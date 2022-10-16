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
        int currentShape = 1;
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
                    switch (currentShape)
                    {
                        case 1:
                            shapes.Add(new Circle(e.X, e.Y));
                            break;
                        case 2:
                            shapes.Add(new Square(e.X, e.Y));
                            break;
                        case 3:
                            shapes.Add(new Triangle(e.X, e.Y));
                            break;
                    }
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

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentShape = 1;
            circleToolStripMenuItem.Checked = true;
            squareToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = false;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentShape = 2;
            circleToolStripMenuItem.Checked = false;
            squareToolStripMenuItem.Checked = true;
            triangleToolStripMenuItem.Checked = false;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentShape = 3;
            circleToolStripMenuItem.Checked = false;
            squareToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = true;
        }
    }
}
