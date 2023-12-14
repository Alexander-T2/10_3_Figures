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
    public partial class GraphicForm : Form
    {

        public GraphicForm()
        {
            InitializeComponent();
        }
        public void draw(Point[] series1, Point[] series2)
        {
            this.comparisonChart.Series[0].Points.Clear();
            this.comparisonChart.Series[1].Points.Clear();
            for(int i = 0; i < 6; i++)
            {
                this.comparisonChart.Series[0].Points.AddXY(series1[i].X, series1[i].Y);
                this.comparisonChart.Series[1].Points.AddXY(series2[i].X, series2[i].Y);
            }
        }
    }
}
