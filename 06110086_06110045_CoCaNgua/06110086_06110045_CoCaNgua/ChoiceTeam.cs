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
    public partial class ChoiceTeam : Form
    {
        Menu frmMenu;
        public ChoiceTeam(Menu frm)
        {
            InitializeComponent();
            this.frmMenu = frm;
        }
        private int team = 0;
        int X = 100;
        int Y = 90;
        

        private void Ptr_OK_Click(object sender, EventArgs e)
        {
            RacingHorses frm = new RacingHorses(frmMenu);
            frm.teamchoose.Add(team);
            this.Hide();
            frm.Show();
        }

        private void Ptr_Cance_Click(object sender, EventArgs e)
        {
            frmMenu.Show();
            this.Dispose();
        }

        private void Ptr_Left_Click(object sender, EventArgs e)
        {
            team--;
            if (team < 0)
                team = 3;
            Refresh();
        }

        private void Ptr_Right_Click(object sender, EventArgs e)
        {
            team++;
            if (team > 3)
                team = 0;
            Refresh();
        }

        private void ChoiceTeam_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMenu.Show();
            this.Dispose();
        }

        private void ChoiceTeam_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap bmp = new Bitmap(imageList1.Images[team]);
            g.DrawImage(bmp,X,Y);
            bmp.Dispose();

        }

        private void Ptr_Right_MouseHover(object sender, EventArgs e)
        {
            

        }

        private void Ptr_Left_MouseHover(object sender, EventArgs e)
        {

        }

        private void Ptr_OK_MouseHover(object sender, EventArgs e)
        {

        }

        private void Ptr_Cance_MouseHover(object sender, EventArgs e)
        {

        }






    }
}
