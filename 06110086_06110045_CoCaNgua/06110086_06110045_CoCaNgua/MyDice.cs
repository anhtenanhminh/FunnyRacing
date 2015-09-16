using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _06110086_06110045_CoCaNgua
{
    class MyDice
    {
        const int x = 355;
        const int y = 505;
        private int _curScore;

        public int curScore
        {
            get { return _curScore; }
            set { _curScore = value; }
        }
        private int[] _Score;
        private Image[] _Img;

        public Image[] Img
        {
            get { return _Img; }
            set { _Img = value; }
        }
        private Image _curImg;

        public Image curImg
        {
            get { return _curImg; }
            set { _curImg = value; }
        }
        public MyDice()
        {
            _Score = new int[6];
            _Img = new Image[6];
            for (int i = 0; i < 6; i++)
            {
                _Score[i] = i + 1;
                _Img[i] = new Bitmap(@"Resources\ImageDice\" + (i + 1).ToString() + ".png");
            }
            curImg = Img[0];
            curScore = 1;
        }
        public int Shake()
        {
            Random ran = new Random(unchecked((int)DateTime.Now.Millisecond));
            curScore = ran.Next(1, 7);
            curImg = Img[curScore - 1];
            return curScore;
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(curImg, x, y, 87, 77);
        }
    }
}
