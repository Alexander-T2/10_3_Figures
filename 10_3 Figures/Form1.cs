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
        List<Shape> movingShapes = new List<Shape>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Shape el in shapes)
                {
                    if (el.IsInside(e.X, e.Y))
                    {
                        movingShapes.Add(el);
                    }
                }
            }     
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
