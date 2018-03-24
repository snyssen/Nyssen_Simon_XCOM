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
            EcranAccueil accueil = new EcranAccueil();
            Application.Run(accueil);

            if (accueil.GameLaunch)
            {
                EcranGame game = new EcranGame(accueil.setup.SelectedbtnIndex, accueil.setup.NbrFantassins, accueil.setup.NbrSnipers, accueil.setup.NbrLourds, accueil.setup.NbrLegers);
                Application.Run(game);
            }

            /*
            if (accueil.GameLaunch) // On veut lancer/reprendre une partie
            {
                if (accueil.Setup) // On veut commencer une nouvelle partie => Choix des paramètres
                {
                    EcranSetup setup = new EcranSetup();
                    Application.Run(setup);
                    if (setup.begin)
                    {
                        MessageBox.Show("Fin de la version d'évaluation !");
                    }
                }
                //else // On reprend une partie précédente
            }
            */
        }
    }
}
