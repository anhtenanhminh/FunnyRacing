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
    public partial class PlayMode : Form
    {
        Menu frmMenu;
        private bool[] player = new bool[4];
        private bool nonprops = false;
        int count = 1;

        public PlayMode(Menu frm)
        {
            InitializeComponent();
            
            for (int i = 0; i < 4; i++)
                player[i] = false;

            player[0] = true;
            this.frmMenu = frm;
        }


        private void PlayMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMenu.Show();
            this.Dispose();
        }

        private void pBlueCom_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name == "pBluePlayer")
            {
                pBluePlayer.Visible = false;
                pBlueCom.Visible = true;
                player[0] = false;
                count--;
            }
            else
            {
                pBluePlayer.Visible = true;
                pBlueCom.Visible = false;
                player[0] = true;
                count++;
            }
        }


        private void pCancel_Click(object sender, EventArgs e)
        {
            frmMenu.Show();
            this.Dispose();
        }

        private void pOK_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                RacingHorses frm = new RacingHorses(frmMenu);
                for (int i = 0; i < 4; i++)
                {
                    if (player[i])
                        frm.teamchoose.Add(i);
                }
                if (nonprops)
                {
                    frm.hasProp = false;
                }
                this.Hide();
                frm.Show();
            }
        }

        private void pRedCom_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name == "pRedPlayer")
            {
                pRedPlayer.Visible = false;
                pRedCom.Visible = true;
                player[1] = false;
                count--;
            }
            else
            {
                pRedPlayer.Visible = true;
                pRedCom.Visible = false;
                player[1] = true;
                count++;
            }
        }

        private void pGreenCom_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name == "pGreenPlayer")
            {
                pGreenPlayer.Visible = false;
                pGreenCom.Visible = true;
                player[2] = false;
                count--;
            }
            else
            {
                pGreenPlayer.Visible = true;
                pGreenCom.Visible = false;
                player[2] = true;
                count++;
            }
        }

        private void pYellowCom_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name == "pYellowPlayer")
            {
                pYellowPlayer.Visible = false;
                pYellowCom.Visible = true;
                player[3] = false;
                count--;
            }
            else
            {
                pYellowPlayer.Visible = true;
                pYellowCom.Visible = false;
                player[3] = true;
                count++;
            }
        }

        private void pModeNonprops_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name == "pModeNonprops")
            {
                pModeNonprops.Visible = false;
                pModeProps.Visible = true;
                nonprops = false;
            }
            else
            {
                pModeNonprops.Visible = true;
                pModeProps.Visible = false;
                nonprops = true;
            }
        }



       







    }
}
