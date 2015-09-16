using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace _06110086_06110045_CoCaNgua
{
    public struct vicPiece
    {
        public int index;
        public int nType;
    }
    public partial class RacingHorses : Form
    {


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        private Menu frmMenu;
        public bool hasProp = true;
        public bool useProp = false;
        private bool win = false;
        bool isClick = false;
        bool first = false;

        private MyProps props = new MyProps();
        private MyProps[] AiProps = new MyProps[4];
        private Button[] btnBuff = new Button[6];
        private MyDice dice;
        private vicPiece vic;
        private MyCage[] cage;
        private MyBoard board;

        public static ImageList imgarr;
        public static List<int> bombarr = new List<int>();
        public List<int> teamchoose = new List<int>();

        private int nCage = 4;
        private int turn;
        private int nextturn;
        int go = 0;
        Thread thread_AI;

        public RacingHorses(Menu frm)
        {
            InitializeComponent();
            frmMenu = frm;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        private void RacingHorses_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                AiProps[i] = new MyProps();
            }

            pShake.Image = imglstShake.Images[0];
            pShake.Visible = false;
            imgarr = imglstArrow;
            pString.Visible = false;
            btnBuff[0] = btnHide;
            btnBuff[1] = btnShield;
            btnBuff[2] = btnGetTurn;
            btnBuff[3] = btnBomb;
            btnBuff[4] = btnSpeedup;
            btnBuff[5] = btnCross;
            cage = new MyCage[nCage];
            board = new MyBoard();
            for (int i = 0; i < 4; i++)
            {
                bool b = false;
                for (int t = 0; t < teamchoose.Count; t++)
                {
                    if (i == teamchoose[t])
                        b = true;
                }
                cage[i] = new MyCage(i + 1, 4, board, b);
            }
            dice = new MyDice();
            turn = 0;
            nextturn = 0;
            vic = new vicPiece();
            vic.index = -1;
            vic.nType = 0;
            if (!hasProp)
                board.props = false;
            btnShake.Visible = false;
            if (cage[0].isPlayer)
            {
                btnShake.Visible = true;
            }
            else
            {
                pShake.Visible = true;
                thread_AI = new Thread(Thread_AI);
                thread_AI.Start();
            }


        }
        private void RacingHorses_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread_AI != null)
                thread_AI.Abort();
            
            frmMenu.Show();
            this.Hide();
        }

        private void RacingHorses_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap bmp = new Bitmap(@"Resources\background.png");

            Graphics gTemp = Graphics.FromImage(bmp);
            dice.Draw(gTemp);
            if (vic.index != -1 && vic.nType != 0)
            {
                cage[vic.nType - 1].Update(vic.index);
                SoundPlayer snd = new SoundPlayer(@"Resources\Audio\kick.wav");
                snd.LoadAsync();
                snd.Play();
                vic.index = -1;
                snd.Dispose();
                vic.nType = 0;
            }
            for (int i = 0; i < nCage; i++)
                cage[i].Draw(gTemp, board);
            Bitmap bmpShield = new Bitmap(@"Resources\ImageIcon\shield.png");
            for (int i = 0; i < 55; i++)
                if (board.arrSquares[i].shadow != 0)
                {
                    Point pSquares = board.arrSquares[i].square;
                    gTemp.DrawImage(bmpShield, pSquares.X, pSquares.Y - 10, 20, 20);
                }
            if (bombarr.Count > 0)
            {
                for (int i = 0; i < bombarr.Count; i++)
                {
                    Point pBombarr = board.arrSquares[i].square;
                    int team = board.arrSquares[i].bomb;
                    if (team != 0)
                        gTemp.DrawImage(imglstBomb.Images[team - 1], pBombarr.X, pBombarr.Y + 10);

                }
            }
            g.DrawImage(bmp, 0, 0, 800, 600);
            gTemp.Dispose();
            bmp.Dispose();
            bmpShield.Dispose();
            win = cage[turn].Win();
            if (win)
            {
                btnBomb.Enabled = true;
                btnCross.Enabled = true;
                btnGetTurn.Enabled = true;
                btnHide.Enabled = true;
                btnShield.Enabled = true;
                btnSpeedup.Enabled = true;
                pShake.Enabled = true;
                pString.Enabled = true;
            }
        }

        //++++++++++++++++++++++++++++++++Kiểm tra prop++++++++++++++++++++++++++++++++++++++++++++++//
        private int isCrossUsed()
        {
            if (AiProps[turn].Cross && cage[turn].Useprop)
            {
                MyCage cg = cage[turn];
                int numc, nume, use, t1, t2;
                numc = nume = use = t1 = t2 = 0;
                for (int i = 0; i < cg.nPieces; i++)
                {
                    MyPieces p = cg.Pieces[i];
                    if (p.Started && !p.Ended && board.arrSquares[(p.curIndex + go) % 56].status != turn + 1)
                    {
                        int numvic = 0, goodvic = 0;
                        int avic = 0;
                        for (int j = -4; j <= 12; j++)
                        {
                            int s = (p.curIndex + j < 0) ? (p.curIndex + 56) : p.curIndex;
                            int team = board.arrSquares[(s + j) % 56].status;
                            int index = board.arrSquares[(s + j) % 56].curIndex;
                            // có địch trong (cur + go) và đang ở xa chuồng
                            if (team != turn + 1 && j < go && j >= 1 && team != 0)
                            {
                                numvic++;
                                use++;
                                t1 = 1;
                                MyPieces p1 = cage[team - 1].Pieces[index];
                                if (((p1.curIndex % 56 < p1.StartIndex) ? Math.Abs(p1.StartIndex - p1.curIndex) <= 20 : Math.Abs(p1.StartIndex - p1.curIndex) >= 35))
                                {
                                    use++;
                                    t2 = i;
                                    goodvic++;
                                }
                                continue;
                            }

                            if (team != turn + 1 && team != 0 && ((j > go && j <= 12) || (j < 0)))
                                avic++;
                        }
                        if (numvic >= 3 && avic == 0)
                        {
                            AiProps[turn].UseProps(6, cage[turn], board, turn + 1);
                            cage[turn].PriP = 1; // ưu tiên cho ngựa i
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                        if (goodvic >= 2 && avic <= 1)//đá 2 con
                        {
                            AiProps[turn].UseProps(6, cage[turn], board, turn + 1);
                            cage[turn].PriP = 1; // ưu tiên cho ngựa i
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                        if (numvic >= 3 || goodvic >= 2)
                        {
                            AiProps[turn].UseProps(6, cage[turn], board, turn + 1);
                            cage[turn].PriP = 1; // ưu tiên cho ngựa i
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                    }
                    if (!p.Started)
                        numc++;
                    if (p.Ended)
                        nume++;
                }
                if (nume == 3 && numc == 0) //còn 1 quân và đang xuất chuồng
                {
                    if (use >= 2)
                    {
                        AiProps[turn].UseProps(6, cage[turn], board, turn + 1);
                        if (t2 != 0)//ưu tiên cho ngựa i
                            cage[turn].PriP = t2;
                        else
                        {
                            cage[turn].PriP = t1;
                        }
                        this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                        return 1;
                    }
                }
                if (nume == 2 && numc == 0)//còn 2 quân
                {
                    if (use >= 3)
                    {
                        AiProps[turn].UseProps(6, cage[turn], board, turn + 1);
                        if (t2 != 0)//ưu tiên cho ngựa i
                            cage[turn].PriP = t2;
                        else
                        {
                            cage[turn].PriP = t1;
                        }
                        this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                        return 1;
                    }
                }

            }
            return 0;
        }

        private int isActiveProp()
        {
            if (!cage[turn].Useprop)
            {
                if ((AiProps[turn].SpeedUp || AiProps[turn].GetTurn))
                {
                    MyCage c = cage[turn];
                    int numc = 0;
                    int nume = 0;
                    for (int i = 0; i < c.nPieces; i++)
                    {
                        MyPieces p = c.Pieces[i];
                        if (p.Started && !p.Ended && cage[turn].IsAction(i, go)) // đã ra quân và có thể hoạt động
                        {
                            int avic = 0;
                            int bvic = 0;
                            for (int k = -5; k <= 12; k++)
                            {
                                int s = (p.curIndex + k < 0) ? (p.curIndex + 56) : p.curIndex;
                                int team = board.arrSquares[(s + k) % 56].status;
                                int index = board.arrSquares[(s + k) % 56].curIndex;
                                if (team != turn + 1 && team != 0)
                                {
                                    if (k > 0)
                                        avic++; //địch phía trước
                                    else
                                    {
                                        bvic++; //địch phía sau
                                    }
                                }
                            }

                            if (bvic >= 2 && avic == 0 && go == 6)
                            {
                                AiProps[turn].UseProps(5, cage[turn], board, turn + 1);
                                cage[turn].PriP = i; //ưu tiên cho ngựa i
                                this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                return 1;
                            }

                            if (avic == 0 && bvic >= 3)
                            {
                                int realgo = go + ((go % 2 == 0) ? go / 2 : (int)(go / 2 + 1));
                                if (p.EndIndex - (p.curIndex % 56) >= realgo && go > 2)
                                {// SpeedUp
                                    AiProps[turn].UseProps(5, cage[turn], board, turn + 1);
                                    cage[turn].PriP = i; //ưu tiên ngựa i
                                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                    return 1;
                                }
                                if (go != 1 && go != 6 && bvic >= 2 && avic >= 1)
                                {// GetTurn
                                    AiProps[turn].UseProps(3, cage[turn], board, turn + 1);
                                    cage[turn].PriP = i; //ưu tiên ngựa i
                                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                    return 1;
                                }
                            }

                            if (((p.curIndex % 56 < p.StartIndex) ? Math.Abs(p.EndIndex - p.curIndex) <= 28 : Math.Abs(p.curIndex - p.StartIndex) >= 28)
                                && bvic >= 2) //Đã quá nữa bàn cờ và địch ở phía sau
                            {
                                if (AiProps[turn].SpeedUp && go >= 4)
                                {
                                    AiProps[turn].UseProps(5, cage[turn], board, turn + 1);
                                    cage[turn].PriP = i; //ưu tiên ngựa i
                                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                    return 1;
                                }
                                if (AiProps[turn].GetTurn && go != 1 && go != 6)
                                {
                                    AiProps[turn].UseProps(3, cage[turn], board, turn + 1);
                                    cage[turn].PriP = i; ////ưu tiên ngựa i
                                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                    return 1;
                                }
                            }
                        }
                        if (!p.Started)
                            numc++;
                        if (p.Ended)
                            nume++;
                    }
                    if ((nume == 3 || nume == 2) && numc == 0) //Còn 1 hoặc 2 quân và đang xuất quân
                    {
                        if (AiProps[turn].SpeedUp)
                        {
                            AiProps[turn].UseProps(5, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                        if (AiProps[turn].GetTurn && go != 1 && go != 6)
                        {
                            AiProps[turn].UseProps(3, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }

        private int isPassiveProp()
        {
            //sau khi đi xong kiểm tra dùng bomb
            if (AiProps[turn].Bomb)
            {
                int dem = 0;
                int vedich = 0;
                for (int i = 0; i < cage[turn].nPieces; i++)
                {
                    MyPieces p = cage[turn].Pieces[i];
                    if (cage[turn].Pieces[i].Started && !cage[turn].Pieces[i].Ended) //Ra chuong
                    {
                        dem++;
                    }
                    if (((p.curIndex % 56 < p.StartIndex) ? Math.Abs(p.EndIndex - p.curIndex) <= 20 : Math.Abs(p.curIndex - p.StartIndex) >= 35))
                    {
                        vedich++;
                    }
                }
                if (dem >= 3) // có trên 3 quân cờ ra khỏi chuồng
                {
                    AiProps[turn].UseProps(4, cage[turn], board, turn + 1);
                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                }
                if (vedich >= 2) // có trên 2 quân cờ ra khỏi chuồng va 2 con gan ve dich
                {
                    AiProps[turn].UseProps(4, cage[turn], board, turn + 1);
                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                }
            }
            //Kiểm tra sử dụng khiên bảo hộ hoặc ẩn thân
            if ((AiProps[turn].Shield || AiProps[turn].Hide))
            {
                MyCage c = cage[turn];
                int numc = 0;
                int nume = 0;
                int goodvic = 0;
                int bvic = 0;
                int use = 0;
                for (int i = 0; i < c.nPieces; i++)
                {
                    MyPieces p = c.Pieces[i];
                    if (p.Started && !p.Ended && cage[turn].IsAction(i, go)) // đã ra quân và có thể hoạt động
                    {
                        for (int k = -6; k <= 0; k++)
                        {
                            int s = (p.curIndex + k < 0) ? (p.curIndex + 56) : p.curIndex;
                            int team = board.arrSquares[(s + k) % 56].status;
                            int index = board.arrSquares[(s + k) % 56].curIndex;
                            if (team != turn + 1 && team != 0)
                            {
                                bvic++; //địch phía sau
                                MyPieces p1 = cage[team - 1].Pieces[index];
                                if (((p1.curIndex % 56 < p1.StartIndex) ? Math.Abs(p1.EndIndex - p1.curIndex) <= 15 : Math.Abs(p1.curIndex - p1.StartIndex) >= 40))
                                {
                                    use++;
                                    goodvic++;
                                }
                            }
                        }
                        if (((p.curIndex % 56 < p.StartIndex) ? Math.Abs(p.EndIndex - p.curIndex) <= 20 : Math.Abs(p.curIndex - p.StartIndex) >= 35))
                        {
                            use++;
                            if (goodvic >= 2 || bvic >= 3)
                            {
                                if (AiProps[turn].Shield)
                                {
                                    AiProps[turn].UseProps(2, cage[turn], board, turn + 1);
                                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                    return 1;
                                }
                                if (AiProps[turn].Hide)
                                {
                                    AiProps[turn].UseProps(1, cage[turn], board, turn + 1);
                                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                    return 1;
                                }
                            }
                        }
                        if (goodvic >= 3 || bvic >= 4)
                        {
                            if (AiProps[turn].Shield)
                            {
                                AiProps[turn].UseProps(2, cage[turn], board, turn + 1);
                                this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                return 1;
                            }
                            if (AiProps[turn].Hide)
                            {
                                AiProps[turn].UseProps(1, cage[turn], board, turn + 1);
                                this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                return 1;
                            }
                        }
                    }
                    if (!p.Started)
                        numc++;
                    if (p.Ended)
                        nume++;
                }
                if (nume == 3 && numc == 0) // còn 1 quân và đã xuất quân
                {
                    if (use >= 1 && bvic >= 1)
                    {
                        if (AiProps[turn].Shield)
                        {
                            AiProps[turn].UseProps(2, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                        if (AiProps[turn].Hide)
                        {
                            AiProps[turn].UseProps(1, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                    }
                }
                if (nume == 2 && numc == 0) //Còn 1 hoặc 2 quân và đang xuất quân
                {
                    if (use >= 2 && bvic >= 1)
                    {
                        if (AiProps[turn].Shield)
                        {
                            AiProps[turn].UseProps(2, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                        if (AiProps[turn].Hide)
                        {
                            AiProps[turn].UseProps(1, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                    }
                    if (use >= 1 && (goodvic >= 1 || bvic >= 2))
                    {
                        if (AiProps[turn].Shield)
                        {
                            AiProps[turn].UseProps(2, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                        if (AiProps[turn].Hide)
                        {
                            AiProps[turn].UseProps(1, cage[turn], board, turn + 1);
                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        //+++++++++++++++++++++++++++ Computer AI +++++++++++++++++++++++++++++++++++++++++++++++++++//
        private void comAI()
        {
            go = Shaking();
            changeTurn(go);
            //Kiểm tra sử dụng Hổ báo
            if (hasProp)
            {
                isCrossUsed();
            }
            if (cage[turn].CanAction(go))
            {
                if (hasProp)
                {
                    isActiveProp();
                }
                int pieceindex;
                if ((cage[turn].Cross || cage[turn].SpeedUp || cage[turn].GetTurn) && cage[turn].PriP != -2)
                {
                    pieceindex = cage[turn].PriP + 10;
                    if (cage[turn].Pieces[pieceindex - 10].moveFail(go, board))
                    {
                        pieceindex = cage[turn].CheckPieceAI(go);
                    }
                    cage[turn].PriP = -2;
                }
                else
                {
                    pieceindex = cage[turn].CheckPieceAI(go);
                }
                if (pieceindex >= 10 && pieceindex <= 13)
                {
                    if (cage[turn].SpeedUp)
                        go = SpeedUp(turn, pieceindex - 10, go);
                    Process(turn, pieceindex - 10);

                }
                else
                {
                    if (pieceindex >= 20)
                    {
                        vic = cage[turn].Action_AI(go, pieceindex - 20);
                        this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                        if (win)
                            goto exit;
                    }
                    if (pieceindex < 10)
                    {
                        vic = cage[turn].Action_AI(go, pieceindex);
                        this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                    }
                }
            }
            if (turn == nextturn)
            {
                comAI();
            }
            else
            {
                if (!cage[turn].GetTurn)
                {
                    pString.Image = imglstString.Images[nextturn];
                    if (!pString.InvokeRequired)
                        pString.Visible = true;
                    else
                        pString.Invoke(new AntiCrossThread2Argument(SetVisible), pString, true);
                    //Refresh();
                    //Xóa đạo cụ chủ động nếu có xài
                    if (cage[turn].Cross)
                    {
                        cage[turn].Cross = false;
                        cage[turn].clearbuff(board);
                    }
                    if (cage[turn].SpeedUp)
                    {
                        cage[turn].SpeedUp = false;
                        cage[turn].clearbuff(board);//bug
                    }

                    if (cage[nextturn].Hide) //Xóa đạo cụ của địch
                    {
                        for (int i = 0; i < cage[nextturn].nPieces; i++)
                        {
                            MyPieces p = cage[nextturn].Pieces[i];
                            if (p.Started && !p.Ended)
                            {
                                int t = board.arrSquares[p.curIndex % 56].status;
                                if (t != nextturn + 1)
                                {
                                    if (t != 0)
                                    {
                                        int team = board.arrSquares[p.curIndex % 56].status - 1;
                                        int index = board.arrSquares[p.curIndex % 56].curIndex;
                                        cage[team].Update(index);
                                        SoundPlayer sound = new SoundPlayer(@"Resources\Audio\Kick.wav");
                                        sound.LoadAsync();
                                        sound.Play();
                                        sound.Dispose();
                                        this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                        //Thread.Sleep(300);
                                    }
                                    board.arrSquares[p.curIndex % 56].status = nextturn + 1;
                                    board.arrSquares[p.curIndex % 56].curIndex = i;
                                }
                            }
                        }
                        cage[nextturn].Hide = false;
                        cage[nextturn].clearbuff(board);
                    }
                    if (cage[nextturn].Shield) // Xóa đạo cụ của địch
                    {
                        cage[nextturn].Shield = false;
                        cage[nextturn].clearbuff(board);
                    }

                    for (int i = 0; i < cage[turn].nPieces; i++)
                    {
                        if (cage[turn].Pieces[i].IsChoice)
                            cage[turn].Pieces[i].IsChoice = false;
                    }
                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                    if (cage[turn].Useprop)
                        cage[turn].Useprop = false;
                    //KT dùng passiveprop sau khi kết thúc lượt đi
                    if (hasProp)
                    {
                        isPassiveProp();
                    }
                    //Thread.Sleep(200);
                    pString.Invoke(new AntiCrossThread2Argument(SetVisible), pString, false);
                    pShake.Invoke(new AntiCrossThread3Argument(SetImage), pShake, imglstShake.Images[nextturn], 1);
                    btnShake.Invoke(new AntiCrossThread3Argument(SetImage), btnShake, imglstShake.Images[nextturn], 0);
                    turn = nextturn;
                    if (cage[nextturn].isPlayer)
                    {
                        pShake.Invoke(new AntiCrossThread2Argument(SetVisible), pShake, false);
                        btnShake.Invoke(new AntiCrossThread2Argument(SetVisible), btnShake, true);
                    }
                    else
                    {
                        comAI();
                    }
                }
                else
                {
                    MessageBox.Show("Tiếp tục 1 lượt nữa.");
                    cage[turn].GetTurn = false;
                    nextturn = turn;
                    //Refresh();
                    comAI();
                }
            }
        exit:
            if (win && !first)
            {
                first = true;
                Congratulation(turn);
                this.Invoke(new AntiCrossThreadNoArgument(frmMenu.Show));
                this.Invoke(new AntiCrossThreadNoArgument(Dispose));
            }
        }

        public void Thread_AI()
        {
            changeImageTurn();
            comAI();
        }


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        //++++++++++++++++++++++++++++ Player +++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        public void playerProcess()
        {
            if (turn != nextturn)
            {
                if (!cage[turn].GetTurn)
                {
                    btnShake.Visible = false;
                    pShake.Visible = true;

                    if (!useProp && hasProp)
                    {
                        for (int i = 0; i < 6; i++)
                            btnBuff[i].Enabled = false;
                    }
                    else
                    {
                        useProp = false;
                    }

                    for (int i = 0; i < cage[turn].nPieces; i++)
                    {
                        if (cage[turn].Pieces[i].IsChoice)
                            cage[turn].Pieces[i].IsChoice = false;
                    }

                    if (cage[turn].Cross)
                    {
                        cage[turn].Cross = false;
                        cage[turn].clearbuff(board);
                    }
                    if (cage[turn].SpeedUp)
                    {
                        cage[turn].SpeedUp = false;
                        cage[turn].clearbuff(board);
                    }

                    if (cage[nextturn].Hide) // Xoa dao cu cua dich 
                    {
                        for (int i = 0; i < cage[nextturn].nPieces; i++)
                        {
                            MyPieces p = cage[nextturn].Pieces[i];
                            if (p.Started && !p.Ended)
                            {
                                int t = board.arrSquares[p.curIndex % 56].status;
                                if (t != nextturn + 1)
                                {
                                    if (t != 0)
                                    {
                                        int team = board.arrSquares[p.curIndex % 56].status - 1;
                                        int index = board.arrSquares[p.curIndex % 56].curIndex;
                                        cage[team].Update(index);
                                        SoundPlayer sound = new SoundPlayer(@"Resources\Audio\Kick.wav");
                                        sound.LoadAsync();
                                        sound.Play();
                                        sound.Dispose();
                                        this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                        //Thread.Sleep(20);
                                    }
                                    board.arrSquares[p.curIndex % 56].status = nextturn + 1;
                                    board.arrSquares[p.curIndex % 56].curIndex = i;
                                }
                            }
                        }
                        cage[nextturn].Hide = false;
                        cage[nextturn].clearbuff(board);
                    }
                    if (cage[nextturn].Shield) //Xóa đạo cụ của địch
                    {
                        cage[nextturn].Shield = false;
                        cage[nextturn].clearbuff(board);
                    }

                    Refresh();
                    //newthread.Resume();
                    //StartAI = true;
                    if (!cage[nextturn].isPlayer)
                    {
                        Thread thread_AI = new Thread(Thread_AI);
                        thread_AI.Start();
                    }
                    else
                    {
                        changeImageTurn();
                        btnShake.Visible = true;
                        pShake.Visible = false;
                    }
                }
                else //Them luot
                {
                    cage[turn].GetTurn = false;
                    MessageBox.Show("Thêm lượt! Tiến hành lượt quay tiếp theo");
                    nextturn = turn;
                    btnShake.Visible = true;
                    pShake.Visible = false;
                }
            }
            else
            {
                btnShake.Visible = true;
                pShake.Visible = false;
            }
        }

        private void Process(int t, int index)
        {
            for (int i = 0; i < cage[turn].nPieces; i++)
                if (cage[turn].Pieces[i].IsChoice)
                    cage[turn].Pieces[i].IsChoice = false;
            SoundPlayer sound = new SoundPlayer(@"Resources\Audio\move.wav");
            sound.LoadAsync();
            sound.Play();
            sound.Dispose();
            int count = 0;
            while (count < go)
            {
                count++;
                vic = cage[t].Pieces[index].MoveSteponStep(board, index);
                if (vic.nType == -2)
                {
                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                    break;
                }
                if (!this.InvokeRequired)
                {
                    Refresh();
                }
                else
                {
                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                }
            }
        }

        public int SpeedUp(int t, int index, int go)
        {
            int realx = go + ((go % 2 == 0) ? (go / 2) : (int)go / 2 + 1);
            if (!cage[t].Pieces[index].moveFail(realx, board))
                return realx;
            return go;
        }

        private void RacingHorses_MouseUp(object sender, MouseEventArgs e)
        {
            if (isClick == true)
            {
                for (int i = 0; i < cage[turn].nPieces; i++)
                    if (cage[turn].Pieces[i].isChoice(e.X, e.Y))
                        if (cage[turn].IsAction(i, go))
                        {
                            isClick = false;
                            cage[turn].Pieces[i].IsChoice = true;
                            if (cage[turn].Pieces[i].Started && !cage[turn].Pieces[i].Ended)
                            {
                                if (cage[turn].SpeedUp)
                                    go = SpeedUp(turn, i, go);
                                Process(turn, i);
                            }
                            else
                            {
                                vic = cage[turn].Action(go, i);
                                Refresh();
                                if (win)
                                    break;
                            }
                            playerProcess();
                            break;
                        }
                        else
                            break;
            }
            if (win)
            {
                Congratulation(turn);
                this.Dispose();
                frmMenu.Show();
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        private int Shaking()
        {
            int i = 0;
            while (i < 3)
            {
                i++;
                dice.Shake();
                if (!this.InvokeRequired)
                {
                    Refresh();
                }
                else
                {
                    this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                }
                Thread.Sleep(300);
            }
            return dice.curScore;
        }

        private void changeTurn(int x)
        {
            if (x != 1 && x != 6)
            {
                if (nextturn < nCage - 1)
                    nextturn += 1;
                else
                    nextturn = 0;
            }
        }

        private void changeImageTurn()
        {
            if (!this.InvokeRequired)
            {
                turn = nextturn;
                pString.Visible = true;
                pString.Image = imglstString.Images[nextturn];
                pShake.Image = imglstShake.Images[nextturn];
                btnShake.Image = imglstShake.Images[nextturn];
                Refresh();
                pString.Visible = false;
                //Thread.Sleep(1000);
            }
            else
            {
                this.Invoke(new AntiCrossThreadNoArgument(changeImageTurn));
                //Thread.Sleep(300);
            }
        }

        private void SetVisible(Control ctrl, bool b)
        {
            ctrl.Visible = b;
        }

        private void SetImage(Control ctrl, Image img, int type)
        {
            switch (type)
            {
                case 0:
                    Button btn = (Button)ctrl;
                    btn.Image = img;
                    break;
                case 1:
                    PictureBox ptr = (PictureBox)ctrl;
                    ptr.Image = img;
                    break;
            }

        }

        //+++++++++++++++ Button ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        private void btnShake_Click(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(@"Resources\Audio\Click.wav");
            sound.LoadAsync();
            sound.Play();
            sound.Dispose();
            btnShake.Visible = false;
            pShake.Visible = true;
            go = Shaking();
            changeTurn(go);

            if (cage[turn].CanAction(go))
            {
                if (!useProp) // chưa dùng đạo cụ
                {
                    bool[] buffs = props.getState();
                    for (int i = 0; i < 6; i++)
                        btnBuff[i].Enabled = buffs[i];
                }
                for (int i = 0; i < cage[turn].nPieces; i++)
                {
                    cage[turn].Pieces[i].IsChoice = false;
                }
                isClick = true;
            }
            else
            {
                if (go == 1 || go == 6)
                {
                    btnShake.Visible = true;
                    pShake.Visible = false;
                }
                if (go != 1 && go != 6) //Khong them luot
                {
                    if (!cage[turn].GetTurn)
                    {
                        if (!useProp && hasProp)
                        {
                            for (int i = 0; i < 6; i++)
                                btnBuff[i].Enabled = false;
                        }

                        for (int i = 0; i < cage[turn].nPieces; i++)
                        {
                            if (cage[turn].Pieces[i].IsChoice)
                                cage[turn].Pieces[i].IsChoice = false;
                        }

                        //Xoa cross hoac speedup neu co buff
                        if (cage[turn].Cross)
                        {
                            cage[turn].Cross = false;
                            cage[turn].clearbuff(board);
                        }

                        if (cage[turn].SpeedUp)
                        {
                            cage[turn].SpeedUp = false;
                            cage[turn].clearbuff(board);
                        }

                        if (cage[nextturn].Hide) // Xoa dao cu cua dich 
                        {
                            for (int i = 0; i < cage[nextturn].nPieces; i++)
                            {
                                MyPieces p = cage[nextturn].Pieces[i];
                                if (p.Started && !p.Ended)
                                {
                                    int t = board.arrSquares[p.curIndex % 56].status;
                                    if (t != nextturn + 1)
                                    {
                                        if (t != 0)
                                        {
                                            int team = board.arrSquares[p.curIndex % 56].status - 1;
                                            int index = board.arrSquares[p.curIndex % 56].curIndex;
                                            cage[team].Update(index);
                                            sound = new SoundPlayer(@"Resources\Audio\Kick.wav");
                                            sound.LoadAsync();
                                            sound.Play();
                                            sound.Dispose();
                                            this.Invoke(new AntiCrossThreadNoArgument(Refresh));
                                            //Thread.Sleep(20);
                                        }
                                        board.arrSquares[p.curIndex % 56].status = nextturn + 1;
                                        board.arrSquares[p.curIndex % 56].curIndex = i;
                                    }
                                }
                            }
                            cage[nextturn].Hide = false;
                            cage[nextturn].clearbuff(board);
                        }
                        if (cage[nextturn].Shield) // Xoa dao cu cua dich
                        {
                            cage[nextturn].Shield = false;
                            cage[nextturn].clearbuff(board);
                        }
                        int dem = 0;
                        for (int i = 0; i < cage[turn].nPieces; i++)
                            if (cage[turn].Pieces[i].Started && !cage[turn].Pieces[i].Ended)
                                dem++;
                        useProp = false;
                        if (!cage[nextturn].isPlayer)
                        {
                            Thread thread_AI = new Thread(Thread_AI);
                            thread_AI.Start();
                        }
                        else
                        {
                            changeImageTurn();
                            btnShake.Visible = true;
                            pShake.Visible = false;
                        }
                    }
                    else
                    { //Co them luot
                        cage[turn].GetTurn = false;
                        MessageBox.Show("Thêm lượt! Tiến hành lượt quay tiếp theo");
                        btnShake.Visible = true;
                        nextturn = turn;
                        pShake.Visible = false;
                    }
                }

            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            props.UseProps(1, cage[turn], board, turn + 1);

            for (int i = 0; i < 6; i++)
                btnBuff[i].Enabled = false;
            useProp = true;
            Refresh();
        }

        private void btnHide_MouseHover(object sender, EventArgs e)
        {
            toolTipSkill.ToolTipTitle = ("Núp Lùm");
        }

        private void btnShield_Click(object sender, EventArgs e)
        {
            props.UseProps(2, cage[turn], board, turn + 1);
            for (int i = 0; i < 6; i++)
            {
                btnBuff[i].Image = imglstSkillOff.Images[i];
                btnBuff[i].Enabled = false;
            }
            useProp = true;
            Refresh();
        }

        private void btnShield_MouseHover(object sender, EventArgs e)
        {
            toolTipSkill.ToolTipTitle = ("Bá Đạo Thuẫn");
        }

        private void btnGetTurn_Click(object sender, EventArgs e)
        {
            props.UseProps(3, cage[turn], board, turn + 1);
            for (int i = 0; i < 6; i++)
                btnBuff[i].Enabled = false;
            useProp = true;
            Refresh();
        }

        private void btnGetTurn_MouseHover(object sender, EventArgs e)
        {
            toolTipSkill.ToolTipTitle = ("Ăn Gian Lượt");
        }

        private void btnBomb_Click(object sender, EventArgs e)
        {
            props.UseProps(4, cage[turn], board, turn + 1);
            for (int i = 0; i < 6; i++)
                btnBuff[i].Enabled = false;
            useProp = true;
            Refresh();
        }

        private void btnBomb_MouseHover(object sender, EventArgs e)
        {
            toolTipSkill.ToolTipTitle = ("TNT Siêu Cấp");
        }

        private void btnSpeedup_Click(object sender, EventArgs e)
        {
            props.UseProps(5, cage[turn], board, turn + 1);
            for (int i = 0; i < 6; i++)
                btnBuff[i].Enabled = false;
            useProp = true;
            Refresh();
        }

        private void btnSpeedup_MouseHover(object sender, EventArgs e)
        {
            toolTipSkill.ToolTipTitle = ("Chạy Như Bay");
        }

        private void btnCross_Click(object sender, EventArgs e)
        {
            props.UseProps(6, cage[turn], board, turn + 1);
            for (int i = 0; i < 6; i++)
                btnBuff[i].Enabled = false;
            useProp = true;
            Refresh();
        }

        private void btnCross_MouseHover(object sender, EventArgs e)
        {
            toolTipSkill.ToolTipTitle = ("Hổ Báo");
        }



        private void Congratulation(int type)
        {
            switch (type)
            {
                case 0:
                    MessageBox.Show("Chúc mừng đội xanh da trời đã giành chiến thắng");
                    break;
                case 1:
                    MessageBox.Show("Chúc mừng đội đỏ đã giành chiến thắng");
                    break;
                case 2:
                    MessageBox.Show("Chúc mừng đội xanh lá cây đã giành chiến thắng");
                    break;
                case 3:
                    MessageBox.Show("Chúc mừng đội vàng đã giành chiến thắng");
                    break;
            }
        }

        private delegate void AntiCrossThreadNoArgument();
        private delegate void AntiCrossThread2Argument(Control ctrl, bool b);
        private delegate void AntiCrossThread3Argument(Control ctrl, Image img, int type);











    }
}
