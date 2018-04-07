using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Nyssen_Simon_XCOM
{
    class Case_Echiquier
    {
        private int _posX, _posY, _Xmax, _Ymax, _IndexX, _IndexY;
        public int Cover = 3; // Déclaré par défaut à 3, représente une valeur indéfinie => provoque une erreur si laissée telle quelle
                              // 0 = aucune
                              // 1 = moyenne
                              // 2 = élevée
        public Soldat soldier = null; 

        public Case_Echiquier(int posX, int posY, int XMax, int YMax, int IndexX, int IndexY)
        {
            this._posX = posX;
            this._posY = posY;
            this._Xmax = XMax;
            this._Ymax = YMax;
            this._IndexX = IndexX;
            this._IndexY = IndexY;
            this.Cover = 3;
        }

        public Case_Echiquier(int posX, int posY, int XMax, int YMax, int IndexX, int IndexY, int cover)
        {
            this._posX = posX;
            this._posY = posY;
            this._Xmax = XMax;
            this._Ymax = YMax;
            this._IndexX = IndexX;
            this._IndexY = IndexY;
            this.Cover = cover;
        }

        public int posX
        { get { return this._posX; } }
        public int posY
        { get { return this._posY; } }
        public int Xmax
        { get { return this._Xmax; } }
        public int Ymax
        { get { return this._Ymax; } }
        public int IndexX
        { get { return this._IndexX; } }
        public int IndexY
        { get { return this._IndexY; } }
        public Point Centre
        { get { return new Point(_Xmax - ((_Xmax - _posX) / 2), _Ymax - ((_Ymax - _posY) / 2)); } }
        public Point Origin
        { get { return new Point(this._posX, this._posY); } }

        public void DessinerCase(IntPtr handle)
        {
            Graphics gr = Graphics.FromHwnd(handle);

            gr.DrawRectangle(new Pen(Color.AliceBlue, 3),_posX, _posY, _Xmax - _posX, _Ymax - _posY);

            //gr.Dispose();
        }
    }
}
