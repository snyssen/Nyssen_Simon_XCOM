using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Nyssen_Simon_XCOM
{
    public partial class EcranGame : Form
    {
        private short SelectedbtnIndex; // Choix de la carte
        private int NbrFantassins; // Nombre de fantassins (par escouade)
        private int NbrSnipers; // Nombre de snipers (par escouade)
        private int NbrLourds; // Nombre de soldats lourds (par escouade)
        private int NbrLegers; // Nombre de soldats légers (par escouade)
        private int NbrSoldats; // Nombre de soldats toute classe confondue (par escouades)
        bool error = false;
        Case_Echiquier[,] Cases = new Case_Echiquier[10,10];
        int IndexX, IndexY;

        //private GraphicsPath GraphEnr = null;

        public EcranGame(short Index, int NbrFant, int NbrSnip, int NbrLd, int NbrLg)
        {
            InitializeComponent();
            tsInfo.Text = "";
            this.SelectedbtnIndex = Index;
            this.NbrFantassins = NbrFant;
            this.NbrSnipers = NbrSnip;
            this.NbrLourds = NbrLd;
            this.NbrLegers = NbrLg;
            this.NbrSoldats = NbrFantassins + NbrSnipers + NbrLourds + NbrLegers;

            pbCase.Parent = pbCarte;

            tsAvancement.Maximum = NbrSoldats;
            tsAvancement.Alignment = ToolStripItemAlignment.Right;
            tsNbrSoldatsJoue.Alignment = ToolStripItemAlignment.Right;
            tsNbrSoldatsJoue.Text = "0/" + NbrSoldats.ToString();

            switch (SelectedbtnIndex)
            {
                case 0:
                    pbCarte.BackgroundImage = Properties.Resources.desert_road;
                    break;
                case 1:
                    pbCarte.BackgroundImage = Properties.Resources.Snowy_Pass;
                    break;
                case 2:
                    pbCarte.BackgroundImage = Properties.Resources.Urban_Parking;
                    break;
                default:
                    MessageBox.Show("ERREUR ! Pas de carte sélectionnée !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error = true;
                    break;
            }

            if (!error)
            {
                RemplirTabCases();
                //DessinerEchiquier();
            }

            else
                Close();
        }

        private void RemplirTabCases()
        {
            int Longueur = pbCarte.Width;
            int Hauteur = pbCarte.Height;

            for (int x = 0; x < 10; x++) // Colonnes
            {
                for (int y = 0; y < 10; y++) // Lignes
                {
                    Cases[x, y] = new Case_Echiquier(x * Longueur / 10, y * Hauteur / 10, (x+1) * Longueur / 10, (y+1) * Hauteur / 10);
                    Cases[x, y].DessinerCase(pbCarte.Handle);
                    Console.WriteLine("Case[" + x + ";" + y + "] cree. Elle a pour valeurs :\n\t\tPoint d'origine : (" + Cases[x, y].posX + ";" + Cases[x, y].posY + ")\n\t\tPoint de chute : (" + Cases[x, y].Xmax + ";" + Cases[x, y].Ymax + ")");
                }
            }
        }

        private void FindLocation(int posX, int posY)
        {
            Boolean Trouve = false;
            for (int x = 0; x < 10; x++) // Colonnes
            {
                for (int y = 0; y < 10; y++) // Lignes
                {
                    if (posX >= Cases[x, y].posX && posX < Cases[x, y].Xmax)
                    {
                        if (posY >= Cases[x, y].posY && posY < Cases[x, y].Ymax)
                        {
                            Trouve = true;
                            IndexX = x;
                            IndexY = y;
                            break;
                        }
                    }
                }
                if (Trouve)
                    break;
            }

            if (!Trouve)
                tsInfo.Text = "ERREUR : case non trouvée ! pos souris = " + posX + " " + posY;
            else
                tsInfo.Text = "case (" + IndexX + ";" + IndexY + "), pos souris = " + posX + " " + posY;
        }

        private void ShowCover(Case_Echiquier caseactu)
        {
            Boolean Error = false;

            switch (caseactu.Cover)
            {
                case 0:
                    pbCase.Image = Properties.Resources.Shield_Empty;
                    break;
                case 1:
                    pbCase.Image = Properties.Resources.Shield_Half;
                    break;
                case 2:
                    pbCase.Image = Properties.Resources.Shield_Full;
                    break;
                default:
                    MessageBox.Show("ERREUR : Pas de niveau de couverture défini sur cette case ! ");
                    Error = true;
                    break;
            }

            if (!Error)
            {
                pbCase.Width = Cases[IndexX, IndexY].Xmax - Cases[IndexX, IndexY].posX;
                pbCase.Height = Cases[IndexX, IndexY].Ymax - Cases[IndexX, IndexY].posY;
                pbCase.Location = new Point(Cases[IndexX, IndexY].posX, Cases[IndexX, IndexY].posY);
                pbCase.Visible = true;
            }
        }

        private void DessinerEchiquier()
        {
            int Largeur = pbCarte.Width;
            int Hauteur = pbCarte.Height;

            //GraphEnr = new GraphicsPath();
            Graphics gr = Graphics.FromHwnd(pbCarte.Handle);
            //Graphics gr = pbCarte.CreateGraphics();

            gr.FillRectangle(new SolidBrush(Color.Red), pbCarte.Location.X, pbCarte.Location.Y, Largeur, Hauteur);

            for (int i = 0; i <= 10; i++)
            {
                gr.DrawLine(new Pen(Color.AliceBlue, 50), new Point(i * Largeur / 10, 0), new Point(i * Largeur / 10, Hauteur)); // Lignes verticales
                //GraphEnr.AddLine(new Point(i * Largeur / 10, 0), new Point(i * Largeur / 10, Hauteur));
                gr.DrawLine(new Pen(Color.AliceBlue, 50), new Point(0, i * Hauteur / 10), new Point(Largeur, i * Hauteur / 10)); // Lignes horizontales
                //GraphEnr.AddLine(new Point(0, i * Hauteur / 10), new Point(Largeur, i * Hauteur / 10));
            }

            gr.Dispose();
        }

        private void pbCarte_MouseMove(object sender, MouseEventArgs e) // On déplace la souris au dessus de la picturebox ~= hover
        {
            FindLocation(e.X, e.Y);
            ShowCover(Cases[IndexX, IndexY]);
        }

        private void EcranGame_Resize(object sender, EventArgs e)
        {
            Invalidate();
            RemplirTabCases();
        }

        private void EcranGame_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (GraphEnr != null)
            {
                e.Graphics.DrawPath(new Pen(Color.AliceBlue, 50), GraphEnr);
            }
            */
        }

        private void EcranGame_Load(object sender, EventArgs e)
        {
            //pbCarte.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCarte_Paint);
        }

        private void tsfQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbCase_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pbCarte_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
