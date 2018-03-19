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
        private int _posX, _posY, _Xmax, _Ymax;
        private Point _Centre;
        //public int Cover = 3; // Déclaré par défaut à 3, représente une valeur indéfinie => provoque une erreur si laissée telle quelle
        public int Cover = 2; // Debug


        public Case_Echiquier(int posX, int posY, int XMax, int YMax)
        {
            this._posX = posX;
            this._posY = posY;
            this._Xmax = XMax;
            this._Ymax = YMax;
            this._Centre = new Point((_Xmax - _posX) / 2, (_Ymax - _posY) / 2);
        }

        public int posX
        { get { return this._posX; } }
        public int posY
        { get { return this._posY; } }
        public int Xmax
        { get { return this._Xmax; } }
        public int Ymax
        { get { return this._Ymax; } }
        public Point Centre
        { get { return this._Centre; } }

        public void DessinerCase(IntPtr handle)
        {
            Graphics gr = Graphics.FromHwnd(handle);

            gr.DrawRectangle(new Pen(Color.AliceBlue, 50),_posX, _posY, _Xmax - _posX, _Ymax - _posY);

            //gr.Dispose();
        }
    }
}
