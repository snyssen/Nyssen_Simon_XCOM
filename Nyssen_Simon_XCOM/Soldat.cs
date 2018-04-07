using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyssen_Simon_XCOM
{
    class Soldat
    {
        private int _classe = 4; // Définit la classe du soldat (read only avec accesseur): 
                                 // 0 -> Fantassin
                                 // 1 -> Sniper
                                 // 2 -> Lourd
                                 // 3 -> Léger
                                 // 4 -> Indéfini
        private bool _covered = false; // Permet de savoir si le soldat a renforcé sa position ce tour-ci ou non (avec accesseur)
        public Case_Echiquier position; // Position du soldat définit par une case dans l'échiquier, modifiable par le déplacement du soldat
        private int _HP; // Points de vie du soldat, modifiable par une attaque (via accesseur, avec condition > 0)
        private int _damage, _precision, _evasion, _mobility; // Stats du soldat
        private bool _alive = true; // Détermine si le soldat est en vie ou non
        public bool error = false; // Présence d'une erreur
        public bool played = false; // Vrai si le soldat a déjà utilisé son action
        Random tir = new Random(); // Permet de tirer des nombres entre 0 et 100 pour déterminer si un tir touche ou non

        public Soldat(int classe, Case_Echiquier _position)
        {
            this._classe = classe;
            this.position = _position;

            switch (_classe)
            {
                case 0: // Fantassin
                    this._damage = 5;
                    this._precision = 5;
                    this._HP = 10;
                    this._evasion = 4;
                    this._mobility = 5;
                    break;
                case 1: // Sniper
                    this._damage = 6;
                    this._precision = 7;
                    this._HP = 8;
                    this._evasion = 5;
                    this._mobility = 4;
                    break;
                case 2: // Lourd
                    this._damage = 4;
                    this._precision = 3;
                    this._HP = 15;
                    this._evasion = 2;
                    this._mobility = 3;
                    break;
                case 3: // Leger
                    this._damage = 4;
                    this._precision = 4;
                    this._HP = 5;
                    this._evasion = 8;
                    this._mobility = 8;
                    break;
                default: // Erreur -> Pas de classe assignée
                    error = true;
                    break;
            }
        }
        /*
        public Soldat(int classe, )
        {

        }
        */

        #region Accesseurs
        public bool covered
        {
            get { return this._covered; }
            set { this._covered = value; }        
        }
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
        public int damage
        { get { return this._damage; } }
        public int classe
        { get { return this._classe; } }
        #endregion

        #region Méthodes
        #region Actions
        public int Attack(Soldat target) // Attaque un soldat ciblé
        {
            if (target.DefenseCalc() < 0)
                return (int)target.DefenseCalc();
            float chance = (this._precision / (DistanceCalc(target.position) * target.DefenseCalc())) * 1000;
            // DEBUG
            //float chance = 100;
            Console.WriteLine("chance de toucher : " + chance);
            if (this.tir.Next(0, 101) <= chance)
            {
                target.HP = this._damage;
                this.played = true;
                return 0;
            }
            else
            {
                this.played = true;
                return 1;
            }
        }

        public int Move(Case_Echiquier targetPos) // Se déplace vers une case ciblée, renvoie 0 si pas d'erreur, 1 si la case ciblée est trop loin et 2 si la case ciblée est déjà occupée
        {
            if (targetPos.soldier == null) // On vérifie que la case sélectionnée est vide
            {
                if (DistanceCalc(targetPos) <= this._mobility) // On vérifie que le soldat a assez de mobilité pour atteindre la case
                {
                    this.position.soldier = null;
                    this.position = targetPos;
                    targetPos.soldier = this;
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
        
        private float DefenseCalc() // Calcul la "défense" du soldat, càd si il a plus ou moins de chance d'esquiver un tir
        {
            float defense;
            switch (this.position.Cover)
            {
                case 0:
                    defense = (float)(this._evasion * 0.1);
                    break;
                case 1:
                    defense = (float)(this._evasion * 1.5);
                    break;
                case 2:
                    defense = this._evasion * 2;
                    break;
                default:
                    defense = -1;
                    break;
            }
            if (this.covered)
                defense += 10;
            return defense;
        }
        

        private int DistanceCalc(Case_Echiquier target) // Calcule la distance entre la case sur laquelle se trouve le soldat et une case visée (en nombre de cases)
        {
            int distance = Math.Abs(this.position.IndexX - target.IndexX) + Math.Abs(this.position.IndexY - target.IndexY);
            // DEBUG
            Console.WriteLine("Distance = " + distance);
            return distance;
        }

        public string AfficherStats()
        {
            string stats = "Statistiques :" +
                           "\n\t- Points de vie = " + this._HP +
                           "\n\t- Dégâts = " + this._damage +
                           "\n\t- Précision = " + this._precision +
                           "\n\t- Esquive = " + this._evasion +
                           "\n\t- Mobilité = " + this._mobility + " cases";
            stats += (covered) ? "\nCe soldat a renforcé sa position et dispose d'un bonus à l'esquive." : "";
            stats += (played) ? "\nCe soldat a déjà joué." : "\nCe soldat doit encore jouer.";
            return stats;
        }
        #endregion
    }
}
