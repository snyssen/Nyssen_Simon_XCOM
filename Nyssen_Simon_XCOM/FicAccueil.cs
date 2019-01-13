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
        public SocComm Comm;

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

            AudioOn = false;
            btnAudio.BackgroundImage = Properties.Resources.audio_off;
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
            if (Comm != null && Comm.IsConnected)
            {
                if (Comm.IsServer)
                    Comm.SendMessage("Cmd:NewGame");
                Comm.ReceivedMessageChanged -= this.OnMessageReceived;
                Comm.IsConnectedChanged -= this.OnConnectionChanged;
            }
            setup = new EcranSetup(AudioOn, this.Comm);
            setup.ShowDialog();
            if (setup.Comm != null)
            {
                this.Comm = setup.Comm;
                setup.Comm = null;
            }
            if (setup.begin)
            {
                Close();
                GameLaunch = true;
                Setup = true;
            }
            else
            {
                if (AudioOn)
                    music.PlayLooping();
                if (Comm != null && Comm.IsConnected)
                {
                    Comm.ReceivedMessageChanged += this.OnMessageReceived;
                    Comm.IsConnectedChanged += this.OnConnectionChanged;
                }
            }
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            if (dlgLoadGame.ShowDialog() == DialogResult.OK)
            {
                if (Comm != null && Comm.IsConnected)
                    Comm.SendMessage("Load:" + File.ReadAllText(dlgLoadGame.FileName));
                LoadSaveFile(dlgLoadGame.FileName);
            }
        }

        private void LoadSaveFile(string FileName)
        {
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

            StreamReader sr = new StreamReader(FileName);
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
            GameLaunch = true;
            Setup = false;
            Close();
        }

        private void llblCopyright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://snyssen.be");
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

        private void BtnMultijoueur_Click(object sender, EventArgs e)
        {
            EcranMulti multi = new EcranMulti();
            multi.ShowDialog();
            this.Comm = multi.Comm;
            if (Comm != null && Comm.IsConnected)
            {
                if (Comm.IsServer)
                {
                    this.Text = "XCOM - Joueur 1";
                }
                else
                {
                    this.Text = "XCOM - Joueur 2";
                }

                this.btnNewGame.Enabled = this.btnLoadGame.Enabled = Comm.IsServer;
                Comm.ReceivedMessageChanged += this.OnMessageReceived;
                Comm.IsConnectedChanged += this.OnConnectionChanged;
            } 
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            SocComm TmpSoc = (SocComm)sender;
            if (!TmpSoc.IsConnected)
            {
                MessageBox.Show("La connexion a été perdue !");
                btnNewGame.Invoke((MethodInvoker)(() => { btnNewGame.Enabled = true; }));
                btnLoadGame.Invoke((MethodInvoker)(() => { btnLoadGame.Enabled = true; }));
                this.Comm = null;
            }
        }

        private void OnMessageReceived(object sender, EventArgs e)
        {
            SocComm TmpSoc = (SocComm)sender;
            string[] RawData = TmpSoc.ReceivedMessage.Split(':');
            string Type = RawData[0];
            RawData[1] = RawData[1].TrimEnd('\0'); // On retire le padding du message
            if (Type == "Load") // On veut sauvegarder les datas dans un fichier pour le lire
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "Tmp.sav", RawData[1]);
                this.Invoke((MethodInvoker)(() => { LoadSaveFile(System.AppDomain.CurrentDomain.BaseDirectory + "Tmp.sav"); }));
            }
            else
            {
                string[] Data = RawData[1].Split(';');

                TreatMessage(Type, Data);
            }
        }

        private void TreatMessage(string Type, string[] Data)
        {
            switch (Type)
            {
                case "Cmd": // Commande de lancement d'une méthode
                    switch (Data[0])
                    {
                        case "NewGame":
                            // BeginInvoke est non bloquant => permet à ce thread de finir son travail (+ attente pour être sûr qu'il ait bien fini)
                            // Important pour que le socket puisse être transmis à la fenêtre qui sera ouverte (sinon il est bloqué au milieu de sa réception)
                            this.BeginInvoke((MethodInvoker)(() => { System.Threading.Thread.Sleep(10); this.btnNewGame_Click(null, null); }));
                            break;
                    }
                    break;
            }
        }
    }
}
