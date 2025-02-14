using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ScavengerHuntMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadCsv(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string csvFilename = openFileDialog.FileName;
                if (CreateScavengerHunt(csvFilename))
                {
                    statusLabel.Text = "Scavenger Hunt CSV Created Successfully!";
                    MessageBox.Show("Scavenger Hunt CSV Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    statusLabel.Text = "Error: Duplicate answers found!";
                    MessageBox.Show("Error: Duplicate answers detected in the CSV!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CreateScavengerHunt(string csvFilename)
        {
            List<string> lines;

            try
            {
                using (var fs = new FileStream(csvFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(fs))
                {
                    lines = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        lines.Add(sr.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (lines.Count < 2)
            {
                MessageBox.Show("Error: CSV file must contain at least a header and one row of data!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Parse header to find column indexes
            string[] headers = lines[0].Split(',');
            int questionIndex = Array.IndexOf(headers, "Question");
            int answerIndex = Array.IndexOf(headers, "Answer");

            if (questionIndex == -1 || answerIndex == -1)
            {
                MessageBox.Show("Error: CSV must contain 'Question' and 'Answer' columns!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Parse data rows
            var data = lines.Skip(1)
                            .Select((line, index) => line.Split(','))
                            .Where(values => values.Length > Math.Max(questionIndex, answerIndex))
                            .Select((values, index) => new { Index = index + 1, Question = values[questionIndex], Answer = values[answerIndex] })
                            .ToList();

            var answerSet = new HashSet<string>();
            foreach (var item in data)
            {
                if (!answerSet.Add(item.Answer))
                {
                    MessageBox.Show($"Error: Duplicate answer found - {item.Answer}", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            var random = new Random();
            var shuffledData = data.OrderBy(_ => random.Next()).ToList();

            // Create scavenger hunt list
            var scavengerHunt = new List<(int QuestionNumber, string Question, string PreviousAnswer)>();

            for (int i = 0; i < shuffledData.Count; i++)
            {
                int questionNumber = shuffledData[i].Index;
                string question = shuffledData[i].Question;
                string previousAnswer = i == 0 ? shuffledData[^1].Answer : shuffledData[i - 1].Answer;

                scavengerHunt.Add((questionNumber, question, previousAnswer));
            }

            // Sort the list in ascending order by Question Number
            var sortedScavengerHunt = scavengerHunt.OrderBy(q => q.QuestionNumber).ToList();

            // Convert to CSV format
            var outputLines = new List<string> { "Question Number,Question,Answer Previous" };
            outputLines.AddRange(sortedScavengerHunt.Select(q => $"{q.QuestionNumber},{q.Question},{q.PreviousAnswer}"));

            try
            {
                string outputFilename = Path.Combine(Path.GetDirectoryName(csvFilename), Path.GetFileNameWithoutExtension(csvFilename) + "_ScavengerHunt.csv");
                File.WriteAllLines(outputFilename, outputLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }




    }
}
