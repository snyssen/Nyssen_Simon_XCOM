﻿using System;
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

        public EcranSetup(bool _audio)                
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
                    Close();
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
    }
}
