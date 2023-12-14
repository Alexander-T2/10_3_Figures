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
    public partial class SliderForm : Form
    {
        public bool isOpened = false;
        public event RadiusChanged RadiusChange;
        public SliderForm()
        {
            InitializeComponent();
            trackBar1.Value = Shape.R;
            isOpened = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.RadiusChange(this, new RadiusEventArgs(trackBar1.Value));
        }

        private void SliderForm_Load(object sender, EventArgs e)
        {

        }
    }
    
}
