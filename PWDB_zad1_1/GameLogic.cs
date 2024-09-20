using System;
using System.Collections.Generic;
using System.Linq;

public class GameLogic
{
    public int Size { get; private set; }
    public int[,] Board { get; private set; }
    public bool[,] Revealed { get; private set; }
    private List<int> cardValues;
    public int Moves { get; private set; }
    private int firstCardRow;
    private int firstCardCol;
    private int secondCardRow;
    private int secondCardCol;
    private bool firstCardSelected;
    public bool GameWon { get; private set; }
    public bool CanReveal { get; private set; }

    public GameLogic(int size)
    {
        Size = size;
        Board = new int[size, size];
        Revealed = new bool[size, size];
        cardValues = new List<int>();
        Moves = 0;
        firstCardRow = -1;
        firstCardCol = -1;
        secondCardRow = -1;
        secondCardCol = -1;
        firstCardSelected = false;
        GameWon = false;
        CanReveal = true;
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        Random rand = new Random();
        int numberOfPairs = (Size * Size) / 2;

        for (int i = 0; i < numberOfPairs; i++)
        {
            cardValues.Add(i);
            cardValues.Add(i);
        }

        // Mieszanie kart
        cardValues = cardValues.OrderBy(x => rand.Next()).ToList();

        int index = 0;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (index < cardValues.Count)
                {
                    Board[i, j] = cardValues[index];
                    index++;
                }
                else
                {
                    Board[i, j] = -1;
                }
            }
        }
    }

    public bool RevealCard(int row, int col)
    {
        if (Revealed[row, col] || !CanReveal)
            return false;

        if (!firstCardSelected)
        {
            firstCardRow = row;
            firstCardCol = col;
            Revealed[row, col] = true;
            firstCardSelected = true;
        }
        else
        {
            secondCardRow = row;
            secondCardCol = col;
            Revealed[row, col] = true;
            Moves++;
            CanReveal = false;

            if (Board[firstCardRow, firstCardCol] == Board[secondCardRow, secondCardCol])
            {
                CanReveal = true;
                firstCardSelected = false;
                CheckWinCondition();
            }
        }
        return true;
    }

    public void ResetMismatchedCards()
    {
        if (Board[firstCardRow, firstCardCol] != Board[secondCardRow, secondCardCol])
        {
            Revealed[firstCardRow, firstCardCol] = false;
            Revealed[secondCardRow, secondCardCol] = false;
        }
        CanReveal = true;
        firstCardSelected = false;
    }

    private void CheckWinCondition()
    {
        GameWon = Revealed.Cast<bool>().All(revealed => revealed || Board[Array.IndexOf(Revealed.Cast<bool>().ToArray(), revealed) / Size, Array.IndexOf(Revealed.Cast<bool>().ToArray(), revealed) % Size] == -1);
    }
}
