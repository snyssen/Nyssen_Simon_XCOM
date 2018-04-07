﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nyssen_Simon_XCOM
{
    public partial class EcranAccueil : Form
    {
        public Boolean GameLaunch = false; // Si true, on veut lancer une partie (peu importe que ce soit une nouvelle ou une ancienne); sinon on veut quitter le programme
        public Boolean Setup = false;      // Si true, on veut lancer une NOUVELLE partie; sinon on veut reprendre une en cours (dépendant dans les deux cas de GameLaunch)
        public EcranSetup setup;

        public bool Joueur1Joue;
        public int SelectedbtnIndex;

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

        public EcranAccueil()
        {
            InitializeComponent();
            dlgLoadGame.Filter = "Fichier de sauvegarde|*.sav|Tous fichiers|*.*";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            GameLaunch = false;
            Setup = false;
            Close();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            setup = new EcranSetup();
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
                    //(tab[0] == "True") ? (Joueur1Joue = true) : (Joueur1Joue = false);
                    if (tab[0] == "True")
                        Joueur1Joue = true;
                    else
                        Joueur1Joue = false;
                    SelectedbtnIndex = int.Parse(tab[1]);
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
                // DEBUG
                Console.WriteLine("Donnees generales :\n\tJoueur1Jour : " + Joueur1Joue + "\n\tSelectedbtnIndex : " + SelectedbtnIndex);
                Console.WriteLine("\nSoldats du joueur 1 : ");
                for (int i = 0; i < classes_J1.Count; i++)
                    Console.WriteLine("Soldat " + i + " : \n\tclasse : " + classes_J1[i] + "\n\tcovered : " + covered_J1[i] + "\n\tHP : " + HP_J1[i] + "\n\talive : " + alive_J1[i] + "\n\tplayed : " + played_J1[i] + "\n\tposition : " + IndexX_J1[i] + "," + IndexY_J1[i]);
                Console.WriteLine("\nSoldats du joueur 2 : ");
                for (int i = 0; i < classes_J1.Count; i++)
                    Console.WriteLine("Soldat " + i + " : \n\tclasse : " + classes_J2[i] + "\n\tcovered : " + covered_J2[i] + "\n\tHP : " + HP_J2[i] + "\n\talive : " + alive_J2[i] + "\n\tplayed : " + played_J2[i] + "\n\tposition : " + IndexX_J2[i] + "," + IndexY_J2[i]);

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
    }
}
