using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyssen_Simon_XCOM
{
    public partial class Splash_Screen : Form
    {
        int i = 0;

        public Splash_Screen()
        {
            InitializeComponent();
        }

        private void Splash_Screen_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            i++;
            if (i <= 100)
                Opacity = i;
            else
            {
                if (i > 200)
                {
                    Opacity--;
                    if (Opacity == 0)
                        Close();
                }
            }
            Invalidate();
        }

        private void Splash_Screen_Load(object sender, EventArgs e)
        {
            timerSplash.Start();
        }
    }
}
