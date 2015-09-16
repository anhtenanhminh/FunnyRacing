using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;

namespace _06110086_06110045_CoCaNgua
{
    struct Square
    {
        public Point square;
        public int status;
        public int curIndex;
        public int bomb;
        public int shield;
        public int shadow;
    }
    struct EndSquare
    {
        public Point[] p;
        public bool[] empty;
    }
    class MyBoard
    {

        public bool props = true;
        private Square[] _arrSquares; // Vung di chuyen

        public Square[] arrSquares
        {
            get { return _arrSquares; }
            set { _arrSquares = value; }
        }

        private EndSquare[] _arrEndSquares;

        internal EndSquare[] arrEndSquares
        {
            get { return _arrEndSquares; }
            set { _arrEndSquares = value; }
        }

        public MyBoard()
        {
            XmlDocument document = new XmlDocument();
            XmlNodeList nodelist;
            XmlNode node;
            int x, y;
            document.Load(@"Resources\Point.xml");
            nodelist = document.SelectNodes("/POINTS/SQUAREPOINTS/POINT");
            //Khoi tao vung di chuyen
            _arrSquares = new Square[56];
            for (int i = 0; i < 56; i++)
            {
                _arrSquares[i].status = 0;
                _arrSquares[i].curIndex = -1;
                _arrSquares[i].bomb = 0;
                _arrSquares[i].shadow = 0;
                _arrSquares[i].shield = 0;
                node = nodelist[i];
                x = Convert.ToInt32(node.Attributes[0].Value);
                y = Convert.ToInt32(node.Attributes[1].Value);
                _arrSquares[i].square = new Point(x, y);
            }

            nodelist = document.SelectNodes("/POINTS/ENDPOINTS/POINT");
            //Khoi tao vung dich den
            _arrEndSquares = new EndSquare[4];
            int stt = 0;
            for (int i = 0; i < 4; i++)
            {
                _arrEndSquares[i] = new EndSquare();
                _arrEndSquares[i].p = new Point[6];
                _arrEndSquares[i].empty = new bool[6];
                for (int j = 0; j < 6; j++)
                {
                    _arrEndSquares[i].empty[j] = true;
                    node = nodelist[stt];
                    x = Convert.ToInt32(node.Attributes[0].Value);
                    y = Convert.ToInt32(node.Attributes[1].Value);
                    _arrEndSquares[i].p[j] = new Point(x, y);
                    stt++;
                }
            }

        }
    }
}
