using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyssen_Simon_XCOM
{
    class Soldat
    {
        private int _classe = 4; // Définit la classe du soldat (read only): // 0 -> Fantassin
                                                                             // 1 -> Sniper
                                                                             // 2 -> Lourd
                                                                             // 3 -> Léger
                                                                             // 4 -> Indéfini
        private bool _covered = false; // Permet de savoir si le soldat a renforcé sa position ce tour-ci ou non (read only avec accesseur)
        public Case_Echiquier position; // Position du soldat définit par une case dans l'échiquier, modifiable par le déplacement du soldat
        private int _HP; // Points de vie du soldat, modifiable par une attaque (via accesseur, avec condition > 0)
        private int damage, precision, evasion, mobility; // Stats du soldat
        private bool _alive = true; // Détermine si le soldat est en vie ou non
        public bool error = false; // Présence d'une erreur
        public bool played = false; // Vrai si le soldat a déjà utilisé son action

        public Soldat(int classe, Case_Echiquier _position)
        {
            this._classe = classe;
            this.position = _position;

            switch (_classe)
            {
                case 0: // Fantassin
                    this.damage = 5;
                    this.precision = 5;
                    this._HP = 10;
                    this.evasion = 4;
                    this.mobility = 5;
                    break;
                case 1: // Sniper
                    this.damage = 6;
                    this.precision = 7;
                    this._HP = 8;
                    this.evasion = 5;
                    this.mobility = 4;
                    break;
                case 2: // Lourd
                    this.damage = 4;
                    this.precision = 3;
                    this._HP = 15;
                    this.evasion = 2;
                    this.mobility = 3;
                    break;
                case 3: // Leger
                    this.damage = 4;
                    this.precision = 4;
                    this._HP = 5;
                    this.evasion = 8;
                    this.mobility = 8;
                    break;
                default: // Erreur -> Pas de classe assignée
                    error = true;
                    break;

            }
        }

        #region Accesseurs
        public bool covered
        { get { return this._covered; } }
        public int HP
        {
            get { return this._HP; }
            set { // Reçoit des dégâts, ne pas essayer dy entrer une valeur absolue de points de vie !
                if (this._HP - value > 0)
                    this._HP -= value;
                else
                    this._alive = false;
                }
        }
        public bool alive
        { get { return this._alive; } }
        #endregion

        #region Méthodes
        #region Actions
        public void Attack(Soldat target) // Attaque un soldat ciblé
        {

        }

        public int Move(Case_Echiquier targetPos) // Se déplace vers une case ciblée, renvoie 0 si pas d'erreur, 1 si la case ciblée est trop loin et 2 si la case ciblée est déjà occupée
        {
            if (targetPos.soldier == null)
            {
                if (DistanceCalc(targetPos) <= this.mobility)
                {
                    this.position = targetPos;
                    this.played = true;
                    return 0;
                }
                else
                    return 1;
            }
            else
                return 2;
        }

        public void TakeCover() // Renforce sa position sur sa case actuelle (augmente donc sa défense)
        {
            this._covered = true;
            this.played = true;
        }
        #endregion
        /*
        private float DefenseCalc()
        {

        }
        */

        private float DistanceCalc(Case_Echiquier target) // Calcule la distance entre la case sur laquelle se trouve le soldat et une case visée
        {
            // DEBUG
            float distance = (float)Math.Sqrt(Math.Pow((position.Centre.X - target.Centre.X), 2) + Math.Pow((position.Centre.Y - target.Centre.Y), 2));
            Console.WriteLine("Distance = " + distance);
            return distance;
            //return (float)Math.Sqrt(Math.Pow((position.Centre.X - target.Centre.X), 2) + Math.Pow((position.Centre.Y - target.Centre.Y), 2));
        }
        #endregion
    }
}
