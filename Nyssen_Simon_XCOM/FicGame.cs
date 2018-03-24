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
        private int NbrSoldats; // Nombre de soldats toute classe confondue (par escouade)
        private int NbrSoldatsJoues; // Nombre de soldats ayant joués sur ce tour (par escouade)
        bool error = false; // Détecte une erreur
        Case_Echiquier[,] Cases = new Case_Echiquier[10,10]; // Tableau de cases qui forme l'échiquier
        int IndexX, IndexY; // Contient la position (l'index) de la case sur laquelle on travaille
        List<PictureBox> SoldiersIcons1 = new List<PictureBox>(); // Icônes des soldats du joueur 1
        List<PictureBox> SoldiersIcons2 = new List<PictureBox>(); // Icônes des soldats du joueur 2
        List<Soldat> Soldiers1 = new List<Soldat>(); // Soldats du joueur 1
        List<Soldat> Soldiers2 = new List<Soldat>(); // Soldats du joueur 2
        private bool First = true; // Premier lancement
        private bool ActionEnCours = false; // Une action est-elle en cours (déplacement ou tir)
        private bool HasClicked = false; // Case sélectionnée durant une action => On veut désactiver le FindLocation() de l'event pbCarte_MouseMove()

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
            this.NbrSoldatsJoues = 0;

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
                
                for (int i = 0; i < NbrSoldats; i++) // Liste d'icônes des soldats du joueur 1
                {
                    SoldiersIcons1.Add(new PictureBox { Size = new Size(Cases[0, 0].Xmax - Cases[0, 0].posX, Cases[0, 0].Ymax - Cases[0, 0].posY), Parent = pbCarte, SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent });
                    this.Controls.Add(SoldiersIcons1[i]);
                    SoldiersIcons1[i].Click += new EventHandler(SoldiersIcons1_Click);
                    SoldiersIcons1[i].Name = "SoldiersIcons1_" + i;
                }
                for (int i = 0; i < NbrSoldats; i++) // Liste d'icônes des soldats du joueur 2
                {
                    SoldiersIcons2.Add(new PictureBox { Size = new Size(Cases[0, 0].Xmax - Cases[0, 0].posX, Cases[0, 0].Ymax - Cases[0, 0].posY), Parent = pbCarte, SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent });
                    this.Controls.Add(SoldiersIcons2[i]);
                    SoldiersIcons2[i].Click += new EventHandler(SoldiersIcons2_Click);
                    SoldiersIcons2[i].Name = "SoldiersIcons2_" + i;
                }

                // Listes des soldats
                int j = 0; // Compte total des boucles, nécessaire pour connaître la place du soldat actuellement créé dans la liste globvale de soldats de chaque joueur
                for (int i = 0; i < NbrFantassins; i++) // Fantassins
                {
                    Soldiers1.Add(new Soldat(0, Cases[i, 0])); // On créée le soldat sur la case désirée
                    Cases[i, 0].soldier = Soldiers1[j]; // Et on assigne ce soldat à la case
                    Soldiers2.Add(new Soldat(0, Cases[9 - i, 9]));
                    Cases[9 - i, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i].Image = Properties.Resources.Fantassin; // On assigne son icône
                    SoldiersIcons2[i].Image = Properties.Resources.Fantassin;
                    SoldiersIcons1[i].Parent = pbCarte; // On s'assure qu'il est parenté au plateau de jeu (permet de le positionner par rapport à ce plateau et de pouvoir utiliser la transparence)
                    SoldiersIcons2[i].Parent = pbCarte;
                    SoldiersIcons1[i].Location = Cases[i, 0].Origin; // On place cet icône sur la bonne case
                    SoldiersIcons2[i].Location = Cases[9 - i, 9].Origin;
                    j++;
                }
                for (int i = 0; i < NbrSnipers; i++) // Snipers
                {
                    Soldiers1.Add(new Soldat(0, Cases[i + NbrFantassins, 0]));
                    Cases[i + NbrFantassins, 0].soldier = Soldiers1[j];
                    Soldiers2.Add(new Soldat(0, Cases[9 - i-NbrFantassins, 9]));
                    Cases[9 - i - NbrFantassins, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i + NbrFantassins].Image = Properties.Resources.Sniper;
                    SoldiersIcons2[i + NbrFantassins].Image = Properties.Resources.Sniper;
                    SoldiersIcons1[i + NbrFantassins].Parent = pbCarte;
                    SoldiersIcons2[i + NbrFantassins].Parent = pbCarte;
                    SoldiersIcons1[i + NbrFantassins].Location = Cases[i + NbrFantassins, 0].Origin;
                    SoldiersIcons2[i + NbrFantassins].Location = Cases[9 - i - NbrFantassins, 9].Origin;
                    j++;
                }
                for (int i = 0; i < NbrLourds; i++) // Soldats lourds
                {
                    Soldiers1.Add(new Soldat(0, Cases[i + NbrFantassins + NbrSnipers, 0]));
                    Cases[i + NbrFantassins + NbrSnipers, 0].soldier = Soldiers1[j];
                    Soldiers2.Add(new Soldat(0, Cases[9 - i - NbrFantassins - NbrSnipers, 9]));
                    Cases[9 - i - NbrFantassins - NbrSnipers, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers].Image = Properties.Resources.Lourd;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers].Image = Properties.Resources.Lourd;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers].Parent = pbCarte;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers].Parent = pbCarte;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers].Location = Cases[i + NbrFantassins + NbrSnipers, 0].Origin;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers].Location = Cases[9 - i - NbrFantassins - NbrSnipers, 9].Origin;
                    j++;
                }
                for (int i = 0; i < NbrLegers; i++) // Soldats légers
                {
                    Soldiers1.Add(new Soldat(0, Cases[i + NbrFantassins + NbrSnipers + NbrLourds, 0]));
                    Cases[i + NbrFantassins + NbrSnipers + NbrLourds, 0].soldier = Soldiers1[j];
                    Soldiers2.Add(new Soldat(0, Cases[9 - i - NbrFantassins - NbrSnipers - NbrLourds, 9]));
                    Cases[9 - i - NbrFantassins - NbrSnipers - NbrLourds, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds].Image = Properties.Resources.Leger;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds].Image = Properties.Resources.Leger;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds].Parent = pbCarte;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds].Parent = pbCarte;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds].Location = Cases[i + NbrFantassins + NbrSnipers + NbrLourds, 0].Origin;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds].Location = Cases[9 - i - NbrFantassins - NbrSnipers - NbrLourds, 9].Origin;
                    j++;
                }

                First = false;
            }

            else
                Close();
        }

        private void RemplirTabCases()
        {
            int Longueur = pbCarte.Width;
            int Hauteur = pbCarte.Height;

            Soldat soldatTmp = null;

            for (int x = 0; x < 10; x++) // Colonnes
            {
                for (int y = 0; y < 10; y++) // Lignes
                {
                    if (!First) // On ne passe pas ici au lancement de la fenêtre
                    {
                        soldatTmp = null;
                        if (Cases[x, y].soldier != null) // La case actuelle a-t-elle un soldat assigné ?
                            soldatTmp = Cases[x, y].soldier; // Stocke le soldat... (l. 172)
                    }
                    Cases[x, y] = new Case_Echiquier(x * Longueur / 10, y * Hauteur / 10, (x + 1) * Longueur / 10, (y + 1) * Hauteur / 10);
                    Cases[x, y].DessinerCase(pbCarte.Handle);
                    if (!First) // On ne passe pas ici au lancement de la fenêtre
                    {
                        if (soldatTmp != null)
                        {
                            Cases[x, y].soldier = soldatTmp; // ... Pour le réassigner à la case nouvellement créée (l. 164)
                            // Puis on vérifie dans "l'autre sens"; A quel soldat cette case est-elle assignée ?
                            foreach (Soldat soldat in Soldiers1) // Soldats joueur 1
                            {
                                if (Cases[x, y].soldier == soldat)
                                    soldat.position = Cases[x, y];
                            }
                            foreach (Soldat soldat in Soldiers2) // Soldats joueur 2
                            {
                                if (Cases[x, y].soldier == soldat)
                                    soldat.position = Cases[x, y];
                            }
                        }
                    }
                    // DEBUG
                    Console.WriteLine("Case[" + x + ";" + y + "] cree. Elle a pour valeurs :\n\t\tPoint d'origine : (" + Cases[x, y].posX + ";" + Cases[x, y].posY + ")\n\t\tPoint de chute : (" + Cases[x, y].Xmax + ";" + Cases[x, y].Ymax + ")");
                }
            }

            if (Soldiers1 != null) // On resize les picturebox des soldats pour s'ajuster à la nouvelle taille des cases
            {
                int i = 0;
                foreach (Soldat soldat in Soldiers1) // Soldats joueur 1
                {
                    SoldiersIcons1[i].Size = new Size(Cases[0, 0].Xmax - Cases[0, 0].posX, Cases[0, 0].Ymax - Cases[0, 0].posY);
                    SoldiersIcons1[i].Location = soldat.position.Origin;
                    i++;
                }
                i = 0;
                foreach (Soldat soldat in Soldiers2) // Soldats joueur 2
                {
                    SoldiersIcons2[i].Size = new Size(Cases[0, 0].Xmax - Cases[0, 0].posX, Cases[0, 0].Ymax - Cases[0, 0].posY);
                    SoldiersIcons2[i].Location = soldat.position.Origin;
                    i++;
                }
            }
        }

        private void FindLocation(int posX, int posY) // VERIFIER LIBERATION DE MEMOIRE
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

            /* DEBUG
            if (!Trouve)
                tsInfo.Text = "ERREUR : case non trouvée ! pos souris = " + posX + " " + posY;
            else
                tsInfo.Text = "case (" + IndexX + ";" + IndexY + "), pos souris = " + posX + " " + posY;
            */
        }

        private void ShowCover(Case_Echiquier caseactu) // VERIFIER LIBERATION DE MEMOIRE
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
            if (!HasClicked)
            {
                FindLocation(e.X, e.Y);
                ShowCover(Cases[IndexX, IndexY]);
            }
        }

        private void EcranGame_Resize(object sender, EventArgs e)
        {
            Invalidate();
            RemplirTabCases();
        }

        private void SoldiersIcons1_Click(object sender, EventArgs e) // On clique sur un soldat bdu joueur 1
        {
            if (!ActionEnCours)
            {
                int Index = -1; // Index de la picturebox et par conséquent du soldat sélectionné. -1 Correspond à une erreur
                int Error;
                try
                {
                    PictureBox KnownSender = (PictureBox)sender;
                    var name = KnownSender.Name.Split('_');
                    Index = Convert.ToInt32(name[1]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (Index >= 0)
                {
                    if (!Soldiers1[Index].played)
                    {
                        ActionEnCours = true;
                        EcranAction Eaction = new EcranAction();
                        Eaction.ShowDialog();
                        HasClicked = false;
                        while (!HasClicked) // On attend que l'utilisateur ait cliqué
                        {
                            Application.DoEvents();
                        }
                        switch (Eaction.Choix)
                        {
                            case 0:
                                tsInfo.Text = "Veuillez sélectionner une case";
                                if (Index >= 0)
                                {
                                    Error = Soldiers1[Index].Move(Cases[IndexX, IndexY]);
                                    switch (Error)
                                    {
                                        case 0:
                                            tsInfo.Text = "Soldat déplacé sur la case [" + IndexX + "," + IndexY + "].";
                                            SoldiersIcons1[Index].Location = Soldiers1[Index].position.Origin;
                                            tsAvancement.Increment(1);
                                            NbrSoldatsJoues++;
                                            tsNbrSoldatsJoue.Text = NbrSoldatsJoues + "/" + NbrSoldats;
                                            break;
                                        case 1:
                                            MessageBox.Show("Vous ne pouvez pas déplacer le soldat aussi loin !", "Déplacement impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            break;
                                        case 2:
                                            MessageBox.Show("Vous ne pouvez pas déplacer le soldat sur une case déjà occupée !", "Déplacement impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            break;
                                    }
                                }
                                else
                                    MessageBox.Show("ERREUR : Index négatif ! Impossible de sélectionner le soldat !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case 1:
                                tsInfo.Text = "Renforcement p1";
                                break;
                            case 2:
                                tsInfo.Text = "Tir p1";
                                break;
                            default:
                                tsInfo.Text = "Annulation p1";
                                Eaction.Close();
                                break;
                        }
                        HasClicked = false;
                        ActionEnCours = false;
                        Eaction.Dispose();
                    }
                    else
                        MessageBox.Show("Ce soldat a déjà joué !", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("ERREUR : Index négatif ! Impossible de sélectionner le soldat !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SoldiersIcons2_Click(object sender, EventArgs e) // On clique sur un soldat du joueur 2
        {
            
        }

        private void pbCase_Click(object sender, EventArgs e)
        {
            if (ActionEnCours)
            {
                try
                {
                    PictureBox KnownSender = (PictureBox)sender;
                    FindLocation(KnownSender.Location.X, KnownSender.Location.Y);
                    HasClicked = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsfQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
