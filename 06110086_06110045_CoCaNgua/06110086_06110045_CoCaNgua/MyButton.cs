using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _06110086_06110045_CoCaNgua
{
    class MyButton
    {
        private Bitmap[] _Bmp;
        private int _nBmp, _iBmp;
        public Bitmap[] Bmp
        {
            get{return _Bmp;}
            set
            {
                _Bmp = value;
                _nBmp = _Bmp.Length;
                _iBmp = 0;
            }
        }
        public int IBmp
        {
            get { return _iBmp; }
            set { _iBmp = value; }
        }
        private int _Top, _Left, _Width, _Height;
        public int Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        private bool _IsHover;
        public bool IsHover
        {
            get { return _IsHover; }
            set { _IsHover = value; }
        }
        private bool _IsClick;

        public bool IsClick
        {
            get { return _IsClick; }
            set { _IsClick = value; }
        }
        public MyButton(Bitmap[] _bmp, int _left, int _top, int _width, int _height)
        {
            Bmp = _bmp;
            Top = _top;
            Left = _left;
            Width = _width;
            Height = _height;
        }
        public void Draw(Graphics g)
        {
            if (this.IsHover)
                g.DrawImage(_Bmp[_iBmp], _Left - 10, _Top - 10, _Width + 20, _Height + 20);
            else
                g.DrawImage(_Bmp[_iBmp], _Left, _Top, _Width, _Height);
        }
        public bool ClickorHover(int x, int y)
        {
            if (_Left <= x && x <= _Left + _Width && _Top <= y && y <= _Top + _Height)
                return true;
            return false;
        }
    }
}
