namespace MathTutorApp
{
    partial class cmbTime
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblQuestion = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            lblScore = new Label();
            lblFeedback = new Label();
            cmbDifficulty = new ComboBox();
            lblPercentage = new Label();
            lblHighScore = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            cmbTimeLimit = new ComboBox();
            btnRestart = new Button();
            btnStart = new Button();
            lblTimer = new Label();
            lblPlayerName = new Label();
            txtPlayerName = new TextBox();
            btnLeaderboard = new Button();
            SuspendLayout();
            // 
            // lblQuestion
            // 
            lblQuestion.BackColor = Color.White;
            lblQuestion.BorderStyle = BorderStyle.FixedSingle;
            lblQuestion.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuestion.ForeColor = Color.FromArgb(25, 43, 78);
            lblQuestion.Location = new Point(280, 170);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(420, 90);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "5 + 8 = ?";
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.FromArgb(25, 43, 78);
            textBox1.Location = new Point(390, 276);
            textBox1.MaxLength = 10;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(220, 35);
            textBox1.TabIndex = 1;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(0, 174, 160);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(405, 317);
            button1.Name = "button1";
            button1.Size = new Size(160, 44);
            button1.TabIndex = 2;
            button1.Text = "Submit Answer";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScore.ForeColor = Color.FromArgb(75, 63, 180);
            lblScore.Location = new Point(441, 426);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(102, 25);
            lblScore.TabIndex = 3;
            lblScore.Text = "Score: 0/0";
            // 
            // lblFeedback
            // 
            lblFeedback.BackColor = Color.Transparent;
            lblFeedback.ForeColor = Color.FromArgb(96, 113, 137);
            lblFeedback.Location = new Point(350, 383);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Size = new Size(300, 43);
            lblFeedback.TabIndex = 4;
            lblFeedback.Text = "Feedback";
            lblFeedback.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.BackColor = Color.White;
            cmbDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDifficulty.FlatStyle = FlatStyle.Flat;
            cmbDifficulty.ForeColor = Color.FromArgb(25, 43, 78);
            cmbDifficulty.FormattingEnabled = true;
            cmbDifficulty.Items.AddRange(new object[] { "Easy", "Meduim ", "Hard" });
            cmbDifficulty.Location = new Point(334, 27);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(180, 25);
            cmbDifficulty.TabIndex = 5;
            cmbDifficulty.SelectedIndexChanged += cmbDifficulty_SelectedIndexChanged_1;
            // 
            // lblPercentage
            // 
            lblPercentage.BackColor = Color.Transparent;
            lblPercentage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPercentage.ForeColor = Color.FromArgb(25, 43, 78);
            lblPercentage.Location = new Point(405, 451);
            lblPercentage.Name = "lblPercentage";
            lblPercentage.Size = new Size(180, 35);
            lblPercentage.TabIndex = 6;
            lblPercentage.Text = " 0%";
            lblPercentage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHighScore
            // 
            lblHighScore.BackColor = Color.Transparent;
            lblHighScore.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHighScore.ForeColor = Color.FromArgb(25, 43, 78);
            lblHighScore.ImageAlign = ContentAlignment.MiddleLeft;
            lblHighScore.Location = new Point(178, 75);
            lblHighScore.Name = "lblHighScore";
            lblHighScore.Size = new Size(150, 30);
            lblHighScore.TabIndex = 7;
            lblHighScore.Text = "High Score: 0";
            lblHighScore.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // cmbTimeLimit
            // 
            cmbTimeLimit.FormattingEnabled = true;
            cmbTimeLimit.Items.AddRange(new object[] { "10 Min", "20 Min", "30 Min", "40 Min", "50 Min", "1 Hour " });
            cmbTimeLimit.Location = new Point(588, 30);
            cmbTimeLimit.Name = "cmbTimeLimit";
            cmbTimeLimit.Size = new Size(100, 25);
            cmbTimeLimit.TabIndex = 0;
            cmbTimeLimit.Tag = "10";
            cmbTimeLimit.Text = "10 Min";
            cmbTimeLimit.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnRestart
            // 
            btnRestart.AutoSize = true;
            btnRestart.BackColor = Color.White;
            btnRestart.Cursor = Cursors.Hand;
            btnRestart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRestart.ForeColor = Color.FromArgb(0, 145, 135);
            btnRestart.Location = new Point(425, 527);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(140, 40);
            btnRestart.TabIndex = 10;
            btnRestart.Text = "Restart Quiz";
            btnRestart.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            btnStart.AutoSize = true;
            btnStart.BackColor = Color.FromArgb(0, 174, 160);
            btnStart.Cursor = Cursors.Hand;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(20, 74);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(140, 42);
            btnStart.TabIndex = 11;
            btnStart.Text = "Start Quiz";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.BackColor = Color.Transparent;
            lblTimer.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTimer.ForeColor = Color.FromArgb(25, 43, 78);
            lblTimer.Location = new Point(334, 85);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(209, 20);
            lblTimer.TabIndex = 12;
            lblTimer.Text = "Time Remaining: 10 seconds";
            lblTimer.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.BackColor = Color.Transparent;
            lblPlayerName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPlayerName.ForeColor = Color.FromArgb(25, 43, 78);
            lblPlayerName.Location = new Point(20, 30);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(90, 17);
            lblPlayerName.TabIndex = 13;
            lblPlayerName.Text = "Player Name:";
            lblPlayerName.Click += label1_Click;
            // 
            // txtPlayerName
            // 
            txtPlayerName.BackColor = Color.White;
            txtPlayerName.ForeColor = Color.FromArgb(25, 43, 78);
            txtPlayerName.Location = new Point(105, 27);
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.Size = new Size(180, 25);
            txtPlayerName.TabIndex = 14;
            txtPlayerName.TextAlign = HorizontalAlignment.Center;
            // 
            // btnLeaderboard
            // 
            btnLeaderboard.BackColor = Color.FromArgb(70, 90, 120);
            btnLeaderboard.Cursor = Cursors.Hand;
            btnLeaderboard.FlatAppearance.BorderSize = 0;
            btnLeaderboard.FlatStyle = FlatStyle.Flat;
            btnLeaderboard.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLeaderboard.ForeColor = Color.White;
            btnLeaderboard.Location = new Point(390, 573);
            btnLeaderboard.Name = "btnLeaderboard";
            btnLeaderboard.Size = new Size(210, 40);
            btnLeaderboard.TabIndex = 15;
            btnLeaderboard.Text = "View Leaderboard";
            btnLeaderboard.UseVisualStyleBackColor = false;
            // 
            // cmbTime
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(984, 711);
            Controls.Add(btnLeaderboard);
            Controls.Add(txtPlayerName);
            Controls.Add(lblPlayerName);
            Controls.Add(lblTimer);
            Controls.Add(btnStart);
            Controls.Add(btnRestart);
            Controls.Add(cmbTimeLimit);
            Controls.Add(lblHighScore);
            Controls.Add(lblPercentage);
            Controls.Add(cmbDifficulty);
            Controls.Add(lblFeedback);
            Controls.Add(lblScore);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(lblQuestion);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Purple;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "cmbTime";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Math Tutor – Challenge Yourself";
            Load += cmbTime_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblQuestion;
        private TextBox textBox1;
        private Button button1;
        private Label lblScore;
        private Label lblFeedback;
        private ComboBox cmbDifficulty;
        private Label lblPercentage;
        private Label lblHighScore;
        private System.Windows.Forms.Timer timer1;
        private ComboBox cmbTimeLimit;
        private Button btnRestart;
        private Button btnStart;
        private Label lblTimer;
        private Label lblPlayerName;
        private TextBox txtPlayerName;
        private Button btnLeaderboard;
    }
}
