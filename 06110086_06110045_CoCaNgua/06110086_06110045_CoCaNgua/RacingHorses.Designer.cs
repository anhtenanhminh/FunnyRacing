namespace _06110086_06110045_CoCaNgua
{
    partial class RacingHorses
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RacingHorses));
            this.imglstBomb = new System.Windows.Forms.ImageList(this.components);
            this.imglstShake = new System.Windows.Forms.ImageList(this.components);
            this.imglstString = new System.Windows.Forms.ImageList(this.components);
            this.imglstArrow = new System.Windows.Forms.ImageList(this.components);
            this.pString = new System.Windows.Forms.PictureBox();
            this.pShake = new System.Windows.Forms.PictureBox();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnShield = new System.Windows.Forms.Button();
            this.btnGetTurn = new System.Windows.Forms.Button();
            this.btnSpeedup = new System.Windows.Forms.Button();
            this.btnBomb = new System.Windows.Forms.Button();
            this.btnCross = new System.Windows.Forms.Button();
            this.toolTipSkill = new System.Windows.Forms.ToolTip(this.components);
            this.btnShake = new System.Windows.Forms.Button();
            this.imglstSkillOff = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pShake)).BeginInit();
            this.SuspendLayout();
            // 
            // imglstBomb
            // 
            this.imglstBomb.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstBomb.ImageStream")));
            this.imglstBomb.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstBomb.Images.SetKeyName(0, "boomblue.png");
            this.imglstBomb.Images.SetKeyName(1, "boomred.png");
            this.imglstBomb.Images.SetKeyName(2, "boomgreen.png");
            this.imglstBomb.Images.SetKeyName(3, "boomyellow.png");
            // 
            // imglstShake
            // 
            this.imglstShake.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstShake.ImageStream")));
            this.imglstShake.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstShake.Images.SetKeyName(0, "blue_shake.png");
            this.imglstShake.Images.SetKeyName(1, "red_shake.png");
            this.imglstShake.Images.SetKeyName(2, "green_shake.png");
            this.imglstShake.Images.SetKeyName(3, "yellow_shake.png");
            // 
            // imglstString
            // 
            this.imglstString.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstString.ImageStream")));
            this.imglstString.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstString.Images.SetKeyName(0, "bluesreen.png");
            this.imglstString.Images.SetKeyName(1, "redsreen.png");
            this.imglstString.Images.SetKeyName(2, "greensreen.png");
            this.imglstString.Images.SetKeyName(3, "yellowsreen.png");
            // 
            // imglstArrow
            // 
            this.imglstArrow.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstArrow.ImageStream")));
            this.imglstArrow.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstArrow.Images.SetKeyName(0, "arrowblue.png");
            this.imglstArrow.Images.SetKeyName(1, "arrowred.png");
            this.imglstArrow.Images.SetKeyName(2, "arrowgreen.png");
            this.imglstArrow.Images.SetKeyName(3, "arrowyellow.png");
            // 
            // pString
            // 
            this.pString.BackColor = System.Drawing.Color.Transparent;
            this.pString.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pString.Location = new System.Drawing.Point(101, 207);
            this.pString.Name = "pString";
            this.pString.Size = new System.Drawing.Size(623, 162);
            this.pString.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pString.TabIndex = 0;
            this.pString.TabStop = false;
            // 
            // pShake
            // 
            this.pShake.BackColor = System.Drawing.Color.Transparent;
            this.pShake.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pShake.Location = new System.Drawing.Point(289, 498);
            this.pShake.Name = "pShake";
            this.pShake.Size = new System.Drawing.Size(60, 58);
            this.pShake.TabIndex = 1;
            this.pShake.TabStop = false;
            this.pShake.Visible = false;
            // 
            // btnHide
            // 
            this.btnHide.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.ghost;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHide.Location = new System.Drawing.Point(462, 525);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(53, 49);
            this.btnHide.TabIndex = 2;
            this.btnHide.Text = "Núp lùm";
            this.toolTipSkill.SetToolTip(this.btnHide, "Thân tan vào khí, thực thực hư hư \r\n khiến đối thủ không thể nào bắt được.");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            this.btnHide.MouseHover += new System.EventHandler(this.btnHide_MouseHover);
            // 
            // btnShield
            // 
            this.btnShield.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.shield;
            this.btnShield.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShield.Location = new System.Drawing.Point(516, 525);
            this.btnShield.Name = "btnShield";
            this.btnShield.Size = new System.Drawing.Size(53, 49);
            this.btnShield.TabIndex = 3;
            this.btnShield.Text = "Bá Đạo Thuẫn";
            this.toolTipSkill.SetToolTip(this.btnShield, "Cường khí hộ thân, đạt đến trình độ kim cương bất hoại. \r\n Địch đá vào như thể đá" +
                    " cột.");
            this.btnShield.UseVisualStyleBackColor = true;
            this.btnShield.Click += new System.EventHandler(this.btnShield_Click);
            this.btnShield.MouseHover += new System.EventHandler(this.btnShield_MouseHover);
            // 
            // btnGetTurn
            // 
            this.btnGetTurn.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.turnup;
            this.btnGetTurn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetTurn.Location = new System.Drawing.Point(571, 525);
            this.btnGetTurn.Name = "btnGetTurn";
            this.btnGetTurn.Size = new System.Drawing.Size(53, 49);
            this.btnGetTurn.TabIndex = 4;
            this.btnGetTurn.Text = "Ăn gian lượt";
            this.toolTipSkill.SetToolTip(this.btnGetTurn, "Diện không biến sắc, sử chiêu dương đông kích tây \r\n lợi dụng sơ hở lắc xí ngầu t" +
                    "hêm 1 lần nữa.");
            this.btnGetTurn.UseVisualStyleBackColor = true;
            this.btnGetTurn.Click += new System.EventHandler(this.btnGetTurn_Click);
            this.btnGetTurn.MouseHover += new System.EventHandler(this.btnGetTurn_MouseHover);
            // 
            // btnSpeedup
            // 
            this.btnSpeedup.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.speed;
            this.btnSpeedup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpeedup.Location = new System.Drawing.Point(680, 525);
            this.btnSpeedup.Name = "btnSpeedup";
            this.btnSpeedup.Size = new System.Drawing.Size(53, 49);
            this.btnSpeedup.TabIndex = 6;
            this.btnSpeedup.Text = "Chạy Như Bay";
            this.toolTipSkill.SetToolTip(this.btnSpeedup, "Uống cạn vò Bò húc, thân thể xung mãn, \r\n lấy đà chạy như bay. (Tăng 50% số ô di " +
                    "chuyển).");
            this.btnSpeedup.UseVisualStyleBackColor = true;
            this.btnSpeedup.Click += new System.EventHandler(this.btnSpeedup_Click);
            this.btnSpeedup.MouseHover += new System.EventHandler(this.btnSpeedup_MouseHover);
            // 
            // btnBomb
            // 
            this.btnBomb.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.bomb;
            this.btnBomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBomb.Location = new System.Drawing.Point(625, 525);
            this.btnBomb.Name = "btnBomb";
            this.btnBomb.Size = new System.Drawing.Size(53, 49);
            this.btnBomb.TabIndex = 5;
            this.btnBomb.Text = "TNT Siêu Cấp";
            this.toolTipSkill.SetToolTip(this.btnBomb, "Chẳng nói chẳng rằng, lặng lẽ đào hố, tặng kèm vài kí TNT \r\n địch đi ngang chỉ có" +
                    " nước về thành dưỡng sức.");
            this.btnBomb.UseVisualStyleBackColor = true;
            this.btnBomb.Click += new System.EventHandler(this.btnBomb_Click);
            this.btnBomb.MouseHover += new System.EventHandler(this.btnBomb_MouseHover);
            // 
            // btnCross
            // 
            this.btnCross.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.attack;
            this.btnCross.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCross.Location = new System.Drawing.Point(735, 525);
            this.btnCross.Name = "btnCross";
            this.btnCross.Size = new System.Drawing.Size(53, 49);
            this.btnCross.TabIndex = 7;
            this.btnCross.Text = "Hổ Báo";
            this.toolTipSkill.SetToolTip(this.btnCross, "Vũ trang tận răng, bất chấp chướng ngại \r\n vẫn lao đi như chỗ không người.");
            this.btnCross.UseVisualStyleBackColor = true;
            this.btnCross.Click += new System.EventHandler(this.btnCross_Click);
            this.btnCross.MouseHover += new System.EventHandler(this.btnCross_MouseHover);
            // 
            // toolTipSkill
            // 
            this.toolTipSkill.AutomaticDelay = 1000;
            this.toolTipSkill.IsBalloon = true;
            this.toolTipSkill.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipSkill.ToolTipTitle = "Kỹ Năng";
            this.toolTipSkill.UseAnimation = false;
            // 
            // btnShake
            // 
            this.btnShake.BackgroundImage = global::_06110086_06110045_CoCaNgua.Properties.Resources.blue_shake;
            this.btnShake.Location = new System.Drawing.Point(289, 498);
            this.btnShake.Name = "btnShake";
            this.btnShake.Size = new System.Drawing.Size(60, 58);
            this.btnShake.TabIndex = 8;
            this.btnShake.UseVisualStyleBackColor = true;
            this.btnShake.Click += new System.EventHandler(this.btnShake_Click);
            // 
            // imglstSkillOff
            // 
            this.imglstSkillOff.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSkillOff.ImageStream")));
            this.imglstSkillOff.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSkillOff.Images.SetKeyName(0, "ghostOff.png");
            this.imglstSkillOff.Images.SetKeyName(1, "shieldOff.png");
            this.imglstSkillOff.Images.SetKeyName(2, "turnupOff.png");
            this.imglstSkillOff.Images.SetKeyName(3, "bombOff.png");
            this.imglstSkillOff.Images.SetKeyName(4, "speedOff.png");
            this.imglstSkillOff.Images.SetKeyName(5, "crossOff.png");
            // 
            // RacingHorses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 597);
            this.Controls.Add(this.btnCross);
            this.Controls.Add(this.btnSpeedup);
            this.Controls.Add(this.btnBomb);
            this.Controls.Add(this.btnGetTurn);
            this.Controls.Add(this.btnShield);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.pString);
            this.Controls.Add(this.pShake);
            this.Controls.Add(this.btnShake);
            this.DoubleBuffered = true;
            this.Name = "RacingHorses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Funny Racing Horses";
            this.Load += new System.EventHandler(this.RacingHorses_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RacingHorses_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RacingHorses_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RacingHorses_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pShake)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglstBomb;
        private System.Windows.Forms.ImageList imglstShake;
        private System.Windows.Forms.ImageList imglstString;
        private System.Windows.Forms.ImageList imglstArrow;
        private System.Windows.Forms.PictureBox pString;
        private System.Windows.Forms.PictureBox pShake;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnShield;
        private System.Windows.Forms.Button btnGetTurn;
        private System.Windows.Forms.Button btnSpeedup;
        private System.Windows.Forms.Button btnBomb;
        private System.Windows.Forms.Button btnCross;
        private System.Windows.Forms.ToolTip toolTipSkill;
        private System.Windows.Forms.Button btnShake;
        private System.Windows.Forms.ImageList imglstSkillOff;
    }
}