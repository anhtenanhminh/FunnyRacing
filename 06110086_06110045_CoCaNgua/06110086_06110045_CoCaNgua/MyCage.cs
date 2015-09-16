using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;


namespace _06110086_06110045_CoCaNgua
{
    class MyCage
    {
        private bool _getTurn = false;
        private bool _speedUp = false;
        private bool _cross = false;
        private int priP = -2; //Uu tien cho ngua priP
        private bool _shield = false;
        private bool _hide = false;
        private bool _useprop = false;

        public int PriP
        {
            get { return priP; }
            set { priP = value; }
        }

        public bool Hide
        {
            get { return _hide; }
            set { _hide = value; }
        }

        public bool Useprop
        {
            get { return _useprop; }
            set { _useprop = value; }
        }

        public bool Shield
        {
            get { return _shield; }
            set { _shield = value; }
        }

        public bool Cross
        {
            get { return _cross; }
            set { _cross = value; }
        }

        public bool SpeedUp
        {
            get { return _speedUp; }
            set { _speedUp = value; }
        }

        public bool GetTurn
        {
            get { return _getTurn; }
            set { _getTurn = value; }
        }

        private int _nType;

        public int nType
        {
            get { return _nType; }
            set { _nType = value; }
        }
        private int _nPieces;

        public int nPieces
        {
            get { return _nPieces; }
            set { _nPieces = value; }
        }
        private MyPieces[] _Pieces;

        public MyPieces[] Pieces
        {
            get { return _Pieces; }
            set { _Pieces = value; }
        }
        private MyBoard board;
        private bool _isPlayer;

        public bool isPlayer
        {
            get { return _isPlayer; }
            set { _isPlayer = value; }
        }
        Image img;
        Point str;
        Point end;
        Point cage;
        public MyCage(int type, int number, MyBoard board, bool player)
        {
            int dem = 0;
            nType = type;
            nPieces = number;
            this.board = board;
            this.isPlayer = player;
            _Pieces = new MyPieces[nPieces];
            switch (nType)
            {
                case 1:
                    img = new Bitmap(@"Resources\ImagePieces\horse_blue.png");
                    str = board.arrSquares[0].square;
                    end = board.arrSquares[55].square;
                    cage = new Point(85, 180);
                    for (int i = 0; i < nPieces; i++)
                    {
                        Pieces[i] = new MyPieces(img, cage, str, end, nType, 0, 55);
                        cage.X += 50;
                        dem++;
                        if (dem == 2)
                        {
                            cage.X -= 100;
                            cage.Y += 50;
                        }
                    }
                    break;
                case 2:
                    dem = 0;
                    img = new Bitmap(@"Resources\ImagePieces\horse_red.png");
                    str = board.arrSquares[14].square;
                    end = board.arrSquares[13].square;
                    cage = new Point(365, 360);
                    for (int i = 0; i < nPieces; i++)
                    {
                        Pieces[i] = new MyPieces(img, cage, str, end, nType, 14, 13);
                        cage.X += 50;
                        dem++;
                        if (dem == 2)
                        {
                            cage.X -= 100;
                            cage.Y += 50;
                        }
                    }
                    break;
                case 3:
                    dem = 0;
                    img = new Bitmap(@"Resources\ImagePieces\horse_green.png");
                    str = board.arrSquares[28].square;
                    end = board.arrSquares[27].square;
                    cage = new Point(640, 180);
                    for (int i = 0; i < nPieces; i++)
                    {
                        Pieces[i] = new MyPieces(img, cage, str, end, nType, 28, 27);
                        cage.X += 50;
                        dem++;
                        if (dem == 2)
                        {
                            cage.X -= 100;
                            cage.Y += 50;
                        }
                    }
                    break;
                case 4:
                    dem = 0;
                    img = new Bitmap(@"Resources\ImagePieces\horse_yellow.png");
                    str = board.arrSquares[42].square;
                    end = board.arrSquares[41].square;
                    cage = new Point(360, 20);
                    for (int i = 0; i < nPieces; i++)
                    {
                        Pieces[i] = new MyPieces(img, cage, str, end, nType, 42, 41);
                        cage.X += 50;
                        dem++;
                        if (dem == 2)
                        {
                            cage.X -= 100;
                            cage.Y += 50;
                        }
                    }
                    break;

            }
        }

        public bool CanAction(int x)
        {
            int dem_1 = 0;
            int dem_2 = 0;
            for (int i = 0; i < nPieces; i++)
            {
                if (!Pieces[i].isFinish)
                {
                    if (!Pieces[i].Started)
                    {
                        if (!Pieces[i].IsStarted(board, x))
                            dem_1++;
                    }
                    else
                    {
                        if (!Pieces[i].Ended)
                        {
                            if (Pieces[i].moveFail(x, board))
                                dem_1++;
                        }
                        else
                            if (Pieces[i].UpRandFail(x, board))
                                dem_1++;
                    }
                }
                else
                    dem_2++;
            }
            if (dem_1 + dem_2 == 4)
                return false;
            return true;
        }

        public vicPiece Action(int x, int i)
        {
            vicPiece vic = new vicPiece();
            vic.index = -1;
            vic.nType = 0;
            if (Pieces[i].Ended && !Pieces[i].UpRandFail(x, board))
            {
                Pieces[i].UpRand(x, board);
            }
            if ((x == 1 || x == 6) && Pieces[i].IsChoice && Pieces[i].emptyStart(board) && !Pieces[i].Started)
            {
                Pieces[i].Started = true;
                vic = Pieces[i].Start(board, i);
                return vic;
            }
            return vic;
        }

        public vicPiece Action_AI(int x, int priP)
        {
            vicPiece vic = new vicPiece();
            vic.index = -1;
            vic.nType = 0;
            if (x == 1)
            {
                for (int k = 0; k < nPieces; k++)
                {
                    if (Pieces[k].emptyStart(board) && !Pieces[k].Started)
                    {
                        Pieces[k].Started = true;
                        Pieces[k].IsChoice = true;
                        vic = Pieces[k].Start(board, k);
                        return vic;
                    }
                }
            }
            for (int k = 0; k < nPieces; k++)
            {
                if (priP != -1 && priP != -2 && priP != 0)
                    k = priP;
                if (!Pieces[k].isFinish)
                {
                    if (Pieces[k].Ended && !Pieces[k].UpRandFail(x, board))
                    {
                        Pieces[k].IsChoice = true;
                        Pieces[k].UpRand(x, board);
                        return vic;
                    }
                    if (x == 6 && Pieces[k].emptyStart(board) && !Pieces[k].Started)
                    {
                        Pieces[k].Started = true;
                        Pieces[k].IsChoice = true;
                        vic = Pieces[k].Start(board, k);
                        return vic;
                    }
                }
            }
            return vic;
        }

        public void Update(int index)
        {
            Pieces[index].InCage();
            Pieces[index].Started = false;
            Pieces[index].Ended = false;
        }

        public bool IsAction(int i, int x)
        {
            if (x != 1 && x != 6 && !Pieces[i].Started)
                return false;
            if (Pieces[i].Ended && Pieces[i].UpRandFail(x, board))
                return false;
            if (Pieces[i].Started && !Pieces[i].Ended && Pieces[i].moveFail(x, board))
                return false;
            if ((x == 1 || x == 6) && !Pieces[i].emptyStart(board) && !Pieces[i].Started)
                return false;
            return true;
        }

        public bool Win()
        {
            int count = 0;
            for (int i = 2; i < 6; i++)
            {
                if (!board.arrEndSquares[nType - 1].empty[i])
                    count++;
            }
            if (count == 4)
                return true;
            return false;
        }

        public void clearbuff(MyBoard board)
        {
            for (int i = 0; i < nPieces; i++)
                Pieces[i].clearBuff(board);
        }

        public int CheckPieceAI(int x)
        {
            if (x == 1)
                for (int k = 0; k < nPieces; k++)
                    if (Pieces[k].emptyStart(board) && !Pieces[k].Started)
                        return k;
            for (int k = 0; k < nPieces; k++)
            {
                if (!Pieces[k].isFinish)
                {
                    if (Pieces[k].Ended && !Pieces[k].UpRandFail(x, board))
                    {
                        return k + 20;
                    }
                    if (Pieces[k].Started && !Pieces[k].Ended && !Pieces[k].moveFail(x, board))
                    {
                        return k + 10;
                    }
                    if (x == 6 && Pieces[k].emptyStart(board) && !Pieces[k].Started)
                    {
                        return k;
                    }
                }
            }
            return -1;

        }

        public void Draw(Graphics g, MyBoard board)
        {
            for (int i = 0; i < nPieces; i++)
                Pieces[i].Draw(g, board, i);
        }

        //-------------------------------------HET------------------------------------------------
    }
}
