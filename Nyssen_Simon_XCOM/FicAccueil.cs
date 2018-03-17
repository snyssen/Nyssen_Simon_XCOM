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
        public Boolean GameLaunch = false; // Si true, on veut lancer une partie (peu importe que ce soit une nouvelle ou une ancienne); sinon on veut quitter le programme
        public Boolean Setup = false;      // Si true, on veut lancer une NOUVELLE partie; sinon on veut reprendre une en cours (dépendant dans les deux cas de GameLaunch)
        public EcranSetup setup = new EcranSetup();

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
            
            setup.ShowDialog();
            if (setup.begin)
            {
                Close();
                GameLaunch = true;
            }
                
            /*
            GameLaunch = true;
            Setup = true;
            Close();
            */
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            GameLaunch = true;
            Setup = false;

            MessageBox.Show("Fonction non-implémentée...");

            /*
            if (dlgLoadGame.ShowDialog() == DialogResult.OK)
            {

                Close();
            }
            */
        }

        private void llblCopyright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/snyssen");
            llblCopyright.LinkVisited = true;
        }

        private void btnAide_Click(object sender, EventArgs e)
        {
            EcranAide aide = new EcranAide();
            aide.ShowDialog();
        }
    }
}
