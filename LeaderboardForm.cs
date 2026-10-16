using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MathTutorApp
{
    public partial class LeaderboardForm : Form
    {
        private Label lblBestScoreValue = null!;
        private Label lblTotalPlayersValue = null!;
        private Label lblTopAccuracyValue = null!;

        private readonly string leaderboardFile =
     Path.Combine(
         Environment.GetFolderPath(
             Environment.SpecialFolder.LocalApplicationData),
         "MathTutorApp",
         "leaderboard.csv");
        public LeaderboardForm()
        {
            InitializeComponent();

            ConfigureLeaderboard();
            btnClose.Click += btnClose_Click;
        }


        // ==========================================
        // INITIALIZE THE FORM AND ITS CONTROLS
        // ==========================================

        private void InitializeComponent()
        {
            lblLeaderboardTitle = new Label();
            dgvLeaderboard = new DataGridView();
            btnClose = new Button();

            ((ISupportInitialize)dgvLeaderboard).BeginInit();
            SuspendLayout();

            // 
            // lblLeaderboardTitle
            // 
            lblLeaderboardTitle.AutoSize = false;

            lblLeaderboardTitle.Font =
                new Font("Segoe UI", 16F, FontStyle.Bold);

            lblLeaderboardTitle.ForeColor =
                Color.FromArgb(108, 99, 255);

            lblLeaderboardTitle.TextAlign =
                ContentAlignment.MiddleCenter;

            lblLeaderboardTitle.Location = new Point(30, 15);
            lblLeaderboardTitle.Size = new Size(774, 35);

            lblLeaderboardTitle.Text = "🏆  MATH TUTOR LEADERBOARD";

            //
            // dgvLeaderboard
            //
            dgvLeaderboard.AllowUserToAddRows = false;
            dgvLeaderboard.AllowUserToDeleteRows = false;
            dgvLeaderboard.AllowUserToOrderColumns = false;
            dgvLeaderboard.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLeaderboard.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvLeaderboard.Location = new Point(30, 65);
            dgvLeaderboard.Name = "dgvLeaderboard";
            dgvLeaderboard.ReadOnly = true;
            dgvLeaderboard.RowHeadersVisible = false;
            dgvLeaderboard.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLeaderboard.MultiSelect = false;
            dgvLeaderboard.Size = new Size(774, 330);
            dgvLeaderboard.TabIndex = 1;

            //
            // btnClose
            //
            btnClose.BackColor = Color.FromArgb(108, 99, 255);
            btnClose.ForeColor = Color.White;

            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;

            btnClose.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            btnClose.Cursor = Cursors.Hand;

            btnClose.Size = new Size(110, 35);
            btnClose.Location = new Point(694, 410);
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;

            //
            // LeaderboardForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(834, 461);

            BackColor = Color.FromArgb(245, 243, 255);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Controls.Add(lblLeaderboardTitle);
            Controls.Add(dgvLeaderboard);
            Controls.Add(btnClose);

            MinimumSize = new Size(750, 400);
            Name = "LeaderboardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MathTutorApp - Leaderboard";

            ((ISupportInitialize)dgvLeaderboard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // ==========================================
        // CONFIGURE THE LEADERBOARD TABLE
        // ==========================================

        private void ConfigureLeaderboard()
        {
            // Clear existing rows and columns.
            dgvLeaderboard.Rows.Clear();
            dgvLeaderboard.Columns.Clear();

            // Table behaviour.
            dgvLeaderboard.ReadOnly = true;
            dgvLeaderboard.AllowUserToAddRows = false;
            dgvLeaderboard.AllowUserToDeleteRows = false;
            dgvLeaderboard.AllowUserToResizeRows = false;
            dgvLeaderboard.AllowUserToOrderColumns = false;

            dgvLeaderboard.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLeaderboard.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLeaderboard.MultiSelect = false;
            dgvLeaderboard.RowHeadersVisible = false;
            dgvLeaderboard.BorderStyle = BorderStyle.None;

            dgvLeaderboard.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvLeaderboard.BackgroundColor = Color.White;
            dgvLeaderboard.GridColor = Color.FromArgb(235, 233, 245);

            // Remove default Windows visual styling.
            dgvLeaderboard.EnableHeadersVisualStyles = false;

            // Column headings.
            dgvLeaderboard.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(108, 99, 255);

            dgvLeaderboard.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvLeaderboard.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(108, 99, 255);

            dgvLeaderboard.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvLeaderboard.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvLeaderboard.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvLeaderboard.ColumnHeadersHeight = 42;

            // Table rows.
            dgvLeaderboard.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Regular);

            dgvLeaderboard.DefaultCellStyle.ForeColor =
                Color.FromArgb(37, 35, 58);

            dgvLeaderboard.DefaultCellStyle.BackColor =
                Color.White;

            dgvLeaderboard.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(232, 229, 255);

            dgvLeaderboard.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(37, 35, 58);

            dgvLeaderboard.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvLeaderboard.RowTemplate.Height = 38;

            // Alternating row colours.
            dgvLeaderboard.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 247, 255);

            // IMPORTANT: Create columns BEFORE loading saved results.
            dgvLeaderboard.Columns.Add("Rank", "RANK");
            dgvLeaderboard.Columns.Add("PlayerName", "PLAYER");
            dgvLeaderboard.Columns.Add("Difficulty", "LEVEL");
            dgvLeaderboard.Columns.Add("BestScore", "BEST SCORE");

            dgvLeaderboard.Columns.Add(
                "QuestionsAttempted",
                "QUESTIONS");

            dgvLeaderboard.Columns.Add("Percentage", "ACCURACY");

            // Form appearance.
            BackColor = Color.FromArgb(245, 243, 255);

            // Create the summary cards.
            CreateSummaryCards();

            // Load saved leaderboard results only AFTER creating columns.
            LoadLeaderboard();

            // Prevent a row from appearing selected when the form opens.
            dgvLeaderboard.ClearSelection();
            dgvLeaderboard.CurrentCell = null;
        }

        // ==========================================
        // LOAD SAVED LEADERBOARD RESULTS
        // ==========================================

        private void LoadLeaderboard()
        {
            dgvLeaderboard.Rows.Clear();

            if (!File.Exists(leaderboardFile))
            {
                MessageBox.Show(
                    "No quiz results have been saved yet.\n\n" +
                    "Complete a quiz first to appear on the leaderboard.",
                    "Leaderboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                List<LeaderboardEntry> entries =
                    ReadLeaderboardFile();

                if (entries.Count == 0)
                {
                    MessageBox.Show(
                        "No valid leaderboard results were found.",
                        "Leaderboard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                string[] difficulties =
                {
                    "Easy",
                    "Medium",
                    "Hard"
                };

                foreach (string difficulty in difficulties)
                {
                    var results = entries
                        .Where(entry =>
                            string.Equals(
                                entry.Difficulty,
                                difficulty,
                                StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(entry => entry.BestScore)
                        .ThenByDescending(entry => entry.Percentage)
                        .ThenBy(entry => entry.PlayerName)
                        .Take(10)
                        .ToList();

                    int rank = 1;

                    foreach (LeaderboardEntry entry in results)
                    {
                        dgvLeaderboard.Rows.Add(
                            rank,
                            entry.PlayerName,
                            entry.Difficulty,
                            entry.BestScore,
                            entry.QuestionsAttempted,
                            entry.Percentage.ToString(
                                "F1",
                                CultureInfo.InvariantCulture) + "%");

                        rank++;
                    }
                }

                dgvLeaderboard.ClearSelection();
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "The saved leaderboard file could not be read.",
                    "Leaderboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "The application does not have permission to read " +
                    "the leaderboard file.",
                    "Leaderboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // READ THE SAVED CSV FILE
        // ==========================================

        private List<LeaderboardEntry> ReadLeaderboardFile()
        {
            List<LeaderboardEntry> entries =
                new List<LeaderboardEntry>();

            string[] lines = File.ReadAllLines(leaderboardFile);

            // Skip the CSV header.
            for (int i = 1; i < lines.Length; i++)
            {
                List<string> fields = ParseCsvLine(lines[i]);

                // Expected columns:
                // 0 PlayerName
                // 1 Difficulty
                // 2 BestScore
                // 3 QuestionsAttempted
                // 4 Percentage
                // 5 DatePlayed

                if (fields.Count < 6)
                {
                    continue;
                }

                if (!int.TryParse(
                    fields[2],
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out int bestScore))
                {
                    continue;
                }

                if (!int.TryParse(
                    fields[3],
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out int questionsAttempted))
                {
                    continue;
                }

                if (!double.TryParse(
                    fields[4],
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double percentage))
                {
                    continue;
                }

                entries.Add(new LeaderboardEntry
                {
                    PlayerName = fields[0],
                    Difficulty = fields[1],
                    BestScore = bestScore,
                    QuestionsAttempted = questionsAttempted,
                    Percentage = percentage
                });
            }

            return entries;
        }

        // ==========================================
        // PARSE CSV VALUES SAFELY
        // ==========================================

        private List<string> ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();

            StringBuilder currentField = new StringBuilder();

            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i];

                if (character == '"')
                {
                    if (insideQuotes &&
                        i + 1 < line.Length &&
                        line[i + 1] == '"')
                    {
                        currentField.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (character == ',' && !insideQuotes)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(character);
                }
            }

            fields.Add(currentField.ToString());

            return fields;
        }

        // ==========================================
        // LEADERBOARD ENTRY
        // ==========================================

        private class LeaderboardEntry
        {
            public string PlayerName { get; set; } = "";

            public string Difficulty { get; set; } = "";

            public int BestScore { get; set; }

            public int QuestionsAttempted { get; set; }

            public double Percentage { get; set; }
        }

        // ==========================================
        // CLOSE BUTTON
        // ==========================================

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }
        private void CreateSummaryCards()
        {
            Panel bestScoreCard = CreateCard(
                "BEST SCORE",
                "0",
                new Point(38, 92),
                Color.FromArgb(108, 99, 255),
                out lblBestScoreValue);

            Panel playersCard = CreateCard(
                "TOTAL PLAYERS",
                "0",
                new Point(288, 92),
                Color.FromArgb(45, 156, 219),
                out lblTotalPlayersValue);

            Panel accuracyCard = CreateCard(
                "TOP ACCURACY",
                "0.0%",
                new Point(538, 92),
                Color.FromArgb(34, 160, 107),
                out lblTopAccuracyValue);

            Controls.Add(bestScoreCard);
            Controls.Add(playersCard);
            Controls.Add(accuracyCard);
        }

        private Panel CreateCard(
            string title,
            string initialValue,
            Point location,
            Color accentColor,
            out Label valueLabel)
        {
            Panel card = new Panel
            {
                Location = location,
                Size = new Size(230, 100),
                BackColor = Color.White
            };

            Label titleLabel = new Label
            {
                Text = title,
                Location = new Point(14, 12),
                Size = new Size(205, 25),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(119, 117, 138),
                TextAlign = ContentAlignment.MiddleLeft
            };

            valueLabel = new Label
            {
                Text = initialValue,
                Location = new Point(14, 42),
                Size = new Size(205, 42),
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = accentColor,
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);

            return card;
        }
    }
}