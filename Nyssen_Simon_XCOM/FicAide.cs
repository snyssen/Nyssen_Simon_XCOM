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
    public partial class EcranAide : Form
    {
        private short BtnIndex;

        public EcranAide()
        {
            InitializeComponent();
            BtnIndex = 0;
            ChangeScreen();
        }

        public void ChangeScreen()
        {
            switch (BtnIndex)
            {
                case 0:
                    pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbImage.Image = Properties.Resources.XCOM_Shield_Logo;
                    lblInfos.Text = "Déroulement d'une partie et but du jeu :\n" +
                        "Les deux joueurs doivent s'entendre sur la taille de leur escouade et les rôles des soldats qui la compose ainsi que sur le choix de la carte avant de commencer la partie.\n" +
                        "La carte choisie servira de champs de bataille, divisée en un échiquier de 100 cases. Les deux joueurs se disputeront alors ce champs de bataille et devront éliminer tous les soldats de l'escouade adverse.\n" +
                        "Le joueur gagnant sera dés lors celui qui arrivera à éliminer en premier tous les soldats de son adversaire.";
                    break;
                case 1:
                    pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbImage.Image = Properties.Resources.Plateau_de_jeu___Edit;
                    lblInfos.Text = "Voici un aperçu du champs de bataille lors d'une partie.";
                    break;
                case 2:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Statistiques_de_soldat;
                    lblInfos.Text = "Survoler l'icône d'un soldat avec votre souris permet de consulter ses statistiques. Dans l'ordre, elles consistent en :\n" +
                        "Points de vie (HP) -> Ce sont les points de vie du soldat. Si ils atteignent zéro, le soldat meurt\n" +
                        "Dégâts -> Ce sont les dégâts que le soldat affligera à une cible si il effectue un tir réussi\n" +
                        "Précision -> Augmente les chances de réussite des tirs que le soldats effectue\n" +
                        "Esquive -> Augmente les chances du soldats d'esquiver un tir qui lui est destiné\n" +
                        "Mobilité -> Définit le nombre de cases sur lesquelles un soldat peut se déplacer sur un tour\n";
                    break;
                case 3:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Actions;
                    lblInfos.Text = "Cliquer sur un soldat allié vous permettra de choisir une action que ce soldat effectuera. Trois actions sont disponibles : \n" +
                        "Déplacer -> Permet de déplacer le soldat vers une case visée, tant que cette case se trouve à sa portée (définit par sa mobilité)\n" +
                        "Tenir la position -> Le soldat renforce sa position actuelle, ce qui lui permet de pouvoir plus facilement esquiver les tirs\n" +
                        "Tirer -> Permet de tirer sur un soldat ennemi. Le tir n'est pas certain d'être réussi; ceci est discuté plus en détail à la page suivante.\n" +
                        "Vous pouvez également décider de ne pas effectuer d'action immédiatement en cliquant sur la croix rouge. Sachez que si vous décidez une action, vous ne pourrez pas revenir en arrière !\n" +
                        "Chaque soldat est aussi obligé d'effectuer une action afin de terminer son tour.";
                    break;
                case 4:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Firing;
                    lblInfos.Text = "La réussite d'un tir dépend de plusieurs facteurs :\n" +
                        "- La précision du soldat (plus il est précis, plus il a de chances de toucher)\n" +
                        "- La distance du soldat par rapport à sa cible (plus il est proche de sa cible, plus il a de chances de toucher)\n" +
                        "- L'évasion du soldat visé (plus le soldat visé a un bon niveau d'évasion, plus il a de chances d'éviter le tir)\n" +
                        "- Si le soldat visé a renforcé sa position ou non (si il l'a renforcée, le tir a moins de chances d'être réussi)\n" +
                        "- Le niveau de couverture de la case sur laquelle le soldat visé se trouve (plus le niveau est élevé, moins le tir a de chances de réussir)\n\n";
                    break;
                case 5:
                    pbImage.SizeMode = PictureBoxSizeMode.Zoom;
                    pbImage.Image = Properties.Resources.Cover;
                    lblInfos.Text = "Il y a trois niveaux de couverture possible, de gauche à droite :\n" +
                        "- Aucune protection\n" +
                        "- Protection moyenne\n" +
                        "- Protection complète\n\n" +
                        "Nous allons maintenant voir les quatres différentes classes de soldats";
                    break;
                case 6:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Fantassin;
                    lblInfos.Text = "Le fantassin :\n" +
                        "Le fantassin est le soldat de base de l'escouade; il dispose de statistiques équilibrées :\n" +
                        "- Dégâts = 5\n" +
                        "- Précision = 5\n" +
                        "- Points de vie = 10\n" +
                        "- Evasion = 4\n" +
                        "- Mobilité = 5 cases";
                    break;
                case 7:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Sniper;
                    lblInfos.Text = "Le sniper :\n" +
                        "Le sniper est un tireur d'élite; Il est très précis et inflige beaucoup de dégâts, mais manque de mobilité, d'évasion et de points de vie :\n" +
                        "- Dégâts = 6\n" +
                        "- Précision = 7\n" +
                        "- Points de vie = 8\n" +
                        "- Evasion = 5\n" +
                        "- Mobilité = 4 cases";
                    break;
                case 8:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Lourd;
                    lblInfos.Text = "Le soldat lourd :\n" +
                        "Le soldat lourd est un soldat équipé d'une armure exosquelette qui lui permet d'absorber de gros dégâts; ainsi, il dispose de beaucoup de points de vie, mais souffre d'une mauvaise précision, évasion et mobilité :\n" +
                        "- Dégâts = 4\n" +
                        "- Précision = 3\n" +
                        "- Points de vie = 15\n" +
                        "- Evasion = 2\n" +
                        "- Mobilité = 3 cases";
                    break;
                case 9:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Properties.Resources.Leger;
                    lblInfos.Text = "Le soldat léger :\n" +
                        "Le soldat léger est un soldat équipé d'une armure légère, ce qui lui permet de se déplacer plus librement; en effet, il dispose d'une mobilité et d'une évasion accrue, mais sacrifie ses dégâts et ses points de vie :\n" +
                        "- Dégâts = 4\n" +
                        "- Précision = 4\n" +
                        "- Points de vie = 5\n" +
                        "- Evasion = 8\n" +
                        "- Mobilité = 8 cases";
                    break;
                case 10:
                    pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbImage.Image = Properties.Resources.XCOM_Shield_Logo;
                    lblInfos.Text = "Vous êtes maintenant prêt à accéder au champs de bataille.\n" +
                        "Bonne chance commandant !";
                    break;
                default:
                    pbImage.Image = null;
                    lblInfos.Text = "";
                    MessageBox.Show("Une erreur est survenue", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            if (BtnIndex <= 0)
                btnPrecedent.Enabled = false;
            else
                btnPrecedent.Enabled = true;
            if (BtnIndex >= 10)
                btnSuivant.Enabled = false;
            else
                btnSuivant.Enabled = true;
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            BtnIndex++;
            ChangeScreen();
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            BtnIndex--;
            ChangeScreen();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbImage_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
