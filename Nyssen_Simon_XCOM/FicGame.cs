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
using System.Media;

namespace Nyssen_Simon_XCOM
{
    public partial class EcranGame : Form
    {
        #region Donnees membres
        private short SelectedbtnIndex; // Choix de la carte
        private int NbrFantassins; // Nombre de fantassins (par escouade)
        private int NbrSnipers; // Nombre de snipers (par escouade)
        private int NbrLourds; // Nombre de soldats lourds (par escouade)
        private int NbrLegers; // Nombre de soldats légers (par escouade)
        private int NbrSoldats; // Nombre de soldats toute classe confondue (par escouade)
        private int NbrSoldatsJ1; // Nombre de soldats du joueur 1
        private int NbrSoldatsJ2; // Nombre de soldats du joueur 2
        bool error = false; // Détecte une erreur
        Case_Echiquier[,] Cases = new Case_Echiquier[10, 10]; // Tableau de cases qui forme l'échiquier
        int IndexX, IndexY; // Contient la position (l'index) de la case sur laquelle on travaille
        List<PictureBox> SoldiersIcons1 = new List<PictureBox>(); // Icônes des soldats du joueur 1
        List<PictureBox> SoldiersIcons2 = new List<PictureBox>(); // Icônes des soldats du joueur 2
        List<Soldat> Soldiers1 = new List<Soldat>(); // Soldats du joueur 1 ENCORE EN VIE
        List<Soldat> Soldiers2 = new List<Soldat>(); // Soldats du joueur 2 ENCORE EN VIE
        private bool First = true; // Premier lancement
        private bool FirstTurn; // Premier tour (permet d'attendre que les deux joueurs aient passé leur premier tour avant de pouvoir tirer (lors d'une nouvelle partie))
        private bool ActionEnCours = false; // Une action est-elle en cours (déplacement ou tir)
        private bool HasClicked = false; // Case sélectionnée durant une action => On veut désactiver le FindLocation() de l'event pbCarte_MouseMove()
        private bool Joueur1Joue = true; // Indique quel joueur joue -> si vrai, c'est le joueur 1, si faux c'est le joueur 2
        private short FinPartie = 0; // Indique 0 si la partie est toujours en cours, 1 si le joueur 1 a gagné et 2 si le joueur 2 a gagné
        private bool saved = false; // Vraie si la partie a été sauvegardée et qu'il n'y a pas eu de modif depuis, sinon fausse.
        public bool Relaunch = false; // Mis à vrai si on veut relancer une nouvelle partie (ou en charger une précédente) = > Renvoie au menu principal.
        private bool AudioOn; // vrai si audio actif
        private SoundPlayer music = new SoundPlayer(Properties.Resources._04_XCOM2_Infiltrator); // Musique de fond
        //private SoundPlayer sounds = new SoundPlayer(); // Sons divers // On ne peut pas utiliser 2 SoundPlayer en même temps !
        private short selectedbtnMusic = 0; // Index de la musique -> 0 = infiltrator
                                            //                     -> 1 = Weapons of Choice
                                            //                     -> 2 = New World Order
                                            //                     -> 3 = Ambush
        private int TimePlayedJ1 = 0; // Temps Joué par le joueur 1
        private int TimePlayedJ2 = 0; // Temps Joué par le joueur 2
        private int NbrTourJoues = 0; // Nbr de tous joués (1 tour = tous les soldats d'un joueur ont joué)
        private int NbrSoldatsJouesJ1 = 0;
        private int NbrSoldatsJouesJ2 = 0;

        SocComm Comm;

        #endregion
        #region Constructeurs
        public EcranGame(bool _audio, short Index, int NbrFant, int NbrSnip, int NbrLd, int NbrLg, SocComm _Comm) // Constructeur pour une nouvelle partie
        {
            InitializeComponent();

            this.AudioOn = _audio;
            this.SelectedbtnIndex = Index;
            this.NbrFantassins = NbrFant;
            this.NbrSnipers = NbrSnip;
            this.NbrLourds = NbrLd;
            this.NbrLegers = NbrLg;
            this.NbrSoldats = NbrFantassins + NbrSnipers + NbrLourds + NbrLegers;
            this.NbrSoldatsJ1 = this.NbrSoldatsJ2 = this.NbrSoldats;
            this.FirstTurn = true;
            this.NbrSoldatsJouesJ1 = this.NbrSoldatsJouesJ2 = 0;

            CreationPartie(_Comm);

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
                    Soldiers2.Add(new Soldat(1, Cases[9 - i - NbrFantassins, 9]));
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

                Random rand = new Random();
                Joueur1Joue = rand.Next(1, 3) == 1 ? true : false;
                tsTour.Text = Joueur1Joue ? "Tour du joueur 1" : "Tour du joueur 2";
                if (Comm != null && Comm.IsConnected && Comm.IsServer)
                    Comm.SendMessage("SyncTour:" + Joueur1Joue.ToString());
            }

            else
                Close();
        }

        public EcranGame(bool _audio, short Index, bool TourJoueur, int _TimePlayedJ1, int _TimePlayedJ2, int _NbrTourJoues,
            List<int> classes_J1, List<bool> covered_J1, List<int> HP_J1, List<bool> alive_J1, List<bool> played_J1, List<int> IndexX_J1, List<int> IndexY_J1,
            List<int> classes_J2, List<bool> covered_J2, List<int> HP_J2, List<bool> alive_J2, List<bool> played_J2, List<int> IndexX_J2, List<int> IndexY_J2, SocComm _Comm) // Constructeur pour charger une partie
        {
            InitializeComponent();

            this.AudioOn = _audio;
            this.SelectedbtnIndex = Index;
            this.Joueur1Joue = TourJoueur;
            this.TimePlayedJ1 = _TimePlayedJ1;
            this.TimePlayedJ2 = _TimePlayedJ2;
            this.NbrTourJoues = _NbrTourJoues;
            this.FirstTurn = (NbrTourJoues > 1 ? false : true);

            this.NbrSoldats = classes_J1.Count;
            this.NbrFantassins = this.NbrSnipers = this.NbrLourds = this.NbrLegers = this.NbrSoldatsJ1 = this.NbrSoldatsJ2 = 0;
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
            foreach (bool played in played_J1) // Compte du nombre de soldats ayant joués
            {
                if (played)
                    NbrSoldatsJouesJ1++;
            }
            foreach (bool played in played_J2) // Compte du nombre de soldats ayant joués
            {
                if (played)
                    NbrSoldatsJouesJ2++;
            }

            tsTour.Text = Joueur1Joue ? "Tour du joueur 1" : "Tour du joueur 2";

            CreationPartie(_Comm);

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
                    // On détermine si le soldat a joué ou non
                    if (Soldiers1[i].played)
                        NbrSoldatsJouesJ1++;
                    if (Soldiers2[i].played)
                        NbrSoldatsJouesJ2++;
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

        private void CreationPartie(SocComm _Comm) // Création de la partie, instructions communes aux deux constructeurs
        {
            Comm = _Comm;
            if (Comm != null)
            {
                Comm.IsConnectedChanged += OnConnectionChanged;
                Comm.ReceivedMessageChanged += OnMessageReceived;
                if (Comm.IsServer)
                    this.Text = "Partie en cours - Joueur 1";
                else
                    this.Text = "Partie en cours - Joueur 2";
            }

            timer1.Start();
            tsInfo.Text = "Sélectionnez un soldat à jouer";
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

                if (AudioOn)
                {
                    music.PlayLooping();
                    tsaMuet.Image = Properties.Resources.audio_on;
                }
                else
                    tsaMuet.Image = Properties.Resources.audio_off;

                First = false;
            }

            else
                Close();
        }

        #endregion
        #region Méthodes
        #region Graphisme
        private void RemplirTabCases() // Remplis le tableau de case. Est utilisé au lancement de la partie pour initialiser l'échiquier, et est rappelé à chaque fois qu'on change la taille de la fenêtre pour que la taille des cases soit adaptées
        {
            int Longueur = pbCarte.Width;
            int Hauteur = pbCarte.Height;

            Soldat soldatTmp = null;

            for (int x = 0; x < 10; x++) // Colonnes
            {
                for (int y = 0; y < 10; y++) // Lignes
                {
                    if (!First) // On ne passe pas ici au lancement de la fenêtre (sauvegarde des emplacements de soldats)
                    {
                        soldatTmp = null;
                        if (Cases[x, y].soldier != null) // La case actuelle a-t-elle un soldat assigné ?
                            soldatTmp = Cases[x, y].soldier; // Stocke le soldat...
                    }
                    Cases[x, y] = new Case_Echiquier(x * Longueur / 10, y * Hauteur / 10, (x + 1) * Longueur / 10, (y + 1) * Hauteur / 10, x, y);
                    if (!First) // On ne passe pas ici au lancement de la fenêtre
                    {
                        if (soldatTmp != null)
                        {
                            Cases[x, y].soldier = soldatTmp; // ... Réassignes le soldat à la case nouvellement créée
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
        private void ShowCover(Case_Echiquier caseactu) // Change l'image de pbCase pour refléter le niveau de couverture de la case entrée en argument et replace cette PB sur la case
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
                pbCase.Location = Cases[IndexX, IndexY].Origin;
                pbCase.Visible = true;
            }
        }
        private void DessinerEchiquier() // Dessine toutes les cases de l'échiquier sur la carte
        {
            foreach (Case_Echiquier c in Cases)
            {
                c.DessinerCase(pbCarte.Handle);
            }
        }
        public void DessinerEtats() // Dessine les indicateurs d'états des différents soldats. Etats = 1) appartenance et 2) A déjà joué ou non
        {
            for (int i = 0; i < SoldiersIcons1.Count; i++)
            {
                if (Soldiers1[i].alive) // Soldats du joueurs 1. On ne dessine que sur les soldats encore en vie
                {
                    Graphics grS1 = Graphics.FromHwnd(SoldiersIcons1[i].Handle);
                    grS1.FillEllipse(new SolidBrush(Color.Yellow), SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10, SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10); // Rond jaune pour indiquer l'appartenance au J1
                    if (Soldiers1[i].played)
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Red), 9 * SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10, SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10); // Rond rouge si il a déjà joué
                    }
                    else
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Green), 9 * SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10, SoldiersIcons1[i].Width / 10, SoldiersIcons1[i].Height / 10); // Rond vert si il doit encore jouer
                    }
                }
                if (Soldiers2[i].alive) // Soldats du joueurs 2
                {
                    Graphics grS1 = Graphics.FromHwnd(SoldiersIcons2[i].Handle);
                    grS1.FillEllipse(new SolidBrush(Color.Violet), SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10, SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10); // Rond violet (apparaît rose à l'écran pour une raison inconnue) pour indiquer l'appartenance au J2
                    if (Soldiers2[i].played)
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Red), 9 * SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10, SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10); // Rond rouge si il a déjà joué
                    }
                    else
                    {
                        grS1.FillEllipse(new SolidBrush(Color.Green), 9 * SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10, SoldiersIcons2[i].Width / 10, SoldiersIcons2[i].Height / 10); // Rond vert si il doit encore jouer
                    }
                }
            }
        }
        private void pbCarte_Paint(object sender, PaintEventArgs e) // Dessine ce qu'il faut
        {
            DessinerEchiquier();
            DessinerEtats();
        }
        private void EcranGame_Resize(object sender, EventArgs e) // On change la taille de la fenêtre => Il faut redéfinir les cases et redessiner
        {
            RemplirTabCases();
            Invalidate();
        }
        #endregion
        #region Gameplay
        private void ChangeTour() // Change le tour
        {
            if (Joueur1Joue)
                NbrSoldatsJouesJ1++;
            else
                NbrSoldatsJouesJ2++;

            // Est-ce que le joueur a joué tous ses soldats ?
            if ((Joueur1Joue ? NbrSoldatsJ1 : NbrSoldatsJ2) - (Joueur1Joue ? NbrSoldatsJouesJ1 : NbrSoldatsJouesJ2) <= 0)
            {
                NbrTourJoues++;
                if (NbrTourJoues > 1) // Les deux joueurs ont joué chacun de leurs soldats au moins une fois => Ils peuvent maintenant tirer
                    FirstTurn = false;
                if (Joueur1Joue)
                {
                    foreach (Soldat soldier in Soldiers1)
                    {
                        soldier.covered = false;
                        soldier.played = false;
                    }
                    MessageBox.Show("Joueur 1 a joué tous ses soldats !");
                    NbrSoldatsJouesJ1 = 0;
                }
                else
                {
                    foreach (Soldat soldier in Soldiers2)
                    {
                        soldier.covered = false;
                        soldier.played = false;
                    }
                    MessageBox.Show("Joueur 2 a joué tous ses soldats !");
                    NbrSoldatsJouesJ2 = 0;
                }
            }

            Joueur1Joue = !Joueur1Joue;
            tsTour.Text = Joueur1Joue ? "Tour du joueur 1" : "Tour du joueur 2";
            if(Joueur1Joue)
            {
                tsAvancement.Value = NbrSoldatsJouesJ1;
                tsAvancement.Maximum = NbrSoldatsJ1;
                tsNbrSoldatsJoue.Text = NbrSoldatsJouesJ1 + "/" + NbrSoldatsJ1;
            }
            else
            {
                tsAvancement.Value = NbrSoldatsJouesJ2;
                tsAvancement.Maximum = NbrSoldatsJ2;
                tsNbrSoldatsJoue.Text = NbrSoldatsJouesJ2 + "/" + NbrSoldatsJ2;
            }
        }
        private void Gagne(bool ActionAnnulee) // Détermine si la partie est terminée ou non
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

            if (FinPartie != 0) // La partie est gagnée
            {
                if (MessageBox.Show("Le joueur " + (FinPartie == 1 ? "1 " : "2 ") + "gagne la partie !\n\n Voulez-vous retourner au menu principal ?", "Fin de la partie", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                    this.Relaunch = true;
                this.saved = true;
                this.Close();
            }
            else // Partie non terminée, on teste si le joueur a fini son tour
            {
                if (!ActionAnnulee)
                    ChangeTour();

            }
            tsInfo.Text = "Sélectionnez un soldat à jouer";
        }
        private void FindLocation(int posX, int posY) // Détermine la case sur laquelle on se trouve à partir de coordonnées posX, posY. Renvoie cette position en changeant IndexX et IndexY
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
        }
        private void LoadCover() // Génère les niveaux de couvertures de toutes les cases en fonction de la carte qui a été choisie
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
        private void DoAction(List<PictureBox> SoldiersIcons, List<Soldat> Soldiers, object sender) // Permet d'effectuer une action. 2 cas principaux : Il n'y a pas d'action en cours => On en choisit une OU il y a déjà une action en cours => on finit cette action
        {
            bool ActionAnnulee = false;
            if (!ActionEnCours) // Pas encore d'action en cours => Sélection du soldat pour en choisir une
            {
                int Index = -1; // Index de la picturebox et par conséquent du soldat sélectionné. -1 Correspond à une erreur
                int Error;
                try
                {
                    PictureBox KnownSender = (PictureBox)sender;
                    // Je récupère le nom de la picturebox pour savoir le numéro de soldat
                    // Cela nous permet plus loin de consulter la liste de soldats
                    var name = KnownSender.Name.Split('_');
                    Index = Convert.ToInt32(name[1]);
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // DEBUG
                //Console.WriteLine("soldat sélectionné ! -> num " + Index);
                if (Index >= 0)
                {
                    if (!Soldiers[Index].played) // On vérifie que le soldat sélectionné n'a pas déjà joué
                    {
                        ActionEnCours = true;
                        EcranAction Eaction = new EcranAction(this.FirstTurn);
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
                                            MoveSoldier(SoldiersIcons[Index], Soldiers[Index], false, Index);
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
                                ReinforceSoldier(SoldiersIcons[Index], Soldiers[Index], false, Index);
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
                                Error = Soldiers[Index].Attack(Cases[IndexX, IndexY].soldier); // Action de tir
                                AttackSoldier(Error, SoldiersIcons[Index], (SoldiersIcons == SoldiersIcons1 ? SoldiersIcons2 : SoldiersIcons1) /* Liste d'icônes des soldats ADVERSES */, Soldiers[Index], false, Index);
                                break;
                            default: // Annulation
                                tsInfo.Text = "Annulation de l'action";
                                Eaction.Close();
                                ActionAnnulee = true;
                                break;
                        }
                        // Retour à l'état par défaut
                        HasClicked = false;
                        ActionEnCours = false;
                        Eaction.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Ce soldat a déjà joué !", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ActionAnnulee = true;
                    }
                }
                else
                    MessageBox.Show("ERREUR : Index négatif ! Impossible de sélectionner le soldat !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else // Action en cours => Sélection du soldat pour lui appliquer une action (lancée par un autre soldat)
            {    // => Change juste IndexX et IndexY 
                if (!HasClicked)
                {
                    PictureBox KnownSender = null;
                    try
                    {
                        KnownSender = (PictureBox)sender;
                        FindLocation(KnownSender.Location.X, KnownSender.Location.Y);
                        // DEBUG
                        //Console.WriteLine("Position du soldat trouvée ! -> (" + IndexX + ";" + IndexY + ")");
                        HasClicked = true;
                        ActionAnnulee = true;
                    }
                    catch (InvalidCastException ex)
                    {
                        MessageBox.Show("ERREUR : Le contrôle générant cet événement n'est pas une PictureBox !\n" + ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            Gagne(ActionAnnulee); // On teste la condition de victoire
            
        }
        #region Actions
        // Index dans ces fonctions important uniquement pour la comm
        private void MoveSoldier(PictureBox SoldierIcon, Soldat Soldier, bool IsOnlineUpdate, int Index)
        {
            if (IsOnlineUpdate)
            {
                Soldier.Move(Cases[IndexX, IndexY]);
            }
            tsInfo.Text = "Soldat déplacé sur la case [" + IndexX + "," + IndexY + "].";
            SoldierIcon.Location = Soldier.position.Origin;
            ttInfos.SetToolTip(SoldierIcon, Soldier.AfficherStats());
            if (!IsOnlineUpdate && Comm != null && Comm.IsConnected)
            {
                Comm.SendMessage("Move:" + Index + ";" + IndexX + ";" + IndexY);
            }
        }
        private void ReinforceSoldier(PictureBox SoldierIcon, Soldat Soldier, bool IsOnlineUpdate, int Index)
        {
            Soldier.TakeCover();
            tsInfo.Text = "Position renforcée";
            ttInfos.SetToolTip(SoldierIcon, Soldier.AfficherStats());
            if (!IsOnlineUpdate && Comm != null && Comm.IsConnected)
            {
                Comm.SendMessage("Cover:" + Index);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Error">Code d'erreur renvoyé par Soldat.Attack()</param>
        /// <param name="SoldierIcon">Icône du soldat qui tire</param>
        /// <param name="Icons">Liste d'icônes des soldats du joueur visé</param>
        /// <param name="Soldier">Soldat qui tire</param>
        /// <param name="IsOnlineUpdate">Vrai si effectué par sync avec l'autre terminal</param>
        /// <param name="Index">Index, utile seulement pour envoyer les données</param>
        private void AttackSoldier(int Error, PictureBox SoldierIcon, List<PictureBox> Icons, Soldat Soldier, bool IsOnlineUpdate, int Index)
        {
            if (Error < 0)
                MessageBox.Show("ERREUR : cette case n'a pas de niveau de couverture défini !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (Error == 0) // Le tir a touché
                {
                    if (IsOnlineUpdate)
                        Cases[IndexX, IndexY].soldier.HP = Soldier.damage; // En cas sync, il faut retirer les points de vie au soldat visé

                    if (Cases[IndexX, IndexY].soldier.alive) // Le soldat visé est encore en vie
                    {
                        MessageBox.Show("Le tir tir a touché ! Le soldat visé a reçu " + Soldier.damage + " points de dégâts, ce qui amène sa vie à " + Cases[IndexX, IndexY].soldier.HP);
                        foreach (PictureBox pb in Icons)
                        {
                            if (pb.Location == Cases[IndexX, IndexY].Origin)
                            {
                                ttInfos.SetToolTip(pb, Cases[IndexX, IndexY].soldier.AfficherStats()); // Mise à jour des stats du soldats
                                break;
                            }
                        }
                    }
                    else // Le soldat visé est mort
                    {
                        MessageBox.Show("Le tir tir a touché ! Le soldat visé a reçu " + Soldier.damage + " points de dégâts, ce qui l'a achevé !");
                        Cases[IndexX, IndexY].soldier = null; // On retire le soldat de la case
                        foreach (PictureBox pb in Icons)
                        {
                            if (pb.Location == Cases[IndexX, IndexY].Origin) // Et on cherche la pb du soldat tué pour la supprimer
                            {
                                pb.Enabled = false;
                                pb.Dispose();
                                Controls.Remove(pb);
                                break;
                            }
                        }
                        if (Joueur1Joue) // Enfin, on décrémente le nombre de soldats du joueur qui vient de perdre un soldat
                            NbrSoldatsJ2--;
                        else
                            NbrSoldatsJ1--;
                    }
                }
                else // Le tir a raté
                {
                    MessageBox.Show("Le tir a échoué...");
                }
                // Mise à jour des infos affichées à l'écran
                ttInfos.SetToolTip(SoldierIcon, Soldier.AfficherStats());
            }
            if (!IsOnlineUpdate && Comm != null && Comm.IsConnected)
            {
                Comm.SendMessage("Attack:" + Index + ";" + IndexX + ";" + IndexY + ";" + Error);
            }
        }
        #endregion
        #endregion
        #region Musiques
        private void ChangeSoundtrack()
        {
            music.Stop();
            switch (selectedbtnMusic)
            {
                case 0: // infiltrator
                    music.Stream = Properties.Resources._04_XCOM2_Infiltrator;
                    musique1ToolStripMenuItem.Image = Properties.Resources.audio_play;
                    musique2ToolStripMenuItem.Image = null;
                    musique3ToolStripMenuItem.Image = null;
                    musique4ToolStripMenuItem.Image = null;
                    break;
                case 1: // Weapons of Choice
                    music.Stream = Properties.Resources._14_XCOM2_Weapons_of_Choice;
                    musique1ToolStripMenuItem.Image = null;
                    musique2ToolStripMenuItem.Image = Properties.Resources.audio_play;
                    musique3ToolStripMenuItem.Image = null;
                    musique4ToolStripMenuItem.Image = null;
                    break;
                case 2: // New World Order
                    music.Stream = Properties.Resources._21_XCOM2_New_World_Order;
                    musique1ToolStripMenuItem.Image = null;
                    musique2ToolStripMenuItem.Image = null;
                    musique3ToolStripMenuItem.Image = Properties.Resources.audio_play;
                    musique4ToolStripMenuItem.Image = null;
                    break;
                case 3: // Ambush
                    music.Stream = Properties.Resources._27_XCOM2_Ambush;
                    musique1ToolStripMenuItem.Image = null;
                    musique2ToolStripMenuItem.Image = null;
                    musique3ToolStripMenuItem.Image = null;
                    musique4ToolStripMenuItem.Image = Properties.Resources.audio_play;
                    break;
            }
            music.Load();
            if (AudioOn)
                music.PlayLooping();
        }
        #endregion
        #region Autre
        public void Sauvegarde() // Sauvegarde la partie dans un fichier d'extension .sav. Ce fichier est un pseudo CSV, on utilise des ";" pour séparer les variables et des lignes vides pour séparer les classes. Le texte est en clair dans le fichier.
        {
            if (dlgSauvegarder.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(dlgSauvegarder.FileName);
                sw.WriteLine(Joueur1Joue + ";" + SelectedbtnIndex + ";" + TimePlayedJ1 + ";" + TimePlayedJ2 + ";" + NbrTourJoues); // Qui joue et sur quelle map + duree partie + NbrTourJoues
                sw.WriteLine(""); // On sépare avec une ligne vide
                foreach (Soldat soldier in Soldiers1)
                {
                    sw.WriteLine(soldier.classe + ";" + soldier.covered + ";" + soldier.HP + ";" + soldier.alive + ";" + soldier.played + ";" + soldier.position.IndexX + ";" + soldier.position.IndexY);
                }
                sw.WriteLine(""); // On sépare avec une ligne vide
                foreach (Soldat soldier in Soldiers2)
                {
                    sw.WriteLine(soldier.classe + ";" + soldier.covered + ";" + soldier.HP + ";" + soldier.alive + ";" + soldier.played + ";" + soldier.position.IndexX + ";" + soldier.position.IndexY);
                }
                sw.Close();
                saved = true;
            }
        }

        #endregion
        #endregion
        #region Event handlers
        #region Graphisme
        private void pbCarte_MouseMove(object sender, MouseEventArgs e) // On déplace la souris au dessus de la picturebox ~= hover
        {
            if (!HasClicked)
            {
                FindLocation(e.X, e.Y);
                ShowCover(Cases[IndexX, IndexY]);
            }
        }
        #endregion
        #region Gameplay
        private void SoldiersIcons1_Click(object sender, EventArgs e) // On clique sur un soldat du joueur 1
        {
            if ((Joueur1Joue && Comm == null) || (Joueur1Joue && Comm != null && Comm.IsServer) || ActionEnCours) // Joueur 1 joue (SOIT en solo SOIT online et on est serveur (=J1)) => On sélectionne le soldat pour lui demander une action OU Joueur 2 joue mais Action en cours => ce soldat est ciblé par un tir ennemi
                DoAction(SoldiersIcons1, Soldiers1, sender);
            else
                MessageBox.Show("C'est au tour du joueur " + (Joueur1Joue ? "1" : "2") + " de jouer !", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void SoldiersIcons2_Click(object sender, EventArgs e) // On clique sur un soldat du joueur 2
        {
            if ((!Joueur1Joue && Comm == null) || (!Joueur1Joue && Comm != null && !Comm.IsServer) || ActionEnCours)  // Joueur 2 joue (SOIT en solo SOIT online et on est client (=J2)) => On sélectionne le soldat pour lui demander une action OU Joueur 1 joue mais Action en cours => ce soldat est ciblé par un tir ennemi
                DoAction(SoldiersIcons2, Soldiers2, sender);
            else
                MessageBox.Show("C'est au tour du joueur " + (Joueur1Joue ? "1" : "2") + " de jouer !", "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void pbCase_Click(object sender, EventArgs e) // On clique sur une case (qui est alors obscurcie par la pbCase qui affiche son niveau de couverture)
        {
            if (ActionEnCours) // Action en cours => On veut se rendre sur une case OU tirer sur un soldat. Dans les deux cas l'événement relève la position, DoAction() choisit ce qu'il en fait
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
        #endregion
        #region Musiques
        private void tsaMuet_Click(object sender, EventArgs e)
        {
            AudioOn = !AudioOn;
            if (AudioOn)
            {
                music.PlayLooping();
                tsaMuet.Image = Properties.Resources.audio_on;
                tsaMuet.Text = "audio on";
            }
            else
            {
                music.Stop();
                tsaMuet.Image = Properties.Resources.audio_off;
                tsaMuet.Text = "audio off";
            }
        }
        private void musique1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedbtnMusic = 0;
            ChangeSoundtrack();
        }
        private void musique2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedbtnMusic = 1;
            ChangeSoundtrack();
        }
        private void musique3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedbtnMusic = 2;
            ChangeSoundtrack();
        }
        private void musique4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedbtnMusic = 3;
            ChangeSoundtrack();
        }
        #endregion
        #region Autres
        private void EcranGame_FormClosed(object sender, FormClosedEventArgs e) // Juste une assurance que tout est bien supprimé, normalement pas nécessaire
        {
            this.Dispose();
        }
        private void tsfQuitter_Click(object sender, EventArgs e) // On appuie sur le bouton "Quitter" => On ferme la fenêtre
        {
            Close();
        }
        private void EcranGame_FormClosing(object sender, FormClosingEventArgs e) // En cas de fermeture sans sauvegarde, on s'assure de ce que veut l'utilisateur
        {
            if (!error) // On ne veut pas interrompre la fermeture de la fenêtre si elle est dûe à une erreur
            {
                if (!saved) // Si la partie n'est pas sauvegarder on demande à l'utilisateur si il veut : 1) Sauvegarder avant de quitter 2) Quitter sans sauvegarder 3) Annuler => Annule la fermeture et la partie continue
                {
                    DialogResult message = MessageBox.Show("Voulez vous sauvegarder la partie avant de quitter ?", "Confirmer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (message == DialogResult.Yes) // 1) 
                        Sauvegarde();
                    else // 2) ou 3)
                    {
                        if (message == DialogResult.Cancel) // 3)
                            e.Cancel = true;
                    }
                }
                music.Stop();
                music.Dispose();
            }
        }
        private void tsfSauvegarder_Click(object sender, EventArgs e) // Sauvegarde de la partie
        {
            Sauvegarde();
        }
        private void tsfRetourMenu_Click(object sender, EventArgs e) // Permet de retourner au menu principal
        {
            this.Relaunch = true;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Joueur1Joue)
            {
                TimePlayedJ1++;
                int TmpTimePlayed = TimePlayedJ1;
                int Hours = 0, Minutes = 0;
                if (TmpTimePlayed >= 3600) // Plus d'1 heure
                {
                    while (TmpTimePlayed / 3600 >= 1)
                    {
                        Hours++;
                        TmpTimePlayed -= 3600;
                    }
                }
                if (TmpTimePlayed >= 60) // Plus d'1 minute
                {
                    while (TmpTimePlayed / 60 >= 1)
                    {
                        Minutes++;
                        TmpTimePlayed -= 60;
                    }
                }

                tsTimer.Text = String.Format("{0:00}", Hours) + ":" + String.Format("{0:00}", Minutes) + ":" + String.Format("{0:00}", TmpTimePlayed);
            }
            else
            {
                TimePlayedJ2++;
                int TmpTimePlayed = TimePlayedJ2;
                int Hours = 0, Minutes = 0;
                if (TmpTimePlayed >= 3600) // Plus d'1 heure
                {
                    while (TmpTimePlayed / 3600 >= 1)
                    {
                        Hours++;
                        TmpTimePlayed -= 3600;
                    }
                }
                if (TmpTimePlayed >= 60) // Plus d'1 minute
                {
                    while (TmpTimePlayed / 60 >= 1)
                    {
                        Minutes++;
                        TmpTimePlayed -= 60;
                    }
                }

                tsTimer.Text = String.Format("{0:00}", Hours) + ":" + String.Format("{0:00}", Minutes) + ":" + String.Format("{0:00}", TmpTimePlayed);
            }
        }

        private void tsHelp_Click(object sender, EventArgs e)
        {
            EcranAide aide = new EcranAide();
            aide.ShowDialog();
        }
        #endregion
        #region Communications
        private void OnMessageReceived(object sender, EventArgs e)
        {
            SocComm TmpSoc = (SocComm)sender;
            string[] RawData = TmpSoc.ReceivedMessage.Split(':');
            string Type = RawData[0];
            string[] Data = RawData[1].Split(';');
            Data[Data.Length - 1] = Data[Data.Length - 1].TrimEnd('\0'); // On retire le padding du message

            TreatMessage(Type, Data);
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            SocComm TmpSoc = (SocComm)sender;
            if (!TmpSoc.IsConnected)
            {
                MessageBox.Show("La connexion a été perdue, vous êtes désigné vainqueur !");
                this.Invoke((MethodInvoker)(() => {
                    this.Relaunch = true;
                    this.saved = true;
                    this.Comm = null;
                    this.Close();
                }));
            }
        }

        private void TreatMessage(string Type, string[] Data)
        {
            switch (Type)
            {
                case "SyncTour": // Au démarrage de la partie, le serveur envoie à qui est le tour
                    Joueur1Joue = bool.Parse(Data[0]);
                    tsTour.Text = Joueur1Joue ? "Tour du joueur 1" : "Tour du joueur 2";
                    break;

                case "Move": // L'autre terminal a déplacé un de ses soldats
                    // Data[0] = Index, Data[1] = IndexX, Data[2] = IndexY
                    IndexX = int.Parse(Data[1]);
                    IndexY = int.Parse(Data[2]);
                    if (Joueur1Joue) // C'est le joueur 1 qui vient de jouer
                    {
                        this.Invoke((MethodInvoker)(() => MoveSoldier(SoldiersIcons1[int.Parse(Data[0])], Soldiers1[int.Parse(Data[0])], true, 0)));
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)(() => MoveSoldier(SoldiersIcons2[int.Parse(Data[0])], Soldiers2[int.Parse(Data[0])], true, 0)));
                    }
                    Gagne(false); // Vérifie les conditions de victoire et fait passer le tour
                    break;
                case "Cover": // L'autre terminal a renforcé la position d'un de ses soldats
                    // Data[0] = Index
                    if (Joueur1Joue)
                    {
                        this.Invoke((MethodInvoker)(() => ReinforceSoldier(SoldiersIcons1[int.Parse(Data[0])], Soldiers1[int.Parse(Data[0])], true, 0)));
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)(() => ReinforceSoldier(SoldiersIcons2[int.Parse(Data[0])], Soldiers2[int.Parse(Data[0])], true, 0)));
                    }
                    Gagne(false); // Vérifie les conditions de victoire et fait passer le tour
                    break;
                case "Attack": // L'autre terminal a tiré sur un des soldats de ce terminal
                    // Data[0] = Index, Data[1] = IndexX, Data[2] = IndexY, Data[3] = Error
                    IndexX = int.Parse(Data[1]);
                    IndexY = int.Parse(Data[2]);
                    if (Joueur1Joue)
                    {
                        this.Invoke((MethodInvoker)(() => AttackSoldier(int.Parse(Data[3]), SoldiersIcons1[int.Parse(Data[0])], SoldiersIcons2, Soldiers1[int.Parse(Data[0])], true, 0)));
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)(() => AttackSoldier(int.Parse(Data[3]), SoldiersIcons2[int.Parse(Data[0])], SoldiersIcons1, Soldiers2[int.Parse(Data[0])], true, 0)));
                    }
                    Gagne(false); // Vérifie les conditions de victoire et fait passer le tour
                    break;
            }
        }
        #endregion
        #endregion
    }
}
