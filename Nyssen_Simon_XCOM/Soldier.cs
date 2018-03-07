using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyssen_Simon_XCOM
{
    class Soldier
    {
        int damage, precision, HP, evasion, mobility;
        bool covered;
        int posX, posY;
        string name;
        Random rand = new Random();

        public Soldier()
        {
            this.name = "N/A";
            this.damage = 0;
            this.precision = 0;
            this.HP = 0;
            this.evasion = 0;
            this.mobility = 0;
            this.covered = false;
            this.posX = 0;
            this.posY = 0;
        }

        public Soldier(string name, int damage, int precision, int HP, int evasion, int mobility, int posX, int posY)
        {
            this.name = name;
            this.damage = damage;
            this.precision = precision;

            this.HP = HP;
            this.evasion = evasion;
            this.mobility = mobility;
            this.covered = false;
            this.posX = posX;
            this.posY = posY;
        }

        public void AfficherStats()
        {
            Console.WriteLine(this.name + " a pour statistiques :");
            Console.WriteLine("Degats = " + this.damage);
            Console.WriteLine("Precision = " + this.precision);
            Console.WriteLine("Points de vie = " + this.HP);
            Console.WriteLine("Evasion = " + this.evasion);
            Console.WriteLine("Mobilite = " + this.mobility);
            Console.WriteLine("Et il se trouve en (" + this.posX + " ; " + this.posY + ").");
        }

        public float DistanceCalc(int e_posX, int e_posY)
        {
            return (float)Math.Sqrt((e_posX - this.posX) ^ 2 + (e_posY - this.posY) ^ 2);
        }

        public void Attack(int e_cover, Soldier target)
        {
            //float chance = (this.precision * 10) - target.Defense(e_cover) - DistanceCalc(target.posX, target.posY);
            Console.WriteLine("debug : distance = " + DistanceCalc(target.posX, target.posY));
            float chance = this.precision * 10 / (DistanceCalc(target.posX, target.posY) * target.Defense(e_cover));
            Console.WriteLine(this.name + " a " + chance + "% de chances de toucher " + target.name + ".");
            int hit = this.rand.Next(0, 101);
            //Console.WriteLine("debug : hit = " + hit);
            if (hit <= chance)
            {
                target.HP -= this.damage;
                Console.WriteLine(this.name + " a inflige " + this.damage + " de degats a " + target.name + " !");
            }
            else
                Console.WriteLine(this.name + " a rate son tir sur " + target.name + ".");
        }

        public void Move(int deplX, int deplY)
        {
            Console.WriteLine("debug : distance = " + DistanceCalc(this.posX + deplX, this.posY + deplY));
            if (DistanceCalc(this.posX + deplX, this.posY + deplY) <= this.mobility)
            {
                this.posX += deplX;
                this.posY += deplY;
                Console.WriteLine(this.name + " se trouve maintenant en (" + this.posX + " ; " + this.posY + ").");
            }
            else
                Console.WriteLine(this.name + " ne peut pas accéder à cette position car elle est hors de portée.");
        }

        public void Cover()
        {
            this.covered = true;
        }

        public float Defense(int cover)
        {
            float defense;
            if (cover == 0)
                defense = (float)(this.evasion * 0.1);
            else if (cover == 1)
                defense = (float)(this.evasion * 1.5);
            else
                defense = this.evasion * 2;

            if (this.covered)
                defense += 10;

            return defense;
        }
    }
}
