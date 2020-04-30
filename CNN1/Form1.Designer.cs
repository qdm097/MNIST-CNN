namespace CNN1
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TestCheck = new System.Windows.Forms.CheckBox();
            this.AvgGradTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GuessTxt = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.LayersTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConvPoolsTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BetaTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AlphaTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AvgCorrectTxt = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ErrorTxt = new System.Windows.Forms.TextBox();
            this.RMSCheck = new System.Windows.Forms.CheckBox();
            this.MomentumCheck = new System.Windows.Forms.CheckBox();
            this.INCountTxt = new System.Windows.Forms.TextBox();
            this.HidCountTxt = new System.Windows.Forms.TextBox();
            this.OutCountTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.RMSDecayTxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Batchtxt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(452, 588);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 77);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(808, 588);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 77);
            this.button2.TabIndex = 1;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(452, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 538);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TestCheck
            // 
            this.TestCheck.AutoSize = true;
            this.TestCheck.Location = new System.Drawing.Point(680, 606);
            this.TestCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestCheck.Name = "TestCheck";
            this.TestCheck.Size = new System.Drawing.Size(86, 29);
            this.TestCheck.TabIndex = 4;
            this.TestCheck.Text = "Test";
            this.TestCheck.UseVisualStyleBackColor = true;
            this.TestCheck.CheckedChanged += new System.EventHandler(this.TestCheck_CheckedChanged);
            // 
            // AvgGradTxt
            // 
            this.AvgGradTxt.Location = new System.Drawing.Point(176, 50);
            this.AvgGradTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AvgGradTxt.Name = "AvgGradTxt";
            this.AvgGradTxt.ReadOnly = true;
            this.AvgGradTxt.Size = new System.Drawing.Size(264, 31);
            this.AvgGradTxt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Avg Gradient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(700, 665);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Guess";
            // 
            // GuessTxt
            // 
            this.GuessTxt.Location = new System.Drawing.Point(662, 698);
            this.GuessTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GuessTxt.Name = "GuessTxt";
            this.GuessTxt.ReadOnly = true;
            this.GuessTxt.Size = new System.Drawing.Size(136, 31);
            this.GuessTxt.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1020, 12);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 77);
            this.button3.TabIndex = 9;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1038, 300);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Layers";
            // 
            // LayersTxt
            // 
            this.LayersTxt.Location = new System.Drawing.Point(1044, 329);
            this.LayersTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LayersTxt.Name = "LayersTxt";
            this.LayersTxt.Size = new System.Drawing.Size(136, 31);
            this.LayersTxt.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1038, 373);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Convs + Pools";
            // 
            // ConvPoolsTxt
            // 
            this.ConvPoolsTxt.Location = new System.Drawing.Point(1044, 400);
            this.ConvPoolsTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConvPoolsTxt.Name = "ConvPoolsTxt";
            this.ConvPoolsTxt.Size = new System.Drawing.Size(136, 31);
            this.ConvPoolsTxt.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 435);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 25);
            this.label5.TabIndex = 17;
            // 
            // BetaTxt
            // 
            this.BetaTxt.Location = new System.Drawing.Point(178, 460);
            this.BetaTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BetaTxt.Name = "BetaTxt";
            this.BetaTxt.Size = new System.Drawing.Size(136, 31);
            this.BetaTxt.TabIndex = 16;
            this.BetaTxt.TextChanged += new System.EventHandler(this.BetaTxt_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(172, 502);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Learning Rate";
            // 
            // AlphaTxt
            // 
            this.AlphaTxt.Location = new System.Drawing.Point(178, 531);
            this.AlphaTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AlphaTxt.Name = "AlphaTxt";
            this.AlphaTxt.Size = new System.Drawing.Size(136, 31);
            this.AlphaTxt.TabIndex = 14;
            this.AlphaTxt.TextChanged += new System.EventHandler(this.AlphaTxt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 185);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 25);
            this.label7.TabIndex = 19;
            this.label7.Text = "% Correct";
            // 
            // AvgCorrectTxt
            // 
            this.AvgCorrectTxt.Location = new System.Drawing.Point(176, 215);
            this.AvgCorrectTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AvgCorrectTxt.Name = "AvgCorrectTxt";
            this.AvgCorrectTxt.ReadOnly = true;
            this.AvgCorrectTxt.Size = new System.Drawing.Size(264, 31);
            this.AvgCorrectTxt.TabIndex = 18;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(176, 271);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(264, 44);
            this.button4.TabIndex = 20;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(172, 104);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "Error";
            // 
            // ErrorTxt
            // 
            this.ErrorTxt.Location = new System.Drawing.Point(176, 137);
            this.ErrorTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ErrorTxt.Name = "ErrorTxt";
            this.ErrorTxt.ReadOnly = true;
            this.ErrorTxt.Size = new System.Drawing.Size(264, 31);
            this.ErrorTxt.TabIndex = 21;
            // 
            // RMSCheck
            // 
            this.RMSCheck.AutoSize = true;
            this.RMSCheck.Location = new System.Drawing.Point(178, 327);
            this.RMSCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RMSCheck.Name = "RMSCheck";
            this.RMSCheck.Size = new System.Drawing.Size(134, 29);
            this.RMSCheck.TabIndex = 23;
            this.RMSCheck.Text = "RMSprop";
            this.RMSCheck.UseVisualStyleBackColor = true;
            this.RMSCheck.CheckedChanged += new System.EventHandler(this.RMSCheck_CheckedChanged);
            // 
            // MomentumCheck
            // 
            this.MomentumCheck.AutoSize = true;
            this.MomentumCheck.Location = new System.Drawing.Point(178, 427);
            this.MomentumCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MomentumCheck.Name = "MomentumCheck";
            this.MomentumCheck.Size = new System.Drawing.Size(150, 29);
            this.MomentumCheck.TabIndex = 24;
            this.MomentumCheck.Text = "Momentum";
            this.MomentumCheck.UseVisualStyleBackColor = true;
            this.MomentumCheck.CheckedChanged += new System.EventHandler(this.MomentumCheck_CheckedChanged);
            // 
            // INCountTxt
            // 
            this.INCountTxt.Location = new System.Drawing.Point(1044, 121);
            this.INCountTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.INCountTxt.Name = "INCountTxt";
            this.INCountTxt.Size = new System.Drawing.Size(136, 31);
            this.INCountTxt.TabIndex = 25;
            // 
            // HidCountTxt
            // 
            this.HidCountTxt.Location = new System.Drawing.Point(1044, 192);
            this.HidCountTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HidCountTxt.Name = "HidCountTxt";
            this.HidCountTxt.Size = new System.Drawing.Size(136, 31);
            this.HidCountTxt.TabIndex = 26;
            // 
            // OutCountTxt
            // 
            this.OutCountTxt.Location = new System.Drawing.Point(1044, 263);
            this.OutCountTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OutCountTxt.Name = "OutCountTxt";
            this.OutCountTxt.ReadOnly = true;
            this.OutCountTxt.Size = new System.Drawing.Size(136, 31);
            this.OutCountTxt.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1038, 235);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 25);
            this.label9.TabIndex = 28;
            this.label9.Text = "Output Count";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1038, 163);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 25);
            this.label10.TabIndex = 29;
            this.label10.Text = "Hidden Count";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1038, 92);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 25);
            this.label11.TabIndex = 30;
            this.label11.Text = "Input Count";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1004, 669);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 25);
            this.label12.TabIndex = 32;
            // 
            // RMSDecayTxt
            // 
            this.RMSDecayTxt.Location = new System.Drawing.Point(178, 367);
            this.RMSDecayTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RMSDecayTxt.Name = "RMSDecayTxt";
            this.RMSDecayTxt.Size = new System.Drawing.Size(136, 31);
            this.RMSDecayTxt.TabIndex = 31;
            this.RMSDecayTxt.TextChanged += new System.EventHandler(this.RMSDecayTxt_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(96, 544);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 25);
            this.label13.TabIndex = 33;
            // 
            // Batchtxt
            // 
            this.Batchtxt.Location = new System.Drawing.Point(177, 604);
            this.Batchtxt.Margin = new System.Windows.Forms.Padding(4);
            this.Batchtxt.Name = "Batchtxt";
            this.Batchtxt.Size = new System.Drawing.Size(136, 31);
            this.Batchtxt.TabIndex = 34;
            this.Batchtxt.TextChanged += new System.EventHandler(this.Batchtxt_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(171, 575);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 25);
            this.label14.TabIndex = 35;
            this.label14.Text = "Batch Size";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 760);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Batchtxt);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.RMSDecayTxt);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.OutCountTxt);
            this.Controls.Add(this.HidCountTxt);
            this.Controls.Add(this.INCountTxt);
            this.Controls.Add(this.MomentumCheck);
            this.Controls.Add(this.RMSCheck);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ErrorTxt);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AvgCorrectTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BetaTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AlphaTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ConvPoolsTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LayersTxt);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GuessTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AvgGradTxt);
            this.Controls.Add(this.TestCheck);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox TestCheck;
        private System.Windows.Forms.TextBox AvgGradTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox GuessTxt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LayersTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ConvPoolsTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox BetaTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox AlphaTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AvgCorrectTxt;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ErrorTxt;
        private System.Windows.Forms.CheckBox RMSCheck;
        private System.Windows.Forms.CheckBox MomentumCheck;
        private System.Windows.Forms.TextBox INCountTxt;
        private System.Windows.Forms.TextBox HidCountTxt;
        private System.Windows.Forms.TextBox OutCountTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox RMSDecayTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Batchtxt;
        private System.Windows.Forms.Label label14;
    }
}