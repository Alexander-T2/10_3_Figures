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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _10_3_Figures
{
    public delegate void RadiusChanged(object sender, RadiusEventArgs e);
    public partial class MainForm : Form
    {
        List<Shape> shapes = new List<Shape>();
        List<Shape> shapescopy = new List<Shape>();
        Point[,] dots = new Point[2, 6];  // for comparison
        Point[] origdots = new Point[6];
        Point[] jardots = new Point[6];
        Shape[] rndDots = new Shape[20000];
        Random rnd = new Random(); // for comparison
        Stopwatch stopwatch = new Stopwatch();
        Shape interest_dot; // for hulls
        Shape interest_dot2; // for jarvis
        Point Vector1 = new Point(0, 0); // for Jarvis
        Point Vector2 = new Point(0, 0); // for Jarvis
        double cos = 0; // for Jarvis
        double mincos; // for Jarvis
        int dragxst; // for dragging
        int dragyst; // for dragging
        int currentShape = 1; // for changing shape of dots
        int currentAlgorythm = 1; // for changing algorythm of drawing hull
        double k; // for o hull
        double b; // for o hull
        bool yLowest = true;
        bool yHighest = true;
        bool xSame = false;
        int counter;
        int lapseCounter;
        bool dotsMove = false; //for mousemove
        int ts; // for compare
        int step; // for compare
        Color hullColor = Color.Magenta; // for color of dots and stuff
        SliderForm f3 = null; // for radius of dots
        string filename = ""; // for saving
        int startingRadius = Shape.R; // for saving
        Color startingColor = Shape.drawingColor; // for saving
        Color starthullColor = Color.Magenta; // for saving
        bool noSave = true; // for saving
        int id = 0; // undo/redo

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) 
        {
            if (e.Button == MouseButtons.Left)
            {
                bool toDraw = true;
                foreach (Shape el in shapes) // if mouse is inside any figure
                {
                    if (el.IsInside(e.X, e.Y))
                    {
                        toDraw = false;
                    }

                }
                foreach (Shape el in shapes)
                {
                    if (el.IsInside(e.X, e.Y))
                    {
                        el.Isdragged = true;
                        dragxst = e.X;
                        dragyst = e.Y;
                    }
                }
                if (toDraw)
                {
                    noSave = false;
                    switch (currentShape)
                    {
                        case 1:
                            shapes.Add(new Circle(e.X, e.Y, id)); 
                            break;
                        case 2:
                            shapes.Add(new Square(e.X, e.Y, id));
                            break;
                        case 3:
                            shapes.Add(new Triangle(e.X, e.Y, id));
                            break;
                    }
                    id++;
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (Shape el in shapes) // perhaps change later
                {
                    if (el.IsInside(e.X, e.Y))
                    {
                        noSave = false;
                        shapes.Remove(el);
                        break;
                    }
                }

            }
            Refresh();

            if (shapes[shapes.Count - 1].InsideBorders && shapes.Count>2)
            {
                shapes.RemoveAt(shapes.Count - 1);
                foreach (Shape item in shapes)
                {
                    item.Isdragged = true;
                    dragxst = e.X;
                    dragyst = e.Y;
                }
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e) 
        {
            foreach(Shape el in shapes)
            {
                if (el.Isdragged)
                {
                    noSave = false;
                    el.newCoords(e.X - dragxst, e.Y - dragyst);
                    dotsMove = true;
                }
            }
            if (dotsMove)
            {
                dotsMove = false;
                Refresh();
            }
            dragxst = e.X;
            dragyst = e.Y;
            
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Shape el in shapes)
            {
                el.Isdragged = false;
            }
            if (shapes.Count > 2)
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i].InsideBorders)
                    {
                        shapes.RemoveAt(i);
                        i--;
                    }
                }
            }
            Refresh();
        }
        private void Original_hull(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(hullColor);
            Pen p = new Pen(br, 3);
            for (int i = 0; i < shapes.Count - 1; i++) // Obolocka
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    if (shapes[i].X == shapes[j].X)
                    {
                        xSame = true;
                    }
                    else
                    {
                        k = (double)(shapes[i].Y - shapes[j].Y) / (shapes[i].X - shapes[j].X); // y = kx+b
                        b = shapes[j].Y - shapes[j].X * k;
                    }
                    foreach (Shape el in shapes)
                    {
                        if (el != shapes[i] && el != shapes[j])
                        {
                            if (xSame)
                            {
                                if (el.X > shapes[i].X) yLowest = false;
                                if (el.X < shapes[i].X) yHighest = false;
                            }
                            else
                            {
                                if (k * el.X + b > el.Y) yLowest = false;
                                if (k * el.X + b < el.Y) yHighest = false;
                            }
                        }
                    }
                    if (yLowest || yHighest)
                    {
                        g.DrawLine(p, shapes[i].X, shapes[i].Y, shapes[j].X, shapes[j].Y);
                        shapes[i].InsideBorders = false;
                        shapes[j].InsideBorders = false;
                    }
                    yLowest = true;
                    yHighest = true;
                    xSame = false;
                }
            }
        }
        private void Original_hull()
        {
            for (int i = 0; i < shapes.Count - 1; i++) // Obolocka
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    if (shapes[i].X == shapes[j].X)
                    {
                        xSame = true;
                    }
                    else
                    {
                        k = (double)(shapes[i].Y - shapes[j].Y) / (shapes[i].X - shapes[j].X); // y = kx+b
                        b = shapes[j].Y - shapes[j].X * k;
                    }
                    foreach (Shape el in shapes)
                    {
                        if (el != shapes[i] && el != shapes[j])
                        {
                            if (xSame)
                            {
                                if (el.X > shapes[i].X) yLowest = false;
                                if (el.X < shapes[i].X) yHighest = false;
                            }
                            else
                            {
                                if (k * el.X + b > el.Y) yLowest = false;
                                if (k * el.X + b < el.Y) yHighest = false;
                            }
                        }
                    }
                    if (!yLowest && !yHighest)
                    {
                        shapes[i].InsideBorders = false;
                        shapes[j].InsideBorders = false;
                    }
                    yLowest = true;
                    yHighest = true;
                    xSame = false;
                }
            }
        }
        private void Jarvis_hull(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(hullColor);
            Pen p = new Pen(br, 3);
            interest_dot = shapes[0];
            for(int i = 1; i < shapes.Count; i++)
            {
                if (shapes[i].Y > interest_dot.Y || (shapes[i].Y == interest_dot.Y && shapes[i].X < interest_dot.X)) {
                    interest_dot = shapes[i]; //lowest and leftest shapes[j]   
                }   
            }
            Vector1.X = -100;
            Vector1.Y = 0;
            for(int i = 0; i < shapes.Count; i++)
            {
                mincos = 1.1;
                for (int j = 0; j < shapes.Count; j++)
                {
                    if (shapes[j] != interest_dot)
                    {
                        Vector2.X = shapes[j].X - interest_dot.X;
                        Vector2.Y = shapes[j].Y - interest_dot.Y;
                        cos = (Vector1.X * Vector2.X + Vector1.Y * Vector2.Y) / (Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) * Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y));
                        if (cos < mincos)
                        {
                            interest_dot2 = shapes[j];
                            mincos = cos;
                        }
                    }
                }
                g.DrawLine(p, interest_dot.X, interest_dot.Y, interest_dot2.X, interest_dot2.Y);
                Vector1.X = interest_dot.X - interest_dot2.X;
                Vector1.Y = interest_dot.Y - interest_dot2.Y;
                interest_dot.InsideBorders = false;
                interest_dot2.InsideBorders = false;
                interest_dot = interest_dot2;
            }
        }
        private void Jarvis_hull()
        {
            interest_dot = shapes[0];
            for (int i = 1; i < shapes.Count; i++)
            {
                if (shapes[i].Y > interest_dot.Y || (shapes[i].Y == interest_dot.Y && shapes[i].X < interest_dot.X))
                {
                    interest_dot = shapes[i]; //lowest and leftest shapes[j]   
                }
            }
            Vector1.X = -100;
            Vector1.Y = 0;
            for (int i = 0; i < shapes.Count; i++)
            {
                mincos = 1.1;
                for (int j = 0; j < shapes.Count; j++)
                {
                    if (shapes[j] != interest_dot)
                    {
                        Vector2.X = shapes[j].X - interest_dot.X;
                        Vector2.Y = shapes[j].Y - interest_dot.Y;
                        cos = (Vector1.X * Vector2.X + Vector1.Y * Vector2.Y) / (Math.Sqrt(Vector1.X * Vector1.X + Vector1.Y * Vector1.Y) * Math.Sqrt(Vector2.X * Vector2.X + Vector2.Y * Vector2.Y));
                        if (cos < mincos)
                        {
                            interest_dot2 = shapes[j];
                            mincos = cos;
                        }
                    }
                }
                Vector1.X = interest_dot.X - interest_dot2.X;
                Vector1.Y = interest_dot.Y - interest_dot2.Y;
                interest_dot.InsideBorders = false;
                interest_dot = interest_dot2;
                interest_dot.InsideBorders = false;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e) 
        {
            Graphics g = e.Graphics;
            foreach (Shape el in shapes)
            {
                el.Draw(g);
                el.InsideBorders = true;
            }
            if (shapes.Count() > 2)
            {
                if(currentAlgorythm == 1) Original_hull(e);
                else Jarvis_hull(e);
            }
        }
        private void hullCompare(bool orig, bool jarv)
        {
            for (int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    dots[i, j].X = 0;
                    dots[i, j].Y = 0;
                }
            }
            shapescopy.Clear();
            foreach(Shape el in shapes)
            {
                shapescopy.Add(el);
            }
            shapes.Clear();
            counter = 0;  // for amount of dots
            dots[0, 0].X = 0;  // First dot of chart is always 0
            dots[0, 0].Y = 0;
            dots[1, 0].X = 0;
            dots[1, 0].Y = 0;
            lapseCounter = 1; // for choosing a dot
            step = 100; // length of step
            GraphicForm f2 = new GraphicForm();
            for(int i = 0; i < 20000; i++)
            {
                rndDots[i] = new Circle(rnd.Next(0, this.Width), rnd.Next(0, this.Height)); // list of random dots
            }
            for(int i = 0; i < 5; i++)
            {
                for(int j = counter; j < counter + step; j++) 
                {
                    shapes.Add(rndDots[j]);  // increase amount of dots to find next dot
                }
                if (orig)
                {
                    stopwatch.Start(); //counting time for original hull algorythm
                    Original_hull();
                    stopwatch.Stop();
                    ts = (int)(stopwatch.ElapsedMilliseconds); // time spent
                    dots[0, lapseCounter].X = counter + step;  // amount of dots 
                    dots[0, lapseCounter].Y = ts;  // amount of time spent
                }
                
                if (jarv)
                {
                    stopwatch.Reset(); // counting time fot Jarvis hull algorythm
                    stopwatch.Start();
                    Jarvis_hull();
                    stopwatch.Stop();
                    ts = (int)(stopwatch.ElapsedMilliseconds);
                    dots[1, lapseCounter].X = counter + step; 
                    dots[1, lapseCounter].Y = ts;
                }

                counter += step;
                lapseCounter++;
                stopwatch.Reset();
            }
            for(int i = 0; i < 6; i++)
            {
                origdots[i] = dots[0, i];
                jardots[i] = dots[1, i];
            }
            shapes.Clear();
            shapes = shapescopy;
            f2.Show();
            f2.draw(origdots, jardots);
            //    public void ShowMyDialogBox()
            //{
            //    Form2 testDialog = new Form2();

            //    // Show testDialog as a modal dialog and determine if DialogResult = OK.
            //    if (testDialog.ShowDialog(this) == DialogResult.OK)
            //    {
            //        // Read the contents of testDialog's TextBox.
            //        this.txtResult.Text = testDialog.TextBox1.Text;
            //    }
            //    else
            //    {
            //        this.txtResult.Text = "Cancelled";
            //    }
            //    testDialog.Dispose();
            //}
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void save()
        {
            noSave = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream FS = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bf.Serialize(FS, shapes);
            bf.Serialize(FS, Shape.drawingColor);
            bf.Serialize(FS, Shape.R);
            bf.Serialize(FS, filename);
            FS.Close();
        }
        private void saveAs()
        {
            noSave = true;
            BinaryFormatter bf = new BinaryFormatter();
            filename = saveFileDialog1.FileName;
            FileStream FS = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bf.Serialize(FS, shapes);
            bf.Serialize(FS, Shape.drawingColor);
            bf.Serialize(FS, Shape.R);
            bf.Serialize(FS, filename);
            FS.Close();
        }
        private void open()
        {
            noSave = true;
            BinaryFormatter bf = new BinaryFormatter();
            filename = openFileDialog1.FileName;
            FileStream FS = new FileStream(filename, FileMode.Open, FileAccess.Read);
            shapes = (List<Shape>)bf.Deserialize(FS);
            Shape.drawingColor = (Color)bf.Deserialize(FS);
            Shape.R = (int)bf.Deserialize(FS);
            filename = (string)bf.Deserialize(FS);
            FS.Close();
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

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentAlgorythm = 1;
            originalToolStripMenuItem.Checked = true;
            jarvisToolStripMenuItem.Checked = false;
        }

        private void jarvisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentAlgorythm = 2;
            originalToolStripMenuItem.Checked = false;
            jarvisToolStripMenuItem.Checked = true;
        }

        private void comparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void bothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hullCompare(true, true);
        }

        private void originalOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hullCompare(true, false);
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void dotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            Shape.drawingColor = colorDialog.Color;
            Refresh();
        }

        private void hullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            hullColor = colorDialog.Color;
            Refresh();
        }

        private void jarvisOnlyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            hullCompare(false, true);
        }

        private void radiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(f3 == null || f3.IsDisposed)
            {
                f3 = new SliderForm();
                f3.RadiusChange += new RadiusChanged(DI);
                f3.Show();
            }
            else
            {
                f3.Activate();
                f3.WindowState = FormWindowState.Normal;
            }
            
        }
        private void DI(object sender, RadiusEventArgs e)
        {
            Shape.R = e.radius;
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Shape el in shapes)
            {
                el.X += rnd.Next(-5, 5);
                el.Y += rnd.Next(-5, 5);
            }
            if (shapes.Count > 2)
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i].InsideBorders)
                    {
                        shapes.RemoveAt(i);
                        i--;
                    }
                }
            }
            Refresh();
        }

        private void StartLifeBtn_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void EndLifeBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.DefaultExt = "sln";
            if ((noSave)||(startingColor == Shape.drawingColor && startingRadius == Shape.R && shapes.Count == 0))
            {
                shapes.Clear();
                Shape.drawingColor = startingColor;
                Shape.R = startingRadius;
            }
            else
            {
                switch (MessageBox.Show("Do you wish to save?", "New file", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (filename == "")
                        {
                            saveFileDialog1.ShowDialog();
                            saveAs();
                        }
                        shapes.Clear();
                        Shape.drawingColor = startingColor;
                        Shape.R = startingRadius;
                        filename = "";
                        break;
                    case DialogResult.No:
                        shapes.Clear();
                        Shape.drawingColor = startingColor;
                        Shape.R = startingRadius;
                        filename = "";
                        break;
                    case DialogResult.Cancel:
                        break;
                }
                Refresh();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((noSave) || (startingColor == Shape.drawingColor && startingRadius == Shape.R && shapes.Count == 0))
            {
                openFileDialog1.ShowDialog();
            }
            else
            {
                switch (MessageBox.Show("Do you wish to save?", "New file", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (filename == "")
                        {
                            saveFileDialog1.ShowDialog();
                            saveAs();
                            openFileDialog1.ShowDialog();
                            open();

                        }
                        else
                        {
                            save();
                            openFileDialog1.ShowDialog();
                            open();
                        }
                        break;
                    case DialogResult.No:
                        openFileDialog1.ShowDialog();
                        open();
                        break;
                    case DialogResult.Cancel:
                        break;
                }
                Refresh();
                noSave = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                saveFileDialog1.ShowDialog();
                saveAs();
            }
            else
            {
                save();
            }
            Refresh();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            saveAs();
        }
        private void Form1_Formclosing(object sender, FormClosingEventArgs e)
        {
            if(noSave == false && shapes.Count() != 0)
            {
                switch (MessageBox.Show("Do you wish to save?", "New file", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if(filename == "")
                        {
                            openFileDialog1.ShowDialog();
                            saveAs();
                        }
                        else
                        {
                            save();
                        }
                        e.Cancel = false;
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
