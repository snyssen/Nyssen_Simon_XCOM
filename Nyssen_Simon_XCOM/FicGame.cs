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
using System.IO;

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
        private int NbrSoldatsJ1; // Nombre de soldats du joueur 1
        private int NbrSoldatsJ2; // Nombre de soldats du joueur 2
        bool error = false; // Détecte une erreur
        Case_Echiquier[,] Cases = new Case_Echiquier[10,10]; // Tableau de cases qui forme l'échiquier
        int IndexX, IndexY; // Contient la position (l'index) de la case sur laquelle on travaille
        List<PictureBox> SoldiersIcons1 = new List<PictureBox>(); // Icônes des soldats du joueur 1
        List<PictureBox> SoldiersIcons2 = new List<PictureBox>(); // Icônes des soldats du joueur 2
        List<Soldat> Soldiers1 = new List<Soldat>(); // Soldats du joueur 1 ENCORE EN VIE
        List<Soldat> Soldiers2 = new List<Soldat>(); // Soldats du joueur 2 ENCORE EN VIE
        private bool First = true; // Premier lancement
        private bool ActionEnCours = false; // Une action est-elle en cours (déplacement ou tir)
        private bool HasClicked = false; // Case sélectionnée durant une action => On veut désactiver le FindLocation() de l'event pbCarte_MouseMove()
        private bool Joueur1Joue = true; // Indique quel joueur joue -> si vrai, c'est le joueur 1, si faux c'est le joueur 2
        private short FinPartie = 0; // Indique 0 si la partie est toujours en cours, 1 si le joueur 1 a gagné et 2 si le joueur 2 a gagné
        private bool saved = false; // Vraie si la partie a été sauvegardée et qu'il n'y a pas eu de modif depuis, sinon fausse.

        public EcranGame(short Index, int NbrFant, int NbrSnip, int NbrLd, int NbrLg)
        {
            InitializeComponent();

            this.SelectedbtnIndex = Index;
            this.NbrFantassins = NbrFant;
            this.NbrSnipers = NbrSnip;
            this.NbrLourds = NbrLd;
            this.NbrLegers = NbrLg;
            this.NbrSoldats = NbrFantassins + NbrSnipers + NbrLourds + NbrLegers;
            this.NbrSoldatsJ1 = this.NbrSoldatsJ2 = this.NbrSoldats;
            this.NbrSoldatsJoues = 0;

            CreationPartie();

            if (!error)
            {

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
                    ttInfos.SetToolTip(SoldiersIcons1[i], Soldiers1[j].AfficherStats());
                    ttInfos.SetToolTip(SoldiersIcons2[i], Soldiers2[j].AfficherStats());
                    j++;
                }
                for (int i = 0; i < NbrSnipers; i++) // Snipers
                {
                    Soldiers1.Add(new Soldat(1, Cases[i + NbrFantassins, 0]));
                    Cases[i + NbrFantassins, 0].soldier = Soldiers1[j];
                    Soldiers2.Add(new Soldat(1, Cases[9 - i-NbrFantassins, 9]));
                    Cases[9 - i - NbrFantassins, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i + NbrFantassins].Image = Properties.Resources.Sniper;
                    SoldiersIcons2[i + NbrFantassins].Image = Properties.Resources.Sniper;
                    SoldiersIcons1[i + NbrFantassins].Parent = pbCarte;
                    SoldiersIcons2[i + NbrFantassins].Parent = pbCarte;
                    SoldiersIcons1[i + NbrFantassins].Location = Cases[i + NbrFantassins, 0].Origin;
                    SoldiersIcons2[i + NbrFantassins].Location = Cases[9 - i - NbrFantassins, 9].Origin;
                    ttInfos.SetToolTip(SoldiersIcons1[i + NbrFantassins], Soldiers1[j].AfficherStats());
                    ttInfos.SetToolTip(SoldiersIcons2[i + NbrFantassins], Soldiers2[j].AfficherStats());
                    j++;
                }
                for (int i = 0; i < NbrLourds; i++) // Soldats lourds
                {
                    Soldiers1.Add(new Soldat(2, Cases[i + NbrFantassins + NbrSnipers, 0]));
                    Cases[i + NbrFantassins + NbrSnipers, 0].soldier = Soldiers1[j];
                    Soldiers2.Add(new Soldat(2, Cases[9 - i - NbrFantassins - NbrSnipers, 9]));
                    Cases[9 - i - NbrFantassins - NbrSnipers, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers].Image = Properties.Resources.Lourd;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers].Image = Properties.Resources.Lourd;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers].Parent = pbCarte;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers].Parent = pbCarte;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers].Location = Cases[i + NbrFantassins + NbrSnipers, 0].Origin;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers].Location = Cases[9 - i - NbrFantassins - NbrSnipers, 9].Origin;
                    ttInfos.SetToolTip(SoldiersIcons1[i + NbrFantassins + NbrSnipers], Soldiers1[j].AfficherStats());
                    ttInfos.SetToolTip(SoldiersIcons2[i + NbrFantassins + NbrSnipers], Soldiers2[j].AfficherStats());
                    j++;
                }
                for (int i = 0; i < NbrLegers; i++) // Soldats légers
                {
                    Soldiers1.Add(new Soldat(3, Cases[i + NbrFantassins + NbrSnipers + NbrLourds, 0]));
                    Cases[i + NbrFantassins + NbrSnipers + NbrLourds, 0].soldier = Soldiers1[j];
                    Soldiers2.Add(new Soldat(3, Cases[9 - i - NbrFantassins - NbrSnipers - NbrLourds, 9]));
                    Cases[9 - i - NbrFantassins - NbrSnipers - NbrLourds, 9].soldier = Soldiers2[j];
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds].Image = Properties.Resources.Leger;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds].Image = Properties.Resources.Leger;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds].Parent = pbCarte;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds].Parent = pbCarte;
                    SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds].Location = Cases[i + NbrFantassins + NbrSnipers + NbrLourds, 0].Origin;
                    SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds].Location = Cases[9 - i - NbrFantassins - NbrSnipers - NbrLourds, 9].Origin;
                    ttInfos.SetToolTip(SoldiersIcons1[i + NbrFantassins + NbrSnipers + NbrLourds], Soldiers1[j].AfficherStats());
                    ttInfos.SetToolTip(SoldiersIcons2[i + NbrFantassins + NbrSnipers + NbrLourds], Soldiers2[j].AfficherStats());
                    j++;
                }
            }

            else
                Close();
        }

        public EcranGame(short Index, bool TourJoueur, 
            List<int> classes_J1, List<bool> covered_J1, List<int> HP_J1, List<bool> alive_J1, List<bool> played_J1, List<int> IndexX_J1, List<int> IndexY_J1, 
            List<int> classes_J2, List<bool> covered_J2, List<int> HP_J2, List<bool> alive_J2, List<bool> played_J2, List<int> IndexX_J2, List<int> IndexY_J2)
        {
            InitializeComponent();

            this.SelectedbtnIndex = Index;
            this.Joueur1Joue = TourJoueur;

            this.NbrSoldats = classes_J1.Count;
            this.NbrFantassins = this.NbrSnipers = this.NbrLourds = this.NbrLegers = this.NbrSoldatsJ1 = this.NbrSoldatsJ2 = this.NbrSoldatsJoues = 0;
            foreach (int classe in classes_J1) // Compte des classes
            {
                switch (classe)
                {
                    case 0: // Fantassin
                        this.NbrFantassins++;
                        break;
                    case 1: // Sniper
                        this.NbrSnipers++;
                        break;
                    case 2: // Lourd
                        this.NbrLourds++;
                        break;
                    case 3: // Léger
                        this.NbrLegers++;
                        break;
                    default: // Indéfini => Erreur !
                        this.error = true;
                        break;
                }
            }
            foreach (bool alive in alive_J1) // Compte des soldats en vie du joueur 1
            {
                if (alive)
                    this.NbrSoldatsJ1++;
            }
            foreach (bool alive in alive_J2) // Compte des soldats en vie du joueur 2
            {
                if (alive)
                    this.NbrSoldatsJ2++;
            }
            foreach (bool played in (this.Joueur1Joue ? played_J1 : played_J2)) // Compte du nombre de soldats ayant joués
            {
                if (played)
                    NbrSoldatsJoues++;
            }

            CreationPartie();

            if (!error)
            {
                for (int i = 0; i < NbrSoldats; i++)
                {
                    // On créée les soldats
                    Soldiers1.Add(new Soldat(classes_J1[i], covered_J1[i], HP_J1[i], alive_J1[i], played_J1[i]));
                    Soldiers2.Add(new Soldat(classes_J2[i], covered_J2[i], HP_J2[i], alive_J2[i], played_J2[i]));
                    // On leur assigne leur position
                    Soldiers1[i].position = Cases[IndexX_J1[i], IndexY_J1[i]];
                    Soldiers2[i].position = Cases[IndexX_J2[i], IndexY_J2[i]];
                    // Et on prévient cette position qu'elle a un soldat dessus
                    Cases[IndexX_J1[i], IndexY_J1[i]].soldier = Soldiers1[i];
                    Cases[IndexX_J2[i], IndexY_J2[i]].soldier = Soldiers2[i];
                    // On définit les icônes
                    switch (classes_J1[i]) // On sait que les classes sont toujours ordonnées dans le même ordre, et que les 2 joueurs ont le même nombre de chaque => Pas besoin d'utiliser 2 switch blocks
                    {
                        case 0: // Fantassin
                            SoldiersIcons1[i].Image = Properties.Resources.Fantassin;
                            SoldiersIcons2[i].Image = Properties.Resources.Fantassin;
                            break;
                        case 1: // Sniper
                            SoldiersIcons1[i].Image = Properties.Resources.Sniper;
                            SoldiersIcons2[i].Image = Properties.Resources.Sniper;
                            break;
                        case 2: // Lourd
                            SoldiersIcons1[i].Image = Properties.Resources.Lourd;
                            SoldiersIcons2[i].Image = Properties.Resources.Lourd;
                            break;
                        case 3: // Leger
                            SoldiersIcons1[i].Image = Properties.Resources.Leger;
                            SoldiersIcons2[i].Image = Properties.Resources.Leger;
                            break;
                        default: // Indéfini => Erreur
                            error = true;
                            break;
                    }
                    SoldiersIcons1[i].Parent = pbCarte;
                    SoldiersIcons2[i].Parent = pbCarte;
                    SoldiersIcons1[i].Location = Cases[IndexX_J1[i], IndexY_J1[i]].Origin;
                    SoldiersIcons2[i].Location = Cases[IndexX_J2[i], IndexY_J2[i]].Origin;
                    ttInfos.SetToolTip(SoldiersIcons1[i], Soldiers1[i].AfficherStats());
                    ttInfos.SetToolTip(SoldiersIcons2[i], Soldiers2[i].AfficherStats());

                    // On élimine les picturebox des soldats morts
                    if (!Soldiers1[i].alive)
                    {
                        SoldiersIcons1[i].Enabled = false;
                        SoldiersIcons1[i].Dispose();
                        Controls.Remove(SoldiersIcons1[i]);
                    }
                    if (!Soldiers2[i].alive)
                    {
                        SoldiersIcons2[i].Enabled = false;
                        SoldiersIcons2[i].Dispose();
                        Controls.Remove(SoldiersIcons2[i]);
                    }
                }
            }
        }

        private void CreationPartie()
        {
            tsInfo.Text = "";
            tsTour.Text = "Tour du joueur 1";
            dlgSauvegarder.Filter = "Fichier de sauvegarde|*.sav|Tous fichiers|*.*";
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
                            soldatTmp = Cases[x, y].soldier; // Stocke le soldat... (l. 186)
                    }
                    Cases[x, y] = new Case_Echiquier(x * Longueur / 10, y * Hauteur / 10, (x + 1) * Longueur / 10, (y + 1) * Hauteur / 10, x, y);
                    if (!First) // On ne passe pas ici au lancement de la fenêtre
                    {
                        if (soldatTmp != null)
                        {
                            Cases[x, y].soldier = soldatTmp; // ... Pour le réassigner à la case nouvellement créée (l. 178)
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
                    //Console.WriteLine("Case[" + x + ";" + y + "] cree. Elle a pour valeurs :\n\t\tPoint d'origine : (" + Cases[x, y].posX + ";" + Cases[x, y].posY + ")\n\t\tPoint de chute : (" + Cases[x, y].Xmax + ";" + Cases[x, y].Ymax + ")");
                }
            }

            LoadCover();

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

        private void ChangeTour()
        {
            if (Joueur1Joue)
                foreach (Soldat soldier in Soldiers2)
                    soldier.covered = false;
            else
                foreach (Soldat soldier in Soldiers1)
                    soldier.covered = false;
            Joueur1Joue = !Joueur1Joue;
            tsTour.Text = Joueur1Joue ? "Tour du joueur 1" : "Tour du joueur 2";
        }

        private void Gagne() // Détermine si la partie est terminée ou non
        {
            if (NbrSoldatsJ1 <= 0)
                FinPartie = 2;
            else
            {
                if (NbrSoldatsJ2 <= 0)
                    FinPartie = 1;
                else
                    FinPartie = 0;
            }
        }

        private void FindLocation(int posX, int posY) // VERIFIER LIBERATION DE MEMOIRE
        {
            bool Trouve = false;
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
            
            /*
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

        private void LoadCover()
        {
            switch (SelectedbtnIndex)
            {
                case 0: // desert map
                    Cases[0, 0].Cover = 0;
                    Cases[1, 0].Cover = 0;
                    Cases[2, 0].Cover = 1;
                    Cases[3, 0].Cover = 1;
                    Cases[4, 0].Cover = 1;
                    Cases[5, 0].Cover = 0;
                    Cases[6, 0].Cover = 0;
                    Cases[7, 0].Cover = 0;
                    Cases[8, 0].Cover = 0;
                    Cases[9, 0].Cover = 1;
                    Cases[0, 1].Cover = 0;
                    Cases[1, 1].Cover = 0;
                    Cases[2, 1].Cover = 1;
                    Cases[3, 1].Cover = 0;
                    Cases[4, 1].Cover = 2;
                    Cases[5, 1].Cover = 2;
                    Cases[6, 1].Cover = 0;
                    Cases[7, 1].Cover = 0;
                    Cases[8, 1].Cover = 0;
                    Cases[9, 1].Cover = 0;
                    Cases[0, 2].Cover = 0;
                    Cases[1, 2].Cover = 0;
                    Cases[2, 2].Cover = 1;
                    Cases[3, 2].Cover = 0;
                    Cases[4, 2].Cover = 1;
                    Cases[5, 2].Cover = 1;
                    Cases[6, 2].Cover = 0;
                    Cases[7, 2].Cover = 0;
                    Cases[8, 2].Cover = 0;
                    Cases[9, 2].Cover = 0;
                    Cases[0, 3].Cover = 0;
                    Cases[1, 3].Cover = 0;
                    Cases[2, 3].Cover = 0;
                    Cases[3, 3].Cover = 0;
                    Cases[4, 3].Cover = 0;
                    Cases[5, 3].Cover = 0;
                    Cases[6, 3].Cover = 0;
                    Cases[7, 3].Cover = 0;
                    Cases[8, 3].Cover = 0;
                    Cases[9, 3].Cover = 0;
                    Cases[0, 4].Cover = 0;
                    Cases[1, 4].Cover = 0;
                    Cases[2, 4].Cover = 0;
                    Cases[3, 4].Cover = 0;
                    Cases[4, 4].Cover = 0;
                    Cases[5, 4].Cover = 0;
                    Cases[6, 4].Cover = 0;
                    Cases[7, 4].Cover = 0;
                    Cases[8, 4].Cover = 0;
                    Cases[9, 4].Cover = 0;
                    Cases[0, 5].Cover = 0;
                    Cases[1, 5].Cover = 0;
                    Cases[2, 5].Cover = 0;
                    Cases[3, 5].Cover = 0;
                    Cases[4, 5].Cover = 0;
                    Cases[5, 5].Cover = 0;
                    Cases[6, 5].Cover = 0;
                    Cases[7, 5].Cover = 1;
                    Cases[8, 5].Cover = 1;
                    Cases[9, 5].Cover = 0;
                    Cases[0, 6].Cover = 0;
                    Cases[1, 6].Cover = 0;
                    Cases[2, 6].Cover = 1;
                    Cases[3, 6].Cover = 0;
                    Cases[4, 6].Cover = 0;
                    Cases[5, 6].Cover = 1;
                    Cases[6, 6].Cover = 0;
                    Cases[7, 6].Cover = 0;
                    Cases[8, 6].Cover = 0;
                    Cases[9, 6].Cover = 1;
                    Cases[0, 7].Cover = 1;
                    Cases[1, 7].Cover = 0;
                    Cases[2, 7].Cover = 1;
                    Cases[3, 7].Cover = 0;
                    Cases[4, 7].Cover = 0;
                    Cases[5, 7].Cover = 1;
                    Cases[6, 7].Cover = 1;
                    Cases[7, 7].Cover = 0;
                    Cases[8, 7].Cover = 0;
                    Cases[9, 7].Cover = 1;
                    Cases[0, 8].Cover = 2;
                    Cases[1, 8].Cover = 0;
                    Cases[2, 8].Cover = 0;
                    Cases[3, 8].Cover = 0;
                    Cases[4, 8].Cover = 1;
                    Cases[5, 8].Cover = 0;
                    Cases[6, 8].Cover = 0;
                    Cases[7, 8].Cover = 2;
                    Cases[8, 8].Cover = 1;
                    Cases[9, 8].Cover = 1;
                    Cases[0, 9].Cover = 1;
                    Cases[1, 9].Cover = 1;
                    Cases[2, 9].Cover = 0;
                    Cases[3, 9].Cover = 0;
                    Cases[4, 9].Cover = 0;
                    Cases[5, 9].Cover = 0;
                    Cases[6, 9].Cover = 1;
                    Cases[7, 9].Cover = 2;
                    Cases[8, 9].Cover = 0;
                    Cases[9, 9].Cover = 0;
                    break;
                case 1: // snowy map
                    Cases[0, 0].Cover = 0;
                    Cases[1, 0].Cover = 1;
                    Cases[2, 0].Cover = 1;
                    Cases[3, 0].Cover = 0;
                    Cases[4, 0].Cover = 0;
                    Cases[5, 0].Cover = 0;
                    Cases[6, 0].Cover = 0;
                    Cases[7, 0].Cover = 1;
                    Cases[8, 0].Cover = 1;
                    Cases[9, 0].Cover = 0;
                    Cases[0, 1].Cover = 1;
                    Cases[1, 1].Cover = 0;
                    Cases[2, 1].Cover = 0;
                    Cases[3, 1].Cover = 0;
                    Cases[4, 1].Cover = 2;
                    Cases[5, 1].Cover = 2;
                    Cases[6, 1].Cover = 2;
                    Cases[7, 1].Cover = 1;
                    Cases[8, 1].Cover = 1;
                    Cases[9, 1].Cover = 0;
                    Cases[0, 2].Cover = 0;
                    Cases[1, 2].Cover = 1;
                    Cases[2, 2].Cover = 0;
                    Cases[3, 2].Cover = 0;
                    Cases[4, 2].Cover = 0;
                    Cases[5, 2].Cover = 0;
                    Cases[6, 2].Cover = 2;
                    Cases[7, 2].Cover = 0;
                    Cases[8, 2].Cover = 1;
                    Cases[9, 2].Cover = 0;
                    Cases[0, 3].Cover = 0;
                    Cases[1, 3].Cover = 1;
                    Cases[2, 3].Cover = 1;
                    Cases[3, 3].Cover = 1;
                    Cases[4, 3].Cover = 0;
                    Cases[5, 3].Cover = 0;
                    Cases[6, 3].Cover = 0;
                    Cases[7, 3].Cover = 2;
                    Cases[8, 3].Cover = 1;
                    Cases[9, 3].Cover = 0;
                    Cases[0, 4].Cover = 0;
                    Cases[1, 4].Cover = 0;
                    Cases[2, 4].Cover = 0;
                    Cases[3, 4].Cover = 2;
                    Cases[4, 4].Cover = 1;
                    Cases[5, 4].Cover = 0;
                    Cases[6, 4].Cover = 0;
                    Cases[7, 4].Cover = 0;
                    Cases[8, 4].Cover = 1;
                    Cases[9, 4].Cover = 0;
                    Cases[0, 5].Cover = 0;
                    Cases[1, 5].Cover = 0;
                    Cases[2, 5].Cover = 0;
                    Cases[3, 5].Cover = 0;
                    Cases[4, 5].Cover = 2;
                    Cases[5, 5].Cover = 1;
                    Cases[6, 5].Cover = 0;
                    Cases[7, 5].Cover = 0;
                    Cases[8, 5].Cover = 0;
                    Cases[9, 5].Cover = 0;
                    Cases[0, 6].Cover = 0;
                    Cases[1, 6].Cover = 0;
                    Cases[2, 6].Cover = 0;
                    Cases[3, 6].Cover = 0;
                    Cases[4, 6].Cover = 2;
                    Cases[5, 6].Cover = 2;
                    Cases[6, 6].Cover = 0;
                    Cases[7, 6].Cover = 0;
                    Cases[8, 6].Cover = 1;
                    Cases[9, 6].Cover = 0;
                    Cases[0, 7].Cover = 1;
                    Cases[1, 7].Cover = 1;
                    Cases[2, 7].Cover = 0;
                    Cases[3, 7].Cover = 0;
                    Cases[4, 7].Cover = 0;
                    Cases[5, 7].Cover = 2;
                    Cases[6, 7].Cover = 0;
                    Cases[7, 7].Cover = 1;
                    Cases[8, 7].Cover = 0;
                    Cases[9, 7].Cover = 0;
                    Cases[0, 8].Cover = 1;
                    Cases[1, 8].Cover = 2;
                    Cases[2, 8].Cover = 0;
                    Cases[3, 8].Cover = 0;
                    Cases[4, 8].Cover = 0;
                    Cases[5, 8].Cover = 1;
                    Cases[6, 8].Cover = 1;
                    Cases[7, 8].Cover = 0;
                    Cases[8, 8].Cover = 1;
                    Cases[9, 8].Cover = 0;
                    Cases[0, 9].Cover = 0;
                    Cases[1, 9].Cover = 1;
                    Cases[2, 9].Cover = 0;
                    Cases[3, 9].Cover = 0;
                    Cases[4, 9].Cover = 0;
                    Cases[5, 9].Cover = 0;
                    Cases[6, 9].Cover = 1;
                    Cases[7, 9].Cover = 0;
                    Cases[8, 9].Cover = 0;
                    Cases[9, 9].Cover = 0;
                    break;
                case 2: // urban map
                    Cases[0, 0].Cover = 0;
                    Cases[1, 0].Cover = 0;
                    Cases[2, 0].Cover = 0;
                    Cases[3, 0].Cover = 0;
                    Cases[4, 0].Cover = 2;
                    Cases[5, 0].Cover = 2;
                    Cases[6, 0].Cover = 1;
                    Cases[7, 0].Cover = 2;
                    Cases[8, 0].Cover = 1;
                    Cases[9, 0].Cover = 0;
                    Cases[0, 1].Cover = 0;
                    Cases[1, 1].Cover = 1;
                    Cases[2, 1].Cover = 1;
                    Cases[3, 1].Cover = 1;
                    Cases[4, 1].Cover = 1;
                    Cases[5, 1].Cover = 1;
                    Cases[6, 1].Cover = 1;
                    Cases[7, 1].Cover = 1;
                    Cases[8, 1].Cover = 1;
                    Cases[9, 1].Cover = 0;
                    Cases[0, 2].Cover = 0;
                    Cases[1, 2].Cover = 2;
                    Cases[2, 2].Cover = 1;
                    Cases[3, 2].Cover = 1;
                    Cases[4, 2].Cover = 1;
                    Cases[5, 2].Cover = 0;
                    Cases[6, 2].Cover = 0;
                    Cases[7, 2].Cover = 2;
                    Cases[8, 2].Cover = 1;
                    Cases[9, 2].Cover = 0;
                    Cases[0, 3].Cover = 0;
                    Cases[1, 3].Cover = 0;
                    Cases[2, 3].Cover = 0;
                    Cases[3, 3].Cover = 2;
                    Cases[4, 3].Cover = 0;
                    Cases[5, 3].Cover = 0;
                    Cases[6, 3].Cover = 0;
                    Cases[7, 3].Cover = 1;
                    Cases[8, 3].Cover = 0;
                    Cases[9, 3].Cover = 0;
                    Cases[0, 4].Cover = 0;
                    Cases[1, 4].Cover = 2;
                    Cases[2, 4].Cover = 2;
                    Cases[3, 4].Cover = 1;
                    Cases[4, 4].Cover = 0;
                    Cases[5, 4].Cover = 1;
                    Cases[6, 4].Cover = 1;
                    Cases[7, 4].Cover = 0;
                    Cases[8, 4].Cover = 1;
                    Cases[9, 4].Cover = 0;
                    Cases[0, 5].Cover = 0;
                    Cases[1, 5].Cover = 2;
                    Cases[2, 5].Cover = 1;
                    Cases[3, 5].Cover = 1;
                    Cases[4, 5].Cover = 0;
                    Cases[5, 5].Cover = 1;
                    Cases[6, 5].Cover = 1;
                    Cases[7, 5].Cover = 0;
                    Cases[8, 5].Cover = 1;
                    Cases[9, 5].Cover = 0;
                    Cases[0, 6].Cover = 0;
                    Cases[1, 6].Cover = 0;
                    Cases[2, 6].Cover = 0;
                    Cases[3, 6].Cover = 0;
                    Cases[4, 6].Cover = 0;
                    Cases[5, 6].Cover = 0;
                    Cases[6, 6].Cover = 0;
                    Cases[7, 6].Cover = 0;
                    Cases[8, 6].Cover = 0;
                    Cases[9, 6].Cover = 0;
                    Cases[0, 7].Cover = 0;
                    Cases[1, 7].Cover = 2;
                    Cases[2, 7].Cover = 1;
                    Cases[3, 7].Cover = 1;
                    Cases[4, 7].Cover = 0;
                    Cases[5, 7].Cover = 1;
                    Cases[6, 7].Cover = 1;
                    Cases[7, 7].Cover = 0;
                    Cases[8, 7].Cover = 1;
                    Cases[9, 7].Cover = 0;
                    Cases[0, 8].Cover = 1;
                    Cases[1, 8].Cover = 2;
                    Cases[2, 8].Cover = 1;
                    Cases[3, 8].Cover = 1;
                    Cases[4, 8].Cover = 0;
                    Cases[5, 8].Cover = 1;
                    Cases[6, 8].Cover = 2;
                    Cases[7, 8].Cover = 2;
                    Cases[8, 8].Cover = 1;
                    Cases[9, 8].Cover = 0;
                    Cases[0, 9].Cover = 0;
                    Cases[1, 9].Cover = 0;
                    Cases[2, 9].Cover = 0;
                    Cases[3, 9].Cover = 0;
                    Cases[4, 9].Cover = 0;
                    Cases[5, 9].Cover = 0;
                    Cases[6, 9].Cover = 1;
                    Cases[7, 9].Cover = 2;
                    Cases[8, 9].Cover = 0;
                    Cases[9, 9].Cover = 0;
                    break;
                default:
                    MessageBox.Show("ERREUR ! Pas de carte sélectionnée !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    break;
            }
        }

        private void DoAction(List<PictureBox> SoldiersIcons, List<Soldat> Soldiers, object sender)
        {
            if (!ActionEnCours) // Pas encore d'action en cours => Sélection du soldat pour en choisir une
            {
                int Index = -1; // Index de la picturebox et par conséquent du soldat sélectionné. -1 Correspond à une erreur
                int Error;
                try
                {
                    PictureBox KnownSender = (PictureBox)sender;
                    var name = KnownSender.Name.Split('_');
                    Index = Convert.ToInt32(name[1]);
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // DEBUG
                Console.WriteLine("soldat sélectionné ! -> num " + Index);
                if (Index >= 0)
                {
                    if (!Soldiers[Index].played)
                    {
                        ActionEnCours = true;
                        EcranAction Eaction = new EcranAction();
                        Eaction.ShowDialog();
                        switch (Eaction.Choix)
                        {
                            case 0: // Déplacement du soldat
                                tsInfo.Text = "Veuillez sélectionner une case";
                                do
                                {
                                    HasClicked = false;
                                    while (!HasClicked) // On attend que l'utilisateur ait cliqué
                                    {
                                        Application.DoEvents();
                                    }
                                    Error = Soldiers[Index].Move(Cases[IndexX, IndexY]);
                                    switch (Error)
                                    {
                                        case 0:
                                            tsInfo.Text = "Soldat déplacé sur la case [" + IndexX + "," + IndexY + "].";
                                            SoldiersIcons[Index].Location = Soldiers[Index].position.Origin;
                                            ttInfos.SetToolTip(SoldiersIcons[Index], Soldiers[Index].AfficherStats());
                                            tsAvancement.Increment(1);
                                            NbrSoldatsJoues++;
                                            tsNbrSoldatsJoue.Text = NbrSoldatsJoues + "/" + (Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2);
                                            break;
                                        case 1:
                                            MessageBox.Show("Vous ne pouvez pas déplacer le soldat aussi loin !", "Déplacement impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            break;
                                        case 2:
                                            MessageBox.Show("Vous ne pouvez pas déplacer le soldat sur une case déjà occupée !", "Déplacement impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            break;
                                    }
                                }
                                while (Error != 0);
                                break;
                            case 1: // Renforcement du soldat
                                Soldiers[Index].TakeCover();
                                tsInfo.Text = "Position renforcée";
                                tsAvancement.Increment(1);
                                NbrSoldatsJoues++;
                                tsNbrSoldatsJoue.Text = NbrSoldatsJoues + "/" + (Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2);
                                ttInfos.SetToolTip(SoldiersIcons[Index], Soldiers[Index].AfficherStats());
                                break;
                            case 2: // Tir initié par le soldat
                                tsInfo.Text = "Veuillez sélectionner un soldat à viser";
                                HasClicked = false;
                                while (!HasClicked)// On attend que l'utilisateur ait cliqué
                                {
                                    Application.DoEvents();
                                    if (Cases[IndexX, IndexY].soldier == null) // La dernière case sélectionnée est vide. Il faut donc continuer à tourner vu que cette case n'est pas valide
                                        HasClicked = false;
                                    else // La case sélectionnée contient un soldat...
                                    {
                                        foreach (Soldat soldier in Soldiers) // ...Mais on doit s'assurer que ce soldat est un soldat adverse
                                        {
                                            if (Cases[IndexX, IndexY].soldier == soldier)
                                            {
                                                MessageBox.Show("Vous ne pouvez pas tirer sur un soldat allié !", "Tir impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                HasClicked = false;
                                                System.Threading.Thread.Sleep(100); // Il faut attendre un peu pour que l'utilisateur déplace sa souris (nécessaire pour fermer le popup) => que  pbCarte_MouseMove() entre en action, rappelle FindLocation() et change ainsi les Index, ce qui évite de ré-ouvrir le popup une seconde fois
                                                break;
                                            }
                                        }
                                    }
                                }
                                Error = Soldiers[Index].Attack(Cases[IndexX, IndexY].soldier);
                                if (Error < 0)
                                    MessageBox.Show("ERREUR : cette case n'a pas de niveau de couverture défini !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                {
                                    if (Error == 0)
                                    {
                                        if (Cases[IndexX, IndexY].soldier.alive)
                                        {
                                            MessageBox.Show("Le tir tir a touché ! Le soldat visé a reçu " + Soldiers[Index].damage + " points de dégâts, ce qui amène sa vie à " + Cases[IndexX, IndexY].soldier.HP);
                                            List<PictureBox> Icons = (SoldiersIcons == SoldiersIcons1 ? SoldiersIcons2 : SoldiersIcons1);
                                            foreach (PictureBox pb in Icons)
                                            {
                                                Console.WriteLine("Boucle de recherche de la PictureBox du soldat blessé");
                                                if (pb.Location == Cases[IndexX, IndexY].Origin)
                                                {
                                                    Console.WriteLine("PictureBox du soldat blessé trouvée !");
                                                    ttInfos.SetToolTip(pb, Cases[IndexX, IndexY].soldier.AfficherStats());
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Le tir tir a touché ! Le soldat visé a reçu " + Soldiers[Index].damage + " points de dégâts, ce qui a achevé le soldat !");
                                            Cases[IndexX, IndexY].soldier = null;
                                            List<PictureBox> Icons = (SoldiersIcons == SoldiersIcons1 ? SoldiersIcons2 : SoldiersIcons1);
                                            foreach (PictureBox pb in Icons)
                                            {
                                                Console.WriteLine("Boucle de recherche de la PictureBox du soldat tué");
                                                if (pb.Location == Cases[IndexX, IndexY].Origin)
                                                {
                                                    Console.WriteLine("PictureBox du soldat tué trouvée !");
                                                    pb.Enabled = false;
                                                    pb.Dispose();
                                                    Controls.Remove(pb);
                                                    break;
                                                }
                                            }
                                            if (Joueur1Joue)
                                                NbrSoldatsJ2--;
                                            else
                                                NbrSoldatsJ1--;
                                        }
                                    }
                                    else
                                        MessageBox.Show("Le tir a échoué...");
                                    ttInfos.SetToolTip(SoldiersIcons[Index], Soldiers[Index].AfficherStats());
                                    tsAvancement.Increment(1);
                                    NbrSoldatsJoues++;
                                    tsNbrSoldatsJoue.Text = NbrSoldatsJoues + "/" + (Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2);
                                }
                                break;
                            default: // Annulation
                                tsInfo.Text = "Annulation de l'action";
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
            else // Action en cours => Sélection du soldat pour lui appliquer une action (lancée par un autre soldat)
            {
                if (!HasClicked)
                {
                    PictureBox KnownSender = null;
                    try
                    {
                        KnownSender = (PictureBox)sender;
                        FindLocation(KnownSender.Location.X, KnownSender.Location.Y);
                        // DEBUG
                        Console.WriteLine("Position du soldat trouvée ! -> (" + IndexX + ";" + IndexY + ")");
                        HasClicked = true;
                    }
                    catch (InvalidCastException ex)
                    {
                        MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            Gagne();
            if (FinPartie != 0)
            {
                MessageBox.Show("Le joueur " + (FinPartie == 1 ? "1 " : "2 ") + "gagne la partie !", "Fin de la partie", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.Close();
            }
            else
            {
                if ((Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2) - NbrSoldatsJoues == 0) // Tous les soldats de l'escouade ont joué => Changement de joueur
                {
                    MessageBox.Show("Tour du joueur " + (Joueur1Joue ? "1 " : "2 ") + "terminé.\nAppuyez sur OK pour continuer", "Fin du tour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ChangeTour();
                    NbrSoldatsJoues = 0;
                    tsAvancement.Value = 0;
                    tsAvancement.Maximum = (Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2);
                    tsNbrSoldatsJoue.Text = NbrSoldatsJoues + "/" + (Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2);
                    int i = 0;
                    foreach (Soldat soldier in Soldiers)
                    {
                        soldier.played = false;
                        ttInfos.SetToolTip(SoldiersIcons[i], soldier.AfficherStats());
                        i++;
                    }
                }
            }
        }

        private void DessinerEchiquier()
        {
            foreach (Case_Echiquier c in Cases)
            {
                c.DessinerCase(pbCarte.Handle);
            }
        }

        public void DessinerEtats()
        {
            for (int i = 0; i < SoldiersIcons1.Count; i++)
            {
                if (Soldiers1[i].alive)
                {
                    Graphics grS1 = Graphics.FromHwnd(SoldiersIcons1[i].Handle);
                    grS1.FillEllipse(new SolidBrush(Color.Yellow), SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10, SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10);
                    if (Soldiers1[i].played)
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Red), 9 * SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10, SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10);
                    }
                    else
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Green), 9 * SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10, SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10);
                    }
                }
                if (Soldiers2[i].alive)
                {
                    Graphics grS1 = Graphics.FromHwnd(SoldiersIcons2[i].Handle);
                    grS1.FillEllipse(new SolidBrush(Color.Violet), SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10, SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10);
                    if (Soldiers2[i].played)
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Red), 9 * SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10, SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10);
                    }
                    else
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Green), 9 * SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10, SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10);
                    }
                }
            }
        }

        private void pbCarte_MouseMove(object sender, MouseEventArgs e) // On déplace la souris au dessus de la picturebox ~= hover
        {
            if (!HasClicked)
            {
                FindLocation(e.X, e.Y);
                ShowCover(Cases[IndexX, IndexY]);
            }
        }

        private void SoldiersIcons1_Click(object sender, EventArgs e) // On clique sur un soldat du joueur 1
        {
            if (Joueur1Joue || ActionEnCours)
                DoAction(SoldiersIcons1, Soldiers1, sender);
            else
                MessageBox.Show("C'est au tour du joueur 2 de jouer !", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void SoldiersIcons2_Click(object sender, EventArgs e) // On clique sur un soldat du joueur 2
        {
            if (!Joueur1Joue || ActionEnCours) 
                DoAction(SoldiersIcons2, Soldiers2, sender);
            else
                MessageBox.Show("C'est au tour du joueur 1 de jouer !", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EcranGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void pbCarte_Paint(object sender, PaintEventArgs e)
        {
            DessinerEchiquier();
            DessinerEtats();
        }

        private void EcranGame_Resize(object sender, EventArgs e)
        {
            RemplirTabCases();
            Invalidate();
        }

        private void tsfSauvegarder_Click(object sender, EventArgs e) // Sauvegarde de la partie
        {
            Sauvegarde();
        }

        private void tsfQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EcranGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!error)
            {
                if (!saved)
                {
                    DialogResult message = MessageBox.Show("Voulez vous sauvegarder la partie avant de quitter ?", "Confirmer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (message == DialogResult.Yes)
                        Sauvegarde();
                    else
                    {
                        if (message == DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }
        }

        private void tsfCharger_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fonction non implémentée...");
        }

        public void Sauvegarde()
        {
            if (dlgSauvegarder.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(dlgSauvegarder.FileName);
                sw.WriteLine(Joueur1Joue + ";" + SelectedbtnIndex); // Qui joue et sur quelle map
                sw.WriteLine("");
                foreach (Soldat soldier in Soldiers1)
                {
                    sw.WriteLine(soldier.classe + ";" + soldier.covered + ";" + soldier.HP + ";" + soldier.alive + ";" + soldier.played + ";" + soldier.position.IndexX + ";" + soldier.position.IndexY);
                }
                sw.WriteLine("");
                foreach (Soldat soldier in Soldiers2)
                {
                    sw.WriteLine(soldier.classe + ";" + soldier.covered + ";" + soldier.HP + ";" + soldier.alive + ";" + soldier.played + ";" + soldier.position.IndexX + ";" + soldier.position.IndexY);
                }
                sw.Close();
                saved = true;
            }
        }
    }
}
