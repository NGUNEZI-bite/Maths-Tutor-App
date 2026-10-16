using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MathTutorApp
{
    public partial class cmbTime : Form
    {
        // ==========================================
        // RANDOM NUMBER GENERATOR
        // ==========================================

        private readonly Random random = new Random();

        // ==========================================
        // CURRENT QUESTION
        // ==========================================

        private int number1;
        private int number2;
        private int correctAnswer;

        private string currentOperation = "+";

        // ==========================================
        // QUIZ STATISTICS
        // ==========================================

        private int score;
        private int totalQuestions;
        private int highScore;

        private int timeLeft;

        private bool quizRunning;
        private bool quizStarted;

        // ==========================================
        // LEADERBOARD STORAGE
        // ==========================================

        private readonly string leaderboardFile =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MathTutorApp",
                "leaderboard.csv");

        private class QuizResult
        {
            public string PlayerName { get; set; } = "";

            public string Difficulty { get; set; } = "";

            public int BestScore { get; set; }

            public int QuestionsAttempted { get; set; }

            public double Percentage { get; set; }

            public DateTime DatePlayed { get; set; }
        }

        // ==========================================
        // CONSTRUCTOR
        // ==========================================

        public cmbTime()
        {
            InitializeComponent();

            // Configure timer.
            timer1.Interval = 1000;
            timer1.Stop();

            // Configure difficulty options.
            cmbDifficulty.Items.Clear();
            cmbDifficulty.Items.Add("Easy");
            cmbDifficulty.Items.Add("Medium");
            cmbDifficulty.Items.Add("Hard");

            cmbDifficulty.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbDifficulty.SelectedIndex = 0;

            // Configure time options if the dropdown is empty.
            if (cmbTimeLimit.Items.Count == 0)
            {
                cmbTimeLimit.Items.Add("10 Min");
                cmbTimeLimit.Items.Add("20 Min");
                cmbTimeLimit.Items.Add("30 Min");
                cmbTimeLimit.Items.Add("40 Min");
                cmbTimeLimit.Items.Add("50 Min");
                cmbTimeLimit.Items.Add("60 Min");
            }

            cmbTimeLimit.DropDownStyle =
                ComboBoxStyle.DropDownList;

            if (cmbTimeLimit.SelectedIndex < 0)
            {
                cmbTimeLimit.SelectedIndex = 0;
            }

            // Set initial control states.
            textBox1.Enabled = false;
            button1.Enabled = false;

            btnRestart.Enabled = true;

            lblFeedback.Text = "";
            lblScore.Text = "Score: 0/0";
            lblPercentage.Text = "0%";
            lblHighScore.Text = "High Score: 0";

            // Prevent duplicate event subscriptions.
            btnStart.Click -= btnStart_Click;
            btnStart.Click += btnStart_Click;

            btnRestart.Click -= btnRestart_Click;
            btnRestart.Click += btnRestart_Click;

            button1.Click -= button1_Click;
            button1.Click += button1_Click;

            timer1.Tick -= timer1_Tick;
            timer1.Tick += timer1_Tick;

            cmbDifficulty.SelectedIndexChanged -=
                cmbDifficulty_SelectedIndexChanged;

            cmbDifficulty.SelectedIndexChanged +=
                cmbDifficulty_SelectedIndexChanged;

            textBox1.KeyDown -= textBox1_KeyDown;
            textBox1.KeyDown += textBox1_KeyDown;

            btnLeaderboard.Click -= btnLeaderboard_Click;
            btnLeaderboard.Click += btnLeaderboard_Click;

            // Display the configured time without starting the quiz.
            timeLeft = GetSelectedTimeInSeconds();

            UpdateTimerDisplay();
            LoadHighScore();
        }

        // ==========================================
        // GET SELECTED QUIZ DURATION
        // ==========================================

        private int GetSelectedTimeInSeconds()
        {
            string selectedTime =
                cmbTimeLimit.SelectedItem?.ToString() ?? "10 Min";

            string numericPart = new string(
                selectedTime
                    .TakeWhile(char.IsDigit)
                    .ToArray());

            if (!int.TryParse(
                numericPart,
                out int selectedNumber))
            {
                selectedNumber = 10;
            }

            if (selectedNumber <= 0)
            {
                selectedNumber = 10;
            }

            // Items such as "10 Min" are interpreted as minutes.
            if (selectedTime.IndexOf(
                "min",
                StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return selectedNumber * 60;
            }

            // A numeric-only item is interpreted as seconds.
            return selectedNumber;
        }

        // ==========================================
        // START BUTTON
        // ==========================================

        private void btnStart_Click(
            object? sender,
            EventArgs e)
        {
            string playerName = txtPlayerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show(
                    "Please enter your name before starting the quiz.",
                    "Player Name Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPlayerName.Focus();
                return;
            }

            StartQuiz();
        }

        // ==========================================
        // START OR RESTART QUIZ
        // ==========================================

        private void StartQuiz()
        {
            string playerName = txtPlayerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show(
                    "Please enter your name first.",
                    "Player Name Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPlayerName.Focus();
                return;
            }

            timer1.Stop();

            score = 0;
            totalQuestions = 0;

            timeLeft = GetSelectedTimeInSeconds();

            quizRunning = true;
            quizStarted = true;

            textBox1.Clear();

            textBox1.Enabled = true;
            button1.Enabled = true;

            btnStart.Enabled = false;

            cmbDifficulty.Enabled = true;
            cmbTimeLimit.Enabled = false;
            txtPlayerName.Enabled = false;

            lblFeedback.Text = "";
            lblFeedback.ForeColor = Color.Black;

            LoadHighScore();

            UpdateScoreDisplay();
            UpdateTimerDisplay();

            GenerateQuestion();

            timer1.Start();

            textBox1.Focus();
        }

        // ==========================================
        // GENERATE MATH QUESTION
        // ==========================================

        private void GenerateQuestion()
        {
            if (!quizRunning)
            {
                return;
            }

            string difficulty =
                cmbDifficulty.SelectedItem?.ToString() ?? "Easy";

            switch (difficulty)
            {
                case "Easy":
                    number1 = random.Next(1, 21);
                    number2 = random.Next(1, 21);
                    break;

                case "Medium":
                    number1 = random.Next(10, 101);
                    number2 = random.Next(10, 101);
                    break;

                case "Hard":
                    number1 = random.Next(50, 501);
                    number2 = random.Next(50, 501);
                    break;

                default:
                    number1 = random.Next(1, 21);
                    number2 = random.Next(1, 21);
                    break;
            }

            int operation = random.Next(1, 5);

            switch (operation)
            {
                case 1:
                    currentOperation = "+";

                    correctAnswer = number1 + number2;
                    break;

                case 2:
                    currentOperation = "-";

                    // Keep subtraction answers non-negative.
                    if (number1 < number2)
                    {
                        int temporary = number1;

                        number1 = number2;
                        number2 = temporary;
                    }

                    correctAnswer = number1 - number2;
                    break;

                case 3:
                    currentOperation = "×";

                    correctAnswer = number1 * number2;
                    break;

                case 4:
                    currentOperation = "÷";

                    // Generate division questions with whole answers.
                    number2 = random.Next(1, 13);

                    correctAnswer = random.Next(1, 13);

                    number1 = number2 * correctAnswer;
                    break;
            }

            lblQuestion.Text =
                $"{number1} {currentOperation} {number2} = ?";

            textBox1.Clear();
        }

        // ==========================================
        // SUBMIT ANSWER
        // ==========================================

        private void button1_Click(
            object? sender,
            EventArgs e)
        {
            if (!quizRunning)
            {
                return;
            }

            if (!int.TryParse(
                textBox1.Text.Trim(),
                out int userAnswer))
            {
                lblFeedback.Text =
                    "Please enter a valid whole number.";

                lblFeedback.ForeColor = Color.Red;

                textBox1.Focus();
                return;
            }

            totalQuestions++;

            if (userAnswer == correctAnswer)
            {
                score++;

                lblFeedback.Text = "Correct! Well done.";

                lblFeedback.ForeColor = Color.Green;
            }
            else
            {
                lblFeedback.Text =
                    $"Incorrect. The correct answer is {correctAnswer}.";

                lblFeedback.ForeColor = Color.Red;
            }

            UpdateScoreDisplay();

            GenerateQuestion();

            textBox1.Focus();
        }

        // ==========================================
        // UPDATE SCORE DISPLAY
        // ==========================================

        private void UpdateScoreDisplay()
        {
            lblScore.Text = $"Score: {score}/{totalQuestions}";

            double percentage = totalQuestions > 0
                ? (double)score / totalQuestions * 100
                : 0;

            lblPercentage.Text = $"{percentage:F1}%";

            lblHighScore.Text = $"High Score: {highScore}";
        }

        // ==========================================
        // UPDATE TIMER DISPLAY
        // ==========================================

        private void UpdateTimerDisplay()
        {
            lblTimer.Text = $"Time Remaining: {timeLeft} seconds";

            if (timeLeft <= 10)
            {
                lblTimer.ForeColor = Color.Red;
            }
            else
            {
                lblTimer.ForeColor = Color.Black;
            }
        }

        // ==========================================
        // COUNTDOWN TIMER
        // ==========================================

        private void timer1_Tick(
            object? sender,
            EventArgs e)
        {
            if (!quizRunning)
            {
                return;
            }

            timeLeft--;

            UpdateTimerDisplay();

            if (timeLeft <= 0)
            {
                EndQuiz();
            }
        }

        // ==========================================
        // END QUIZ
        // ==========================================

        private void EndQuiz()
        {
            if (!quizRunning)
            {
                return;
            }

            quizRunning = false;

            timer1.Stop();

            textBox1.Enabled = false;
            button1.Enabled = false;

            btnStart.Enabled = true;

            cmbDifficulty.Enabled = true;
            cmbTimeLimit.Enabled = true;
            txtPlayerName.Enabled = true;

            lblFeedback.Text = "Time is up!";

            lblFeedback.ForeColor = Color.DarkRed;

            UpdateScoreDisplay();

            SaveQuizResult();

            double percentage = totalQuestions > 0
                ? (double)score / totalQuestions * 100
                : 0;

            MessageBox.Show(
                $"Quiz completed!\n\n" +
                $"Player: {txtPlayerName.Text.Trim()}\n" +
                $"Difficulty: {cmbDifficulty.Text}\n" +
                $"Correct answers: {score}\n" +
                $"Questions attempted: {totalQuestions}\n" +
                $"Final percentage: {percentage:F1}%\n" +
                $"Personal best: {highScore}",
                "MathTutorApp Results",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ==========================================
        // CHANGE DIFFICULTY
        // ==========================================

        private void cmbDifficulty_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            LoadHighScore();

            if (!quizRunning)
            {
                UpdateScoreDisplay();
                return;
            }

            lblFeedback.Text = "";

            GenerateQuestion();

            textBox1.Focus();
        }

        // ==========================================
        // RESTART BUTTON
        // ==========================================

        private void btnRestart_Click(
            object? sender,
            EventArgs e)
        {
            timer1.Stop();

            quizRunning = false;

            textBox1.Clear();

            textBox1.Enabled = false;
            button1.Enabled = false;

            btnStart.Enabled = true;

            cmbDifficulty.Enabled = true;
            cmbTimeLimit.Enabled = true;
            txtPlayerName.Enabled = true;

            score = 0;
            totalQuestions = 0;

            timeLeft = GetSelectedTimeInSeconds();

            lblFeedback.Text = "";
            lblFeedback.ForeColor = Color.Black;

            lblQuestion.Text = "Press Start Quiz to begin.";

            LoadHighScore();

            UpdateScoreDisplay();
            UpdateTimerDisplay();

            txtPlayerName.Focus();
        }

        // ==========================================
        // PRESS ENTER TO SUBMIT ANSWER
        // ==========================================

        private void textBox1_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (quizRunning)
                {
                    button1.PerformClick();
                }
            }
        }

        // ==========================================
        // SAVE QUIZ RESULT
        // ==========================================

        private void SaveQuizResult()
        {
            if (totalQuestions == 0)
            {
                return;
            }

            string playerName = txtPlayerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                return;
            }

            List<QuizResult> results = LoadLeaderboard();

            string difficulty =
                cmbDifficulty.SelectedItem?.ToString() ?? "Easy";

            QuizResult? existingResult = results.FirstOrDefault(
                result =>
                    string.Equals(
                        result.PlayerName,
                        playerName,
                        StringComparison.OrdinalIgnoreCase)
                    &&
                    string.Equals(
                        result.Difficulty,
                        difficulty,
                        StringComparison.OrdinalIgnoreCase));

            double percentage =
                (double)score / totalQuestions * 100;

            if (existingResult == null)
            {
                results.Add(new QuizResult
                {
                    PlayerName = playerName,
                    Difficulty = difficulty,
                    BestScore = score,
                    QuestionsAttempted = totalQuestions,
                    Percentage = percentage,
                    DatePlayed = DateTime.Now
                });
            }
            else if (score > existingResult.BestScore)
            {
                existingResult.BestScore = score;
                existingResult.QuestionsAttempted = totalQuestions;
                existingResult.Percentage = percentage;
                existingResult.DatePlayed = DateTime.Now;
            }
            else
            {
                // Keep the existing personal best.
                highScore = existingResult.BestScore;

                UpdateScoreDisplay();

                return;
            }

            try
            {
                string? directory =
                    Path.GetDirectoryName(leaderboardFile);

                if (directory != null)
                {
                    Directory.CreateDirectory(directory);
                }

                List<string> lines = new List<string>
                {
                    "PlayerName,Difficulty,BestScore,QuestionsAttempted,Percentage,DatePlayed"
                };

                foreach (QuizResult result in results)
                {
                    lines.Add(
                        $"{EscapeCsv(result.PlayerName)}," +
                        $"{EscapeCsv(result.Difficulty)}," +
                        $"{result.BestScore}," +
                        $"{result.QuestionsAttempted}," +
                        $"{result.Percentage.ToString(
                            CultureInfo.InvariantCulture)}," +
                        $"{result.DatePlayed.ToString(
                            "yyyy-MM-dd HH:mm:ss",
                            CultureInfo.InvariantCulture)}");
                }

                File.WriteAllLines(
                    leaderboardFile,
                    lines,
                    Encoding.UTF8);

                LoadHighScore();
                UpdateScoreDisplay();
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "The quiz result could not be saved to the leaderboard.",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "The application does not have permission to save the leaderboard.",
                    "Permission Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // ESCAPE CSV VALUES
        // ==========================================

        private string EscapeCsv(string value)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        // ==========================================
        // LOAD LEADERBOARD DATA
        // ==========================================

        private List<QuizResult> LoadLeaderboard()
        {
            List<QuizResult> results = new List<QuizResult>();

            try
            {
                if (!File.Exists(leaderboardFile))
                {
                    return results;
                }

                string[] lines = File.ReadAllLines(leaderboardFile);

                for (int i = 1; i < lines.Length; i++)
                {
                    List<string> fields = ParseCsvLine(lines[i]);

                    if (fields.Count < 6)
                    {
                        continue;
                    }

                    if (!int.TryParse(fields[2], out int bestScore))
                    {
                        continue;
                    }

                    if (!int.TryParse(
                        fields[3],
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

                    if (!DateTime.TryParse(
                        fields[5],
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime datePlayed))
                    {
                        datePlayed = DateTime.MinValue;
                    }

                    results.Add(new QuizResult
                    {
                        PlayerName = fields[0],
                        Difficulty = fields[1],
                        BestScore = bestScore,
                        QuestionsAttempted = questionsAttempted,
                        Percentage = percentage,
                        DatePlayed = datePlayed
                    });
                }
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "The leaderboard file could not be read.",
                    "Leaderboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return results;
        }

        // ==========================================
        // PARSE CSV LINE
        // ==========================================

        private List<string> ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();

            StringBuilder currentField = new StringBuilder();

            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char currentCharacter = line[i];

                if (currentCharacter == '"')
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
                else if (currentCharacter == ',' && !insideQuotes)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(currentCharacter);
                }
            }

            fields.Add(currentField.ToString());

            return fields;
        }

        // ==========================================
        // LOAD PLAYER'S PERSONAL BEST
        // ==========================================

        private void LoadHighScore()
        {
            string playerName = txtPlayerName.Text.Trim();

            string difficulty =
                cmbDifficulty.SelectedItem?.ToString() ?? "Easy";

            highScore = 0;

            if (!string.IsNullOrWhiteSpace(playerName))
            {
                List<QuizResult> results = LoadLeaderboard();

                QuizResult? result = results.FirstOrDefault(
                    item =>
                        string.Equals(
                            item.PlayerName,
                            playerName,
                            StringComparison.OrdinalIgnoreCase)
                        &&
                        string.Equals(
                            item.Difficulty,
                            difficulty,
                            StringComparison.OrdinalIgnoreCase));

                if (result != null)
                {
                    highScore = result.BestScore;
                }
            }

            lblHighScore.Text = $"High Score: {highScore}";
        }

        // ==========================================
        // VIEW LEADERBOARD BUTTON
        // ==========================================

        private void btnLeaderboard_Click(object? sender, EventArgs e)
        {
            using (LeaderboardForm leaderboardForm = new LeaderboardForm())
            {
                leaderboardForm.ShowDialog(this);
            }
        }

        // ==========================================
        // FORM CLOSING
        // ==========================================

        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            timer1.Stop();

            base.OnFormClosing(e);
        }

        // ==========================================
        // LEGACY DESIGNER EVENT
        // ==========================================
        // This empty method prevents a compile error if
        // the Designer still references the old event.
        // It can be removed later after disconnecting
        // that event in the Designer.

        private void comboBox1_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
        }
        private void label1_Click(object? sender, EventArgs e)
        {
        }

        private void cmbTime_Load(object sender, EventArgs e)
        {

        }

        private void cmbDifficulty_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}