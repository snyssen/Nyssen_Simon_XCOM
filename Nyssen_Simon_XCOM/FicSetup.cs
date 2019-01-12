using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Nyssen_Simon_XCOM
{
    public partial class EcranSetup : Form
    {
        public bool begin = false;
        private int NbrSoldats; // Nombres de soldats dans l'escouade, definit par la reglette tbNbrSoldats
        private int NbrSoldatsSelect; // Nombre de soldats déjà assignés
        public short SelectedbtnIndex = 3; // Index de bouton appuyé -> 0 = Desert
                                           //                        -> 1 = Snowy
                                           //                        -> 2 = Urban
        // Nombres de soldats par classe   //                        -> 3 = non assigné
        public int NbrFantassins;
        public int NbrSnipers;
        public int NbrLourds;
        public int NbrLegers;

        private bool AudioOn; // true si le son est activé
        private SoundPlayer music = new SoundPlayer(Properties.Resources._02_XCOM2_First_Flight);
        private short SelectedbtnMusic = 0; // Index de la musique -> 0 = First Flight
                                            //                     -> 1 = Squad Loadout

        private SocComm Comm;

        public EcranSetup(bool _audio, SocComm _Comm)                
        {                                  
            InitializeComponent();
            tbNbrSoldats_Scroll(null, null);

            cbFantassin.SelectedIndex = 1;
            cbSniper.SelectedIndex = 1;
            cbLourd.SelectedIndex = 1;
            cbLeger.SelectedIndex = 1;

            this.AudioOn = _audio;
            if (AudioOn)
            {
                btnAudio.BackgroundImage = Properties.Resources.audio_on;
                music.PlayLooping();
            }
            else
                btnAudio.BackgroundImage = Properties.Resources.audio_off;

            this.Comm = _Comm;
            if (Comm != null && Comm.IsConnected)
            {
                // On s'abonne aux événements
                Comm.ReceivedMessageChanged += this.OnMessageReceived;
                Comm.IsConnectedChanged += this.OnConnectionChanged;

                // On désactive tous les contrôles (sauf musique)
                tbNbrSoldats.Enabled = cbFantassin.Enabled = cbSniper.Enabled = cbLourd.Enabled = cbLeger.Enabled
                    = btnDesert.Enabled = btnSnowy.Enabled = btnUrban.Enabled = btnLancer.Enabled = false;

                if (!Comm.IsServer) // Si on est client...
                {
                    // ...On demande une synchronisation avec le serveur
                    Comm.SendMessage("AskSync:null");
                    // On prévient l'utilisateur qu'il doit attendre, mais dans un thread pour ne pas bloquer l'application
                    Task.Run(() => { MessageBox.Show("Le joueur 1 est en train de règler les paramètres de la partie..."); });
                }
            }
        }

        private void tbNbrSoldats_Scroll(object sender, EventArgs e)
        {
            NbrSoldats = tbNbrSoldats.Value;
            cbFantassin.Items.Clear();
            cbSniper.Items.Clear();
            cbLourd.Items.Clear();
            cbLeger.Items.Clear();

            for (int i = 0; i <= NbrSoldats; i++) // On réassigne le nombre max de chaque classe
            {
                cbFantassin.Items.Add(i);
                cbSniper.Items.Add(i);
                cbLourd.Items.Add(i);
                cbLeger.Items.Add(i);
            }
        }

        private void cbFantassin_SelectedIndexChanged(object sender, EventArgs e)
        {
            NbrSoldatsSelect = 0;

            // On fait le compte des soldats assignés
            if (cbFantassin.SelectedItem != null)
                NbrSoldatsSelect += (int)cbFantassin.SelectedItem;
            if (cbSniper.SelectedItem != null)
                NbrSoldatsSelect += (int)cbSniper.SelectedItem;
            if (cbLourd.SelectedItem != null)
                NbrSoldatsSelect += (int)cbLourd.SelectedItem;
            if (cbLeger.SelectedItem != null)
                NbrSoldatsSelect += (int)cbLeger.SelectedItem;

            // Et on refait les listes si elles n'ont pas encore été modifiées
            if (cbFantassin.SelectedItem == null)
            {
                cbFantassin.Items.Clear();
                for (int i = 0; i <= NbrSoldats - NbrSoldatsSelect; i++)
                {
                    cbFantassin.Items.Add(i);
                }
            }
            if (cbSniper.SelectedItem == null)
            {
                cbSniper.Items.Clear();
                for (int i = 0; i <= NbrSoldats - NbrSoldatsSelect; i++)
                {
                    cbSniper.Items.Add(i);
                }
            }
            if (cbLourd.SelectedItem == null)
            {
                cbLourd.Items.Clear();
                for (int i = 0; i <= NbrSoldats - NbrSoldatsSelect; i++)
                {
                    cbLourd.Items.Add(i);
                }
            }
            if (cbLeger.SelectedItem == null)
            {
                cbLeger.Items.Clear();
                for (int i = 0; i <= NbrSoldats - NbrSoldatsSelect; i++)
                {
                    cbLeger.Items.Add(i);
                }
            }
        }

        private void btnDesert_Click(object sender, EventArgs e)
        {
            btnSnowy.Text = btnUrban.Text = "";
            btnDesert.Text = "SELECTION";
            SelectedbtnIndex = 0;
        }

        private void btnSnowy_Click(object sender, EventArgs e)
        {
            btnDesert.Text = btnUrban.Text = "";
            btnSnowy.Text = "SELECTION";
            SelectedbtnIndex = 1;
        }

        private void btnUrban_Click(object sender, EventArgs e)
        {
            btnSnowy.Text = btnDesert.Text = "";
            btnUrban.Text = "SELECTION";
            SelectedbtnIndex = 2;
        }

        private void btnLancer_Click(object sender, EventArgs e)
        {
            if (SelectedbtnIndex == 3 || cbFantassin.SelectedItem == null || cbSniper.SelectedItem == null || cbLourd.SelectedItem == null || cbLeger.SelectedItem == null || NbrSoldats - NbrSoldatsSelect != 0)
            {
                if (SelectedbtnIndex == 3)
                {
                    MessageBox.Show("Vous devez sélectionner un champs de bataille !");
                }
                if (cbFantassin.SelectedItem == null || cbSniper.SelectedItem == null || cbLourd.SelectedItem == null || cbLeger.SelectedItem == null || NbrSoldats - NbrSoldatsSelect != 0)
                {
                    MessageBox.Show("Vous n'avez pas fini d'assigner les rôles des soldats de votre escouade ! ");
                }
            }

            else
            {
                if (MessageBox.Show("Voici les paramètres de cette partie :\n Taille de l'escouade : " + tbNbrSoldats.Value + " soldats dont\n   " + cbFantassin.SelectedItem + " fantassins\n   " + cbSniper.SelectedItem + " Tireurs d'élite\n   " + cbLourd.SelectedItem + " Soldats lourds\n   " + cbLeger.SelectedItem + " Soldats léger.\n\nVoulez-vous lancer la partie ?", "Confirmez le lancement de la partie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    begin = true;
                    NbrFantassins = (int)cbFantassin.SelectedItem;
                    NbrSnipers = (int)cbSniper.SelectedItem;
                    NbrLourds = (int)cbLourd.SelectedItem;
                    NbrLegers = (int)cbLeger.SelectedItem;
                    if (Comm != null && Comm.IsConnected)
                    {
                        Task.Run(() => { MessageBox.Show("En attente de confirmation du joueur 2..."); });
                        SendSync();
                        // On désactive tous les contrôles (sauf musique)
                        tbNbrSoldats.Enabled = cbFantassin.Enabled = cbSniper.Enabled = cbLourd.Enabled = cbLeger.Enabled
                            = btnDesert.Enabled = btnSnowy.Enabled = btnUrban.Enabled = btnLancer.Enabled = false;
                    }
                    else
                        this.Close();
                }
                else
                    tbNbrSoldats_Scroll(null, null);
            }
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

        private void btnNextMusic_Click(object sender, EventArgs e)
        {
            if (SelectedbtnMusic == 0)
                SelectedbtnMusic = 1;
            else
                SelectedbtnMusic = 0;
            ChangeSoundtrack();
        }

        private void btnPrevMusic_Click(object sender, EventArgs e)
        {
            if (SelectedbtnMusic == 0)
                SelectedbtnMusic = 1;
            else
                SelectedbtnMusic = 0;
            ChangeSoundtrack();
        }

        private void ChangeSoundtrack()
        {
            music.Stop();
            switch (SelectedbtnMusic)
            {
                case 0:
                    music.Stream = Properties.Resources._02_XCOM2_First_Flight;
                    lblMusic.Text = "musique 1";
                    break;
                case 1:
                    music.Stream = Properties.Resources._08_XCOM2_Squad_Loadout;
                    lblMusic.Text = "musique 2";
                    break;
            }
            music.Load();
            if (AudioOn)
                music.PlayLooping();
        }


        private void OnMessageReceived(object sender, EventArgs e)
        {
            SocComm TmpSoc = (SocComm)sender;
            string[] RawData = TmpSoc.ReceivedMessage.Split(':');
            string Type = RawData[0];
            string[] Data = RawData[1].Split(';');
            Data[Data.Length - 1] = Data[Data.Length - 1].TrimEnd('\0'); // On retire le padding du message

            TreatMessage(Type, Data);
        }
        private void TreatMessage(string Type, string[] Data)
        {
            switch (Type)
            {
                case "AskSync": // Le client demande une synchronisation avec le serveur => il est prêt et le serveur peut paramètrer la partie
                    this.Invoke((MethodInvoker)(() =>
                    {
                        tbNbrSoldats.Enabled = cbFantassin.Enabled = cbSniper.Enabled = cbLourd.Enabled = cbLeger.Enabled
                        = btnDesert.Enabled = btnSnowy.Enabled = btnUrban.Enabled = btnLancer.Enabled = true;
                    }));
                    break;
                case "Sync": // Le serveur envoie un message de synchronisation des données au client
                    AskForConf(Data);
                    break;
                case "Conf": // Le client confirme (ou non) qu'il accepte les paramètres
                    TreatConf(Data);
                    break;
                case "Start": // Le serveur indique que la partie peut commencer
                    begin = true;
                    this.Invoke((MethodInvoker)(() => { this.Close(); }));
                    break;
            }
        }

        private void TreatConf(string[] Data)
        {
            bool IsConf = bool.Parse(Data[0]);
            if (IsConf) // Joueur 2 a confirmé qu'il acceptait les paramètres de la partie
            {
                Comm.SendMessage("Start:null");
                this.Invoke((MethodInvoker)(() => { this.Close(); }));
            }
            else // Joueur 2 a refusé les paramètres de la partie
            {
                MessageBox.Show("Le joueur 2 a refusé les paramètres de partie que vous avez proposé...");
                begin = false;
                //tbNbrSoldats.Invoke((MethodInvoker)(() => { tbNbrSoldats_Scroll(null, null); }));
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    tbNbrSoldats.Enabled = cbFantassin.Enabled = cbSniper.Enabled = cbLourd.Enabled = cbLeger.Enabled
                        = btnDesert.Enabled = btnSnowy.Enabled = btnUrban.Enabled = btnLancer.Enabled = true;
                    tbNbrSoldats_Scroll(null, null);
                }));
            }
        }

        private void SendSync()
        {
            Comm.SendMessage
            (
            "Sync:" + NbrFantassins + ";" + NbrSnipers + ";" + NbrLourds + ";" + NbrLegers + ";" + SelectedbtnIndex
            );
        }

        private void AskForConf(string[] Data)
        {
            NbrFantassins = int.Parse(Data[0]);
            NbrSnipers = int.Parse(Data[1]);
            NbrLourds = int.Parse(Data[2]);
            NbrLegers = int.Parse(Data[3]);
            NbrSoldatsSelect = NbrFantassins + NbrSnipers + NbrLourds + NbrLegers;
            SelectedbtnIndex = short.Parse(Data[4]);

            string SelectedMap = "";
            switch (SelectedbtnIndex)
            {
                case 0:
                    SelectedMap = "Désert";
                    break;
                case 1:
                    SelectedMap = "Enneigée";
                    break;
                case 2:
                    SelectedMap = "Urbaine";
                    break;
            }

            if
            (
                MessageBox.Show
                (
                    "Voici les paramètres de cette partie:\n Taille de l'escouade : "
                    + NbrSoldatsSelect + " soldats dont\n   " + NbrFantassins + " fantassins\n   "
                    + NbrSnipers + " Tireurs d'élite\n   " + NbrLourds + " Soldats lourds\n   "
                    + NbrLegers + " Soldats légers.\n\nCarte sélectionnée : " + SelectedMap,
                    "Confirmer",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question
                 )
                 == DialogResult.Yes
            ) // Joueur 2 accepte les paramètres
            {
                Comm.SendMessage("Conf:true");
            }
            else // Joueur 2 refuse les paramètres
            {
                Comm.SendMessage("Conf:false");
            }
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            SocComm TmpSoc = (SocComm)sender;
            if (!TmpSoc.IsConnected)
            {
                MessageBox.Show("La connexion a été perdue !");
                this.Comm = null;
                this.begin = false;
                this.Close();
            }
        }

        private void EcranSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Comm != null)
            {
                Comm.ReceivedMessageChanged -= this.OnMessageReceived;
                Comm.IsConnectedChanged -= this.OnConnectionChanged;
            }
        }
    }
}
