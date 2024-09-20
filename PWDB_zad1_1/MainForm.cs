using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MainForm : Form
{
    private GameLogic game;
    private Button[,] buttons;
    private int boardSize;
    private Stopwatch stopwatch;

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "ZnajdŸ Pary";
        this.Width = 800;
        this.Height = 800;

        Label label = new Label() { Text = "Podaj rozmiar planszy (n):", Top = 10, Left = 10, Width = 200 };
        TextBox textBox = new TextBox() { Top = 40, Left = 10, Width = 200 };
        Button startButton = new Button() { Text = "Start", Top = 70, Left = 10 };

        startButton.Click += (sender, e) =>
        {
            int size;
            if (int.TryParse(textBox.Text, out size) && size > 1)
            {
                boardSize = size;
                game = new GameLogic(boardSize);
                InitializeBoard();
                stopwatch = Stopwatch.StartNew();
            }
            else
            {
                MessageBox.Show("Podaj prawid³owy rozmiar planszy.");
            }
        };

        this.Controls.Add(label);
        this.Controls.Add(textBox);
        this.Controls.Add(startButton);
    }

    private void InitializeBoard()
    {
        if (buttons != null)
        {
            foreach (Button button in buttons)
            {
                this.Controls.Remove(button);
            }
        }

        buttons = new Button[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                buttons[i, j] = new Button()
                {
                    Width = 50,
                    Height = 50,
                    Top = i * 50 + 100,
                    Left = j * 50 + 10,
                    Tag = new Tuple<int, int>(i, j)
                };
                buttons[i, j].Click += new EventHandler(OnCardClick);
                this.Controls.Add(buttons[i, j]);
            }
        }
    }

    private async void OnCardClick(object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;
        Tuple<int, int> position = clickedButton.Tag as Tuple<int, int>;

        if (game.RevealCard(position.Item1, position.Item2))
        {
            clickedButton.Text = game.Board[position.Item1, position.Item2].ToString();
            clickedButton.Enabled = false;

            if (!game.CanReveal)
            {
                await Task.Delay(1000);
                game.ResetMismatchedCards();
                UpdateBoard();
            }

            if (game.GameWon)
            {
                stopwatch.Stop();
                MessageBox.Show($"Gratulacje! Wygra³eœ w czasie: {stopwatch.Elapsed} z liczb¹ ruchów: {game.Moves}");
                ScoreManager.AddScore(boardSize, game.Moves, stopwatch.Elapsed);
                this.Close();
            }
        }
    }

    private void UpdateBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (game.Revealed[i, j])
                {
                    buttons[i, j].Text = game.Board[i, j].ToString();
                    buttons[i, j].Enabled = false;
                }
                else
                {
                    buttons[i, j].Text = "";
                    buttons[i, j].Enabled = true;
                }
            }
        }
    }
}
