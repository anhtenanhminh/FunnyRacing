namespace _06110086_06110045_CoCaNgua
{
    partial class ChoiceTeam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChoiceTeam));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Ptr_Left = new System.Windows.Forms.PictureBox();
            this.Ptr_Right = new System.Windows.Forms.PictureBox();
            this.Ptr_OK = new System.Windows.Forms.PictureBox();
            this.Ptr_Cance = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_OK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_Cance)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::_06110086_06110045_CoCaNgua.Properties.Resources.selectteam;
            this.pictureBox1.Location = new System.Drawing.Point(12, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Ptr_Left
            // 
            this.Ptr_Left.Image = ((System.Drawing.Image)(resources.GetObject("Ptr_Left.Image")));
            this.Ptr_Left.Location = new System.Drawing.Point(12, 92);
            this.Ptr_Left.Name = "Ptr_Left";
            this.Ptr_Left.Size = new System.Drawing.Size(40, 78);
            this.Ptr_Left.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ptr_Left.TabIndex = 1;
            this.Ptr_Left.TabStop = false;
            this.Ptr_Left.Click += new System.EventHandler(this.Ptr_Left_Click);
            this.Ptr_Left.MouseHover += new System.EventHandler(this.Ptr_Left_MouseHover);
            // 
            // Ptr_Right
            // 
            this.Ptr_Right.Image = ((System.Drawing.Image)(resources.GetObject("Ptr_Right.Image")));
            this.Ptr_Right.Location = new System.Drawing.Point(227, 92);
            this.Ptr_Right.Name = "Ptr_Right";
            this.Ptr_Right.Size = new System.Drawing.Size(45, 78);
            this.Ptr_Right.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ptr_Right.TabIndex = 2;
            this.Ptr_Right.TabStop = false;
            this.Ptr_Right.Click += new System.EventHandler(this.Ptr_Right_Click);
            this.Ptr_Right.MouseHover += new System.EventHandler(this.Ptr_Right_MouseHover);
            // 
            // Ptr_OK
            // 
            this.Ptr_OK.Image = global::_06110086_06110045_CoCaNgua.Properties.Resources.ok;
            this.Ptr_OK.Location = new System.Drawing.Point(33, 208);
            this.Ptr_OK.Name = "Ptr_OK";
            this.Ptr_OK.Size = new System.Drawing.Size(67, 52);
            this.Ptr_OK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ptr_OK.TabIndex = 3;
            this.Ptr_OK.TabStop = false;
            this.Ptr_OK.Click += new System.EventHandler(this.Ptr_OK_Click);
            this.Ptr_OK.MouseHover += new System.EventHandler(this.Ptr_OK_MouseHover);
            // 
            // Ptr_Cance
            // 
            this.Ptr_Cance.Image = global::_06110086_06110045_CoCaNgua.Properties.Resources.cancel;
            this.Ptr_Cance.Location = new System.Drawing.Point(143, 208);
            this.Ptr_Cance.Name = "Ptr_Cance";
            this.Ptr_Cance.Size = new System.Drawing.Size(120, 52);
            this.Ptr_Cance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ptr_Cance.TabIndex = 4;
            this.Ptr_Cance.TabStop = false;
            this.Ptr_Cance.Click += new System.EventHandler(this.Ptr_Cance_Click);
            this.Ptr_Cance.MouseHover += new System.EventHandler(this.Ptr_Cance_MouseHover);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "horse_blue.png");
            this.imageList1.Images.SetKeyName(1, "horse_red.png");
            this.imageList1.Images.SetKeyName(2, "horse_green.png");
            this.imageList1.Images.SetKeyName(3, "horse_yellow.png");
            // 
            // ChoiceTeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Ptr_Cance);
            this.Controls.Add(this.Ptr_OK);
            this.Controls.Add(this.Ptr_Right);
            this.Controls.Add(this.Ptr_Left);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ChoiceTeam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choice Team";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChoiceTeam_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChoiceTeam_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_OK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ptr_Cance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox Ptr_Left;
        private System.Windows.Forms.PictureBox Ptr_Right;
        private System.Windows.Forms.PictureBox Ptr_OK;
        private System.Windows.Forms.PictureBox Ptr_Cance;
        private System.Windows.Forms.ImageList imageList1;

    }
}