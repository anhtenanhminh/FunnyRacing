using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _06110086_06110045_CoCaNgua
{
    class MyProps
    {
        //Cac dao cu chua su dung
        private bool _speedup;

        public bool SpeedUp
        {
            get { return _speedup; }
            set { _speedup = value; }
        }
        private bool _cross;

        public bool Cross
        {
            get { return _cross; }
            set { _cross = value; }
        }
        private bool _getTurn;

        public bool GetTurn
        {
            get { return _getTurn; }
            set { _getTurn = value; }
        }
        private bool _bomb;

        public bool Bomb
        {
            get { return _bomb; }
            set { _bomb = value; }
        }
        private bool _shield;

        public bool Shield
        {
            get { return _shield; }
            set { _shield = value; }
        }
        private bool _hide;

        public bool Hide
        {
            get { return _hide; }
            set { _hide = value; }
        }

        public MyProps()
        {
            SpeedUp = true;
            Hide = true;
            Bomb = true;
            GetTurn = true;
            Cross = true;
            Shield = true;
        }

        public void UseProps(int propsindex, MyCage cage, MyBoard board, int nType)
        {
            if (board.props)
            {
                switch (propsindex)
                {
                    case 1:
                        {
                            Hide = false;
                            cage.Hide= true;
                        } break;
                    case 2:
                        {
                            Shield = false;
                            cage.Shield = true;
                        } break;
                    case 3:
                        {
                            GetTurn = false;
                            cage.Useprop = true;
                            cage.GetTurn = true;
                        } break;
                    case 4:
                        {
                            Bomb = false;
                            for (int i = 0; i < cage.nPieces; i++)
                            {
                                if (cage.Pieces[i].Started && cage.Pieces[i].curEnd == 0) //Da ra khoi chuong va chua len bac
                                {
                                    int index = cage.Pieces[i].curIndex;
                                    board.arrSquares[index].bomb = nType; //bomb do doi nType dat
                                    RacingHorses.bombarr.Add(index);
                                }
                            }
                        } break;
                    case 5:
                        {
                            SpeedUp = false;
                            cage.Useprop = true;
                            cage.SpeedUp = true;
                        } break;
                    case 6:
                        {
                            Cross = false;
                            cage.Useprop = true;
                            cage.Cross = true;
                        } break;
                }
                if (propsindex == 1 || propsindex == 2 || propsindex == 5 || propsindex == 6)
                    for (int i = 0; i < cage.nPieces; i++)
                    {
                        cage.Pieces[i].Buff = propsindex;
                    }
            }
        }

        public bool[] getState()
        {
            bool[] state = new bool[6];
            state[0] = Hide;
            state[1] = Shield;
            state[2] = GetTurn;
            state[3] = Bomb;
            state[4] = SpeedUp;
            state[5] = Cross;
            return state;
        }
    }
}
