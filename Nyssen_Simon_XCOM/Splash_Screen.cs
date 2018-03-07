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
        public Splash_Screen()
        {
            InitializeComponent();
            /*
            System.Threading.Thread.Sleep(3000);
            this.Close();
            */
        }

        private void Splash_Screen_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
