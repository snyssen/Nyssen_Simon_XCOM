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
    public partial class EcranAction : Form
    {
        public int Choix = 3; // Change selon le choix de l'utilisateur :
                                                                        // 0 = Déplacement
                                                                        // 1 = Renforcement de la position
                                                                        // 2 = Tirer
                                                                        // 3 = Indéfini => annulation
        public EcranAction()
        {
            InitializeComponent();
        }

        private void btnMoving_Click(object sender, EventArgs e)
        {
            Choix = 0;
            this.Close();
        }

        private void btnCovering_Click(object sender, EventArgs e)
        {
            Choix = 1;
            this.Close();
        }

        private void btnFiring_Click(object sender, EventArgs e)
        {
            Choix = 2;
            this.Close();
        }
    }
}
