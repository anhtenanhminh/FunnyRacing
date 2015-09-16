using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;

namespace _06110086_06110045_CoCaNgua
{
    class MyPieces
    {
        private int _buff = 0;

        public int Buff
        {
            get { return _buff; }
            set { _buff = value; }
        }

        private Point _curPoint;

        public Point curPoint
        {
            get { return _curPoint; }
            set { _curPoint = value; }
        }
        private Image _Img;

        public Image Img
        {
            get { return _Img; }
            set { _Img = value; }
        }
        private bool _Started;

        public bool Started
        {
            get { return _Started; }
            set { _Started = value; }
        }
        private bool _IsChoice;

        public bool IsChoice
        {
            get { return _IsChoice; }
            set { _IsChoice = value; }
        }
        private int _curIndex;

        public int curIndex
        {
            get { return _curIndex; }
            set { _curIndex = value; }
        }
        private bool _isFinish;

        public bool isFinish
        {
            get { return _isFinish; }
            set { _isFinish = value; }
        }
        private int nType;
        private int _startIndex;

        public int StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; }
        }
        private int _endIndex;

        public int EndIndex
        {
            get { return _endIndex; }
            set { _endIndex = value; }
        }
        private bool _ended;

        public bool Ended
        {
            get { return _ended; }
            set { _ended = value; }
        }
        private int _curEnd;

        public int curEnd
        {
            get { return _curEnd; }
            set { _curEnd = value; }
        }
        private Point start;
        private Point end;
        private Point cage;
        public MyPieces(Image img, Point cage, Point str, Point end, int nType, int sindex, int eindex)
        {
            Img = img;
            Started = false;
            _ended = false;
            _isFinish = false;
            this.nType = nType;
            this.start = str;
            this.end = end;
            this.cage = cage;
            _curPoint = cage;
            _curIndex = -1;
            _startIndex = sindex;
            _endIndex = eindex;
            _curEnd = 0;
        }

        public bool UpRandFail(int x, MyBoard board)
        {
            if (x - 1 <= _curEnd && x != 1)
                return true;
            for (int i = x - 1; i > _curEnd; i--)
                if (!board.arrEndSquares[nType - 1].empty[i])
                    return true;
            return false;
        }

        public void UpRand(int x, MyBoard board)
        {
            if (_curEnd == 0)
                board.arrSquares[curIndex].status = 0;
            if (x > _curEnd)
            {
                board.arrEndSquares[nType - 1].empty[_curEnd] = true;
                _curEnd = x - 1;
                board.arrEndSquares[nType - 1].empty[_curEnd] = false;
                _curPoint = board.arrEndSquares[nType - 1].p[_curEnd];
                SoundPlayer sound = new SoundPlayer(@"Resources\Audio\UpRand.wav");
                sound.LoadAsync();
                sound.Play();
                sound.Dispose();
                if (_curEnd >= 2)
                {
                    int dem = 0;
                    for (int i = 5; i > _curEnd; i--)
                    {
                        if (!board.arrEndSquares[nType - 1].empty[i])
                            dem++;
                    }
                    if (dem == (5 - _curEnd))
                        this._isFinish = true;
                }
            }
        }

        public bool emptyStart(MyBoard board)
        {
            if (board.arrSquares[_startIndex].status == this.nType || board.arrSquares[_startIndex].shield == 2)
                return false;
            return true;
        }

        public bool IsStarted(MyBoard board, int x)
        {
            if ((x != 1 && x != 6) || ((x == 1 || x == 6) && !emptyStart(board)))
                return false;
            return true;
        }

        public vicPiece Start(MyBoard board, int index)
        {
            vicPiece vic = new vicPiece();
            vic.index = -1;
            vic.nType = 0;
            //KT bomb
            if (board.arrSquares[_startIndex].bomb != 0 &&
                board.arrSquares[_startIndex].bomb != nType && Buff != 2) //Co bomb k fai cua doi minh
            {
                board.arrSquares[_startIndex].shield = 0;
                board.arrSquares[_startIndex].shadow = 0;
                InCage();
                board.arrSquares[_startIndex].bomb = 0;
                SoundPlayer sound1 = new SoundPlayer(@"Resources\Audio\ExClose.wav");
                sound1.LoadAsync();
                sound1.Play();
                sound1.Dispose();
                Started = false;
                Ended = false;
                return vic;
            }
            if (board.arrSquares[_startIndex].shadow != 1)
            {
                vic.nType = board.arrSquares[_startIndex].status;
                vic.index = board.arrSquares[_startIndex].curIndex;
            }
            _curPoint = start;
            _curIndex = _startIndex;
            board.arrSquares[_curIndex].status = nType;
            board.arrSquares[_curIndex].curIndex = index;
            SoundPlayer sound = new SoundPlayer(@"Resources\Audio\Start.wav");
            sound.LoadAsync();
            sound.Play();
            sound.Dispose();
            return vic;
        }

        public bool moveFail(int x, MyBoard board)
        {
            //Kiem tra diem toi
            if (board.arrSquares[(curIndex + x) % 56].status == this.nType)
                return true;
            for (int i = 1; i < x; i++)
            {
                if ((curIndex + i) % 56 == _endIndex)
                    return true;
            }
            //Diem toi k fai minh va cross
            if (Buff == 6)
                return false;
            //Kiem tra diem toi co shield khong
            if (board.arrSquares[(_curIndex + x) % 56].shield == 2)
                return true;

            //Kiem tra o giua
            for (int i = 1; i < x; i++)
            {
                if ((board.arrSquares[(curIndex + i) % 56].status != 0 && board.arrSquares[(curIndex + i) % 56].shadow != 1))
                    return true;
            }
            return false;
        }

        public vicPiece MoveSteponStep(MyBoard board, int index)
        {
            vicPiece vic = new vicPiece();
            vic.index = -1;
            vic.nType = 0;
            IsChoice = true;

            board.arrSquares[curIndex].status = 0;
            board.arrSquares[curIndex].curIndex = -1;
            board.arrSquares[curIndex].shadow = 0;
            board.arrSquares[curIndex].shield = 0;

            curIndex = (curIndex + 1) % 56;
            if (board.arrSquares[_curIndex].bomb != 0 &&
               board.arrSquares[_curIndex].bomb != nType && Buff != 6 && Buff != 2) //Co bomb k fai cua doi minh
            {
                board.arrSquares[_startIndex].shield = 0;
                board.arrSquares[_startIndex].shadow = 0;
                board.arrSquares[_curIndex].bomb = 0;
                InCage();
                vic.nType = -2;
                SoundPlayer sound1 = new SoundPlayer(@"Resources\Audio\ExClose.wav");
                sound1.LoadAsync();
                sound1.Play();
                Started = false;
                Ended = false;
                sound1.Dispose();
                return vic;
            }
            if (board.arrSquares[_curIndex].shadow != 1 && board.arrSquares[_curIndex].status != nType)
            {
                vic.nType = board.arrSquares[_curIndex].status;
                vic.index = board.arrSquares[_curIndex].curIndex;
            }
            if (board.arrSquares[curIndex].status == nType) // nguoi doi minh
                curIndex = (curIndex + 1) % 56;
            _curPoint = board.arrSquares[curIndex].square;
            board.arrSquares[_curIndex].status = nType;
            board.arrSquares[_curIndex].curIndex = index;
            if (curIndex == _endIndex)
                _ended = true;
            return vic;
        }

        public void InCage()
        {
            _curPoint = cage;
            curIndex = -1;
        }

        public bool isChoice(int X, int Y)
        {
            if (_curPoint.X <= X && X <= _curPoint.X + 25 && _curPoint.Y <= Y && Y <= _curPoint.Y + 30)
                return true;
            return false;
        }

        public void clearBuff(MyBoard board)
        {
            Buff = 0;
            if (Started && !Ended)
            {
                board.arrSquares[curIndex].shield = 0;
                board.arrSquares[curIndex].shadow = 0;
            }
        }

        public void Draw(Graphics g, MyBoard board, int index)
        {
            Bitmap bmp = new Bitmap(12, 12);
            Bitmap bmp1 = new Bitmap(24, 24);
            /*if (Started && !Ended)
            {
                board.arrSquares[_curIndex% 56].status = nType;
                board.arrSquares[_curIndex% 56].curIndex = index;
            }*/
            if (Buff != 0 && Buff != 3 && Buff != 4) // Co buff
            {
                switch (Buff)
                {
                    case 1:
                        {
                            bmp = new Bitmap(@"Resources\ImageIcon\hide.PNG");
                            if (Started && !Ended)
                                board.arrSquares[_curIndex % 56].shadow = this.Buff;
                        } break;
                    case 2:
                        {
                            bmp = new Bitmap(@"Resources\ImageIcon\shield.PNG");
                            if (Started && !Ended)
                                board.arrSquares[_curIndex % 56].shield = this.Buff;
                        } break;
                    case 5: bmp = new Bitmap(@"Resources\ImageIcon\speedup.PNG"); break;
                    case 6: bmp = new Bitmap(@"Resources\ImageIcon\cross.PNG"); break;
                }
                g.DrawImage(bmp, _curPoint.X + 18, _curPoint.Y, 20, 20);
            }
            if (this.IsChoice)
            {
                bmp1 = new Bitmap(RacingHorses.imgarr.Images[nType - 1]);
                g.DrawImage(bmp1, _curPoint.X, _curPoint.Y - 20, 20, 20);
            }
            bmp.Dispose();
            bmp1.Dispose();
            g.DrawImage(Img, _curPoint.X, _curPoint.Y, 20, 30);

        }

      
    }
}
