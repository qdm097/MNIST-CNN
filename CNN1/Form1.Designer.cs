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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.AlphaTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AvgCorrectTxt = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ErrorTxt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Batchtxt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Nesterov = new System.Windows.Forms.CheckBox();
            this.Special = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SpecialDecay = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.AddBtn = new System.Windows.Forms.Button();
            this.DelBtn = new System.Windows.Forms.Button();
            this.LayerLB = new System.Windows.Forms.ListBox();
            this.LayerCountTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LayerTypeCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ConvStepsTxt = new System.Windows.Forms.TextBox();
            this.BatchNrmlCB = new System.Windows.Forms.CheckBox();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.UpBtn = new System.Windows.Forms.Button();
            this.DownBtn = new System.Windows.Forms.Button();
            this.DefaultBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(452, 588);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 77);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(820, 591);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 77);
            this.button2.TabIndex = 1;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(452, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 538);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TestCheck
            // 
            this.TestCheck.AutoSize = true;
            this.TestCheck.Location = new System.Drawing.Point(662, 636);
            this.TestCheck.Margin = new System.Windows.Forms.Padding(4);
            this.TestCheck.Name = "TestCheck";
            this.TestCheck.Size = new System.Drawing.Size(86, 29);
            this.TestCheck.TabIndex = 4;
            this.TestCheck.Text = "Test";
            this.TestCheck.UseVisualStyleBackColor = true;
            this.TestCheck.CheckedChanged += new System.EventHandler(this.TestCheck_CheckedChanged);
            // 
            // AvgGradTxt
            // 
            this.AvgGradTxt.Location = new System.Drawing.Point(142, 69);
            this.AvgGradTxt.Margin = new System.Windows.Forms.Padding(4);
            this.AvgGradTxt.Name = "AvgGradTxt";
            this.AvgGradTxt.ReadOnly = true;
            this.AvgGradTxt.Size = new System.Drawing.Size(264, 31);
            this.AvgGradTxt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Avg Gradient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(690, 555);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Guess";
            // 
            // GuessTxt
            // 
            this.GuessTxt.Location = new System.Drawing.Point(662, 588);
            this.GuessTxt.Margin = new System.Windows.Forms.Padding(4);
            this.GuessTxt.Name = "GuessTxt";
            this.GuessTxt.ReadOnly = true;
            this.GuessTxt.Size = new System.Drawing.Size(136, 31);
            this.GuessTxt.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1348, 342);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(249, 54);
            this.button3.TabIndex = 9;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 501);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 25);
            this.label5.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(108, 562);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Learning Rate";
            // 
            // AlphaTxt
            // 
            this.AlphaTxt.Location = new System.Drawing.Point(114, 591);
            this.AlphaTxt.Margin = new System.Windows.Forms.Padding(4);
            this.AlphaTxt.Name = "AlphaTxt";
            this.AlphaTxt.Size = new System.Drawing.Size(136, 31);
            this.AlphaTxt.TabIndex = 14;
            this.AlphaTxt.TextChanged += new System.EventHandler(this.AlphaTxt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 204);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 25);
            this.label7.TabIndex = 19;
            this.label7.Text = "% Correct";
            // 
            // AvgCorrectTxt
            // 
            this.AvgCorrectTxt.Location = new System.Drawing.Point(142, 234);
            this.AvgCorrectTxt.Margin = new System.Windows.Forms.Padding(4);
            this.AvgCorrectTxt.Name = "AvgCorrectTxt";
            this.AvgCorrectTxt.ReadOnly = true;
            this.AvgCorrectTxt.Size = new System.Drawing.Size(264, 31);
            this.AvgCorrectTxt.TabIndex = 18;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(142, 290);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
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
            this.label8.Location = new System.Drawing.Point(138, 123);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "Error";
            // 
            // ErrorTxt
            // 
            this.ErrorTxt.Location = new System.Drawing.Point(142, 156);
            this.ErrorTxt.Margin = new System.Windows.Forms.Padding(4);
            this.ErrorTxt.Name = "ErrorTxt";
            this.ErrorTxt.ReadOnly = true;
            this.ErrorTxt.Size = new System.Drawing.Size(264, 31);
            this.ErrorTxt.TabIndex = 21;
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(171, 601);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 25);
            this.label13.TabIndex = 33;
            // 
            // Batchtxt
            // 
            this.Batchtxt.Location = new System.Drawing.Point(281, 514);
            this.Batchtxt.Margin = new System.Windows.Forms.Padding(4);
            this.Batchtxt.Name = "Batchtxt";
            this.Batchtxt.Size = new System.Drawing.Size(136, 31);
            this.Batchtxt.TabIndex = 34;
            this.Batchtxt.TextChanged += new System.EventHandler(this.Batchtxt_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(278, 485);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 25);
            this.label14.TabIndex = 35;
            this.label14.Text = "Batch Size";
            // 
            // Nesterov
            // 
            this.Nesterov.AutoSize = true;
            this.Nesterov.Location = new System.Drawing.Point(105, 433);
            this.Nesterov.Margin = new System.Windows.Forms.Padding(4);
            this.Nesterov.Name = "Nesterov";
            this.Nesterov.Size = new System.Drawing.Size(130, 29);
            this.Nesterov.TabIndex = 36;
            this.Nesterov.Text = "Nesterov";
            this.Nesterov.UseVisualStyleBackColor = true;
            this.Nesterov.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Special
            // 
            this.Special.FormattingEnabled = true;
            this.Special.Location = new System.Drawing.Point(176, 382);
            this.Special.Name = "Special";
            this.Special.Size = new System.Drawing.Size(196, 33);
            this.Special.TabIndex = 38;
            this.Special.SelectedIndexChanged += new System.EventHandler(this.Special_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(171, 354);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(201, 25);
            this.label15.TabIndex = 39;
            this.label15.Text = "Special Techniques";
            // 
            // SpecialDecay
            // 
            this.SpecialDecay.Location = new System.Drawing.Point(113, 514);
            this.SpecialDecay.Margin = new System.Windows.Forms.Padding(4);
            this.SpecialDecay.Name = "SpecialDecay";
            this.SpecialDecay.Size = new System.Drawing.Size(136, 31);
            this.SpecialDecay.TabIndex = 31;
            this.SpecialDecay.TextChanged += new System.EventHandler(this.SpecialDecay_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(108, 488);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(150, 25);
            this.label16.TabIndex = 40;
            this.label16.Text = "Special Decay";
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(1348, 280);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(4);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(120, 54);
            this.AddBtn.TabIndex = 41;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // DelBtn
            // 
            this.DelBtn.Location = new System.Drawing.Point(1476, 280);
            this.DelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Size = new System.Drawing.Size(121, 54);
            this.DelBtn.TabIndex = 42;
            this.DelBtn.Text = "Remove";
            this.DelBtn.UseVisualStyleBackColor = true;
            this.DelBtn.Click += new System.EventHandler(this.DelBtn_Click);
            // 
            // LayerLB
            // 
            this.LayerLB.FormattingEnabled = true;
            this.LayerLB.ItemHeight = 25;
            this.LayerLB.Location = new System.Drawing.Point(1045, 13);
            this.LayerLB.Name = "LayerLB";
            this.LayerLB.Size = new System.Drawing.Size(296, 529);
            this.LayerLB.TabIndex = 43;
            this.LayerLB.SelectedIndexChanged += new System.EventHandler(this.LayerLB_SelectedIndexChanged);
            // 
            // LayerCountTxt
            // 
            this.LayerCountTxt.Location = new System.Drawing.Point(1347, 113);
            this.LayerCountTxt.Margin = new System.Windows.Forms.Padding(4);
            this.LayerCountTxt.Name = "LayerCountTxt";
            this.LayerCountTxt.Size = new System.Drawing.Size(121, 31);
            this.LayerCountTxt.TabIndex = 45;
            this.LayerCountTxt.TextChanged += new System.EventHandler(this.LayerCountTxt_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1348, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 25);
            this.label3.TabIndex = 46;
            this.label3.Text = "Type of Layer";
            // 
            // LayerTypeCB
            // 
            this.LayerTypeCB.FormattingEnabled = true;
            this.LayerTypeCB.Location = new System.Drawing.Point(1347, 40);
            this.LayerTypeCB.Name = "LayerTypeCB";
            this.LayerTypeCB.Size = new System.Drawing.Size(222, 33);
            this.LayerTypeCB.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1348, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 25);
            this.label4.TabIndex = 48;
            this.label4.Text = "Layer Size";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(276, 562);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 25);
            this.label9.TabIndex = 50;
            this.label9.Text = "Conv Step Size";
            // 
            // ConvStepsTxt
            // 
            this.ConvStepsTxt.Location = new System.Drawing.Point(281, 591);
            this.ConvStepsTxt.Margin = new System.Windows.Forms.Padding(4);
            this.ConvStepsTxt.Name = "ConvStepsTxt";
            this.ConvStepsTxt.Size = new System.Drawing.Size(136, 31);
            this.ConvStepsTxt.TabIndex = 49;
            this.ConvStepsTxt.TextChanged += new System.EventHandler(this.ConvStepsTxt_TextChanged);
            // 
            // BatchNrmlCB
            // 
            this.BatchNrmlCB.AutoSize = true;
            this.BatchNrmlCB.Location = new System.Drawing.Point(243, 433);
            this.BatchNrmlCB.Margin = new System.Windows.Forms.Padding(4);
            this.BatchNrmlCB.Name = "BatchNrmlCB";
            this.BatchNrmlCB.Size = new System.Drawing.Size(201, 29);
            this.BatchNrmlCB.TabIndex = 51;
            this.BatchNrmlCB.Text = "Batch Normalize";
            this.BatchNrmlCB.UseVisualStyleBackColor = true;
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Location = new System.Drawing.Point(1347, 218);
            this.UpdateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(121, 54);
            this.UpdateBtn.TabIndex = 52;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // UpBtn
            // 
            this.UpBtn.Location = new System.Drawing.Point(1347, 156);
            this.UpBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UpBtn.Name = "UpBtn";
            this.UpBtn.Size = new System.Drawing.Size(121, 54);
            this.UpBtn.TabIndex = 53;
            this.UpBtn.Text = "Up";
            this.UpBtn.UseVisualStyleBackColor = true;
            this.UpBtn.Click += new System.EventHandler(this.UpBtn_Click);
            // 
            // DownBtn
            // 
            this.DownBtn.Location = new System.Drawing.Point(1476, 156);
            this.DownBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DownBtn.Name = "DownBtn";
            this.DownBtn.Size = new System.Drawing.Size(121, 54);
            this.DownBtn.TabIndex = 54;
            this.DownBtn.Text = "Down";
            this.DownBtn.UseVisualStyleBackColor = true;
            this.DownBtn.Click += new System.EventHandler(this.DownBtn_Click);
            // 
            // DefaultBtn
            // 
            this.DefaultBtn.Location = new System.Drawing.Point(1476, 218);
            this.DefaultBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DefaultBtn.Name = "DefaultBtn";
            this.DefaultBtn.Size = new System.Drawing.Size(121, 54);
            this.DefaultBtn.TabIndex = 55;
            this.DefaultBtn.Text = "Default";
            this.DefaultBtn.UseVisualStyleBackColor = true;
            this.DefaultBtn.Click += new System.EventHandler(this.DefaultBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1614, 760);
            this.Controls.Add(this.DefaultBtn);
            this.Controls.Add(this.DownBtn);
            this.Controls.Add(this.UpBtn);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.BatchNrmlCB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ConvStepsTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LayerTypeCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LayerCountTxt);
            this.Controls.Add(this.LayerLB);
            this.Controls.Add(this.DelBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Special);
            this.Controls.Add(this.Nesterov);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Batchtxt);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.SpecialDecay);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ErrorTxt);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AvgCorrectTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AlphaTxt);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GuessTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AvgGradTxt);
            this.Controls.Add(this.TestCheck);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox AlphaTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AvgCorrectTxt;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ErrorTxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Batchtxt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox Nesterov;
        private System.Windows.Forms.ComboBox Special;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox SpecialDecay;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button DelBtn;
        private System.Windows.Forms.ListBox LayerLB;
        private System.Windows.Forms.TextBox LayerCountTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox LayerTypeCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ConvStepsTxt;
        private System.Windows.Forms.CheckBox BatchNrmlCB;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button UpBtn;
        private System.Windows.Forms.Button DownBtn;
        private System.Windows.Forms.Button DefaultBtn;
    }
}