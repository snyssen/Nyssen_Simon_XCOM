using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Nyssen_Simon_XCOM
{
    public partial class EcranAccueil : Form
    {
        #region Données membres
        public bool GameLaunch = false; // Si true, on veut lancer une partie (peu importe que ce soit une nouvelle ou une ancienne); sinon on veut quitter le programme
        public bool Setup = false;      // Si true, on veut lancer une NOUVELLE partie; sinon on veut reprendre une en cours (dépendant dans les deux cas de GameLaunch)
        public bool AudioOn; // true si audio on
        private SoundPlayer music = new SoundPlayer(Properties.Resources._01_XCOM2_Lazarus);
        public EcranSetup setup;

        public bool Joueur1Joue;
        public short SelectedbtnIndex;
        public int TimePlayedJ1;
        public int TimePlayedJ2;
        public int NbrTourJoues;

        public List<int> classes_J1;
        public List<bool> covered_J1;
        public List<int> HP_J1;
        public List<bool> alive_J1;
        public List<bool> played_J1;
        public List<int> IndexX_J1;
        public List<int> IndexY_J1;

        public List<int> classes_J2;
        public List<bool> covered_J2;
        public List<int> HP_J2;
        public List<bool> alive_J2;
        public List<bool> played_J2;
        public List<int> IndexX_J2;
        public List<int> IndexY_J2;
        #endregion
        public EcranAccueil()
        {
            InitializeComponent();
            dlgLoadGame.Filter = "Fichier de sauvegarde|*.sav|Tous fichiers|*.*";

            music.PlayLooping();
            AudioOn = true;
            btnAudio.BackgroundImage = Properties.Resources.audio_on;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            GameLaunch = false;
            Setup = false;
            Close();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            music.Stop();
            setup = new EcranSetup(AudioOn);
            setup.ShowDialog();
            if (setup.begin)
            {
                Close();
                GameLaunch = true;
                Setup = true;
            }
            else
                music.PlayLooping();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            GameLaunch = true;
            Setup = false;

            classes_J1 = new List<int>();
            covered_J1 = new List<bool>();
            HP_J1 = new List<int>();
            alive_J1 = new List<bool>();
            played_J1 = new List<bool>();
            IndexX_J1 = new List<int>();
            IndexY_J1 = new List<int>();

            classes_J2 = new List<int>();
            covered_J2 = new List<bool>();
            HP_J2 = new List<int>();
            alive_J2 = new List<bool>();
            played_J2 = new List<bool>();
            IndexX_J2 = new List<int>();
            IndexY_J2 = new List<int>();

            if (dlgLoadGame.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dlgLoadGame.FileName);
                string lecture;
                while ((lecture = sr.ReadLine()) != "") // Conditions générales de parties
                {
                    string[] tab = lecture.Split(';');
                    if (tab[0] == "True") // Tour du joueur
                        Joueur1Joue = true;
                    else
                        Joueur1Joue = false;
                    SelectedbtnIndex = short.Parse(tab[1]); // Map de la partie
                    TimePlayedJ1 = int.Parse(tab[2]);
                    TimePlayedJ2 = int.Parse(tab[3]);
                    NbrTourJoues = int.Parse(tab[4]);
                }
                while ((lecture = sr.ReadLine()) != "") // Soldats du joueur 1
                {
                    string[] tab = lecture.Split(';');
                    classes_J1.Add(int.Parse(tab[0]));
                    covered_J1.Add(tab[1] == "True" ? true : false);
                    HP_J1.Add(int.Parse(tab[2]));
                    alive_J1.Add(tab[3] == "True" ? true : false);
                    played_J1.Add(tab[4] == "True" ? true : false);
                    IndexX_J1.Add(int.Parse(tab[5]));
                    IndexY_J1.Add(int.Parse(tab[6]));
                }
                while ((lecture = sr.ReadLine()) != null) // Soldats du joueur 2
                {
                    string[] tab = lecture.Split(';');
                    classes_J2.Add(int.Parse(tab[0]));
                    covered_J2.Add(tab[1] == "True" ? true : false);
                    HP_J2.Add(int.Parse(tab[2]));
                    alive_J2.Add(tab[3] == "True" ? true : false);
                    played_J2.Add(tab[4] == "True" ? true : false);
                    IndexX_J2.Add(int.Parse(tab[5]));
                    IndexY_J2.Add(int.Parse(tab[6]));
                }
                sr.Close();
                Close();
            }
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

        private void btnAudio_Click(object sender, EventArgs e)
        {
            if (AudioOn)
            {
                music.Stop();
                btnAudio.BackgroundImage = Properties.Resources.audio_off;
                AudioOn = false;
            }
            else
            {
                music.PlayLooping();
                btnAudio.BackgroundImage = Properties.Resources.audio_on;
                AudioOn = true;
            }
        }

        private void EcranAccueil_FormClosing(object sender, FormClosingEventArgs e)
        {
            music.Stop();
            music.Dispose();
        }
    }
}
