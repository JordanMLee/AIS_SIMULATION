namespace AIS_SIMULATION
{
    partial class TwoVesselSimulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwoVesselSimulation));
            this.startButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.PacketTimer = new System.Windows.Forms.Timer(this.components);
            this.LoopTimer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Lat2Box = new System.Windows.Forms.Label();
            this.Long2Box = new System.Windows.Forms.Label();
            this.Lat1Box = new System.Windows.Forms.TextBox();
            this.Long1Box = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PB2timer = new System.Windows.Forms.Timer(this.components);
            this.timerAfterSend = new System.Windows.Forms.Timer(this.components);
            this.timerAfterSend1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(318, 236);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(99, 34);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(423, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "STOP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "COAST GUARD VESSEL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(689, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "TARGET VESSEL";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(318, 72);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(215, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(318, 128);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.progressBar2.RightToLeftLayout = true;
            this.progressBar2.Size = new System.Drawing.Size(215, 23);
            this.progressBar2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBox1.Location = new System.Drawing.Point(37, 236);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(235, 137);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBox2.Location = new System.Drawing.Point(562, 236);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(235, 136);
            this.textBox2.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(37, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 136);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(562, 43);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(235, 136);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(271, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(290, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "AIS PROTOCOL SIMULATION";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(380, 294);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 46);
            this.button3.TabIndex = 11;
            this.button3.Text = "3-vessel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // PacketTimer
            // 
            this.PacketTimer.Interval = 1;
            this.PacketTimer.Tick += new System.EventHandler(this.PacketTimer_Tick);
            // 
            // LoopTimer
            // 
            this.LoopTimer.Interval = 1000;
            this.LoopTimer.Tick += new System.EventHandler(this.LoopTimer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Lat1:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Long1:";
            // 
            // Lat2Box
            // 
            this.Lat2Box.AutoSize = true;
            this.Lat2Box.Location = new System.Drawing.Point(559, 203);
            this.Lat2Box.Name = "Lat2Box";
            this.Lat2Box.Size = new System.Drawing.Size(31, 13);
            this.Lat2Box.TabIndex = 14;
            this.Lat2Box.Text = "Lat2:";
            // 
            // Long2Box
            // 
            this.Long2Box.AutoSize = true;
            this.Long2Box.Location = new System.Drawing.Point(706, 203);
            this.Long2Box.Name = "Long2Box";
            this.Long2Box.Size = new System.Drawing.Size(40, 13);
            this.Long2Box.TabIndex = 15;
            this.Long2Box.Text = "Long2:";
            // 
            // Lat1Box
            // 
            this.Lat1Box.Location = new System.Drawing.Point(75, 200);
            this.Lat1Box.Name = "Lat1Box";
            this.Lat1Box.Size = new System.Drawing.Size(83, 20);
            this.Lat1Box.TabIndex = 16;
            this.Lat1Box.Text = "41.3782";
            // 
            // Long1Box
            // 
            this.Long1Box.Location = new System.Drawing.Point(213, 200);
            this.Long1Box.Name = "Long1Box";
            this.Long1Box.Size = new System.Drawing.Size(59, 20);
            this.Long1Box.TabIndex = 17;
            this.Long1Box.Text = "-072.0947";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(600, 200);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 18;
            this.textBox5.Text = "41.3778";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(747, 200);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(71, 20);
            this.textBox6.TabIndex = 19;
            this.textBox6.Text = "-072.0944";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PB2timer
            // 
            this.PB2timer.Interval = 1;
            this.PB2timer.Tick += new System.EventHandler(this.PB2timer_Tick);
            // 
            // timerAfterSend
            // 
            this.timerAfterSend.Interval = 1000;
            this.timerAfterSend.Tick += new System.EventHandler(this.timerAfterSend_Tick);
            // 
            // timerAfterSend1
            // 
            this.timerAfterSend1.Interval = 1000;
            this.timerAfterSend1.Tick += new System.EventHandler(this.timerAfterSend1_Tick);
            // 
            // TwoVesselSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(830, 405);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.Long1Box);
            this.Controls.Add(this.Lat1Box);
            this.Controls.Add(this.Long2Box);
            this.Controls.Add(this.Lat2Box);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startButton);
            this.Name = "TwoVesselSimulation";
            this.Text = "TwoVesselSimulation";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer PacketTimer;
        private System.Windows.Forms.Timer LoopTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Lat2Box;
        private System.Windows.Forms.Label Long2Box;
        public System.Windows.Forms.TextBox Lat1Box;
        public System.Windows.Forms.TextBox Long1Box;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer PB2timer;
        private System.Windows.Forms.Timer timerAfterSend;
        private System.Windows.Forms.Timer timerAfterSend1;
    }
}

