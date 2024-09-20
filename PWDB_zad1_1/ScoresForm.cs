using System;
using System.Windows.Forms;

public class ScoresForm : Form
{
    private DataGridView scoresGridView;

    public ScoresForm()
    {
        InitializeComponent();
        LoadScores();
    }

    private void InitializeComponent()
    {
        this.Text = "Tabela Wyników";
        this.Width = 600;
        this.Height = 400;

        scoresGridView = new DataGridView()
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnCount = 4
        };

        scoresGridView.Columns[0].Name = "Gracz";
        scoresGridView.Columns[1].Name = "Rozmiar Tablicy";
        scoresGridView.Columns[2].Name = "Ilość Ruchów";
        scoresGridView.Columns[3].Name = "Czas";

        this.Controls.Add(scoresGridView);
    }

    private void LoadScores()
    {
        scoresGridView.Rows.Clear();
        foreach (var score in ScoreManager.GetScores())
        {
            scoresGridView.Rows.Add(score.PlayerName, score.BoardSize, score.Moves, score.Time.ToString(@"hh\:mm\:ss"));
        }
    }
}
