using System;
using System.Windows.Forms;

public class MainMenuForm : Form
{
    private Button newGameButton;
    private Button viewScoresButton;

    public MainMenuForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Menu Główne";
        this.Width = 300;
        this.Height = 200;

        newGameButton = new Button() { Text = "Nowy Gracz", Top = 50, Left = 50, Width = 200 };
        viewScoresButton = new Button() { Text = "Tabela Wyników", Top = 100, Left = 50, Width = 200 };

        newGameButton.Click += (sender, e) =>
        {
            Form mainForm = new MainForm();
            mainForm.Show();
        };

        viewScoresButton.Click += (sender, e) =>
        {
            Form scoresForm = new ScoresForm();
            scoresForm.Show();
        };

        this.Controls.Add(newGameButton);
        this.Controls.Add(viewScoresButton);
    }
}
