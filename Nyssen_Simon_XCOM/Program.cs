using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyssen_Simon_XCOM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash_Screen());
            EcranAccueil accueil;
            EcranGame game = null;
            do
            {
                accueil = new EcranAccueil();
                Application.Run(accueil);
                if (accueil.GameLaunch) // On veut lancer/reprendre une partie
                {
                    if (accueil.Setup) // On veut commencer une nouvelle partie => Choix des paramètres
                    {
                        game = new EcranGame(accueil.setup.SelectedbtnIndex, accueil.setup.NbrFantassins, accueil.setup.NbrSnipers, accueil.setup.NbrLourds, accueil.setup.NbrLegers);
                        Application.Run(game);
                    }
                    else // On reprend une partie précédente
                    {
                        game = new EcranGame(accueil.SelectedbtnIndex, accueil.Joueur1Joue,
                            accueil.classes_J1, accueil.covered_J1, accueil.HP_J1, accueil.alive_J1, accueil.played_J1, accueil.IndexX_J1, accueil.IndexY_J1,
                            accueil.classes_J2, accueil.covered_J2, accueil.HP_J2, accueil.alive_J2, accueil.played_J2, accueil.IndexX_J2, accueil.IndexY_J2);
                        Application.Run(game);
                    }
                }
            }
            while (accueil.GameLaunch && game.Relaunch && game != null);
        }
    }
}
