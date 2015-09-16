using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _06110086_06110045_CoCaNgua
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        MyButton[] button;
        int nButton = 4;
        int Y = 20;

        private void Menu_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            int X = 160;
            button = new MyButton[nButton];
            for (int i = 0; i < nButton; i++)
            {
                Bitmap[] bmp;
                bmp = new Bitmap[1];
                for (int j = 0; j < 1; j++)
                    bmp[j] = new Bitmap(@"Resources\ImageButton\" + i.ToString() + ".png");
                button[i] = new MyButton(bmp, X, Y, bmp[0].Width - 20, bmp[0].Height);
                Y += bmp[0].Height - 10;

            }

        }
        bool done = false;
        bool collision = false;

        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(new Bitmap(@"Resources\horse.png"), 0, 0, this.Width, this.Height);
            if (done)
                g.DrawImage(new Bitmap(@"Resources\logo.png"), 25, 15, 480, 100);
            for (int i = 0; i < nButton; i++)
                button[i].Draw(g);
        }
        
        private void time_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < nButton; i++)
            {
                if (!collision)
                    button[i].Top += 10;
                else
                    button[i].Top -= 5;
            }
            if (button[0].Top >= 230 && !collision)
                collision = true;
            if (button[0].Top <= 175 && collision)
            {
                timer1.Enabled = false;
                done = true;
            }
            Invalidate();
            Refresh();
        }
        private void Menu_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (done)
            {
                for (int i = 0; i < nButton; i++)
                    button[i].IsClick = button[i].ClickorHover(e.X, e.Y);
                Invalidate();
                Refresh();
            }

            if (button[0].IsClick)
            {
                ChoiceTeam frm = new ChoiceTeam(this);
                frm.Show();
                this.Hide();
            }

            if (button[1].IsClick)
            {
                PlayMode frm = new PlayMode(this);
                frm.Show();
                this.Hide();
            }

            if (button[2].IsClick)
            {
                MessageBox.Show("Hướng dẫn chơi ... cập nhật sau");
                //this.Hide();
            }

            if (button[3].IsClick)
            {
                this.Close();
            }
        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (done)
            {
                for (int i = 0; i < nButton; i++)
                    button[i].IsHover = button[i].ClickorHover(e.X, e.Y);
                Invalidate();
                Refresh();
            }

        }

        
    }
}
