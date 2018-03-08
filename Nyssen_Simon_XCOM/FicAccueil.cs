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
    public partial class EcranAccueil : Form
    {
        public Boolean GameLaunch = false, Setup = false;

        public EcranAccueil()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            GameLaunch = false;
            Setup = false;
            Close();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            GameLaunch = true;
            Setup = true;
            Close();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            GameLaunch = true;
            Setup = false;

            if (dlgLoadGame.ShowDialog() == DialogResult.OK)
            {

                Close();
            }
        }

        private void llblCopyright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/snyssen");
            llblCopyright.LinkVisited = true;
        }
    }
}
