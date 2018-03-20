using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyssen_Simon_XCOM
{
    class Soldat
    {
        private int classe = 4; // Définit la classe du soldat (read only): // 0 -> Fantassin
                                                                            // 1 -> Sniper
                                                                            // 2 -> Lourd
                                                                            // 3 -> Léger
                                                                            // 4 -> Indéfini
        private bool covered = false; // Permet de savoir si le soldat a renforcé sa position ce tour-ci ou non (read only)
        public Case_Echiquier position; // Position du soldat définit par une case dans l'échiquier, modifiable par le déplacement du soldat
        private int HP; // Points de vie du soldat, modifiable par une attaque (via accesseur, avec condition > 0)
        private int precision, evasion, mobility; // Stats du soldat
        bool Erreur = false; // Présence d'une erreur

        public Soldat(int _classe, Case_Echiquier _position)
        {
            this.classe = _classe;
            this.position = _position;

            switch (classe)
            {
                case 0: // Fantassin
                        /*
                        * Stats ici
                        */
                    break;
                case 1: // Sniper
                        /*
                         * Stats ici
                         */
                    break;
                case 2: // Lourd
                        /*
                         * Stats ici
                         */
                    break;
                case 3: // Leger
                        /*
                         * Stats ici
                         */
                    break;
                default: // Erreur -> Pas de classe assignée
                    Erreur = true;
                    break;

            }
        }
    }
}
