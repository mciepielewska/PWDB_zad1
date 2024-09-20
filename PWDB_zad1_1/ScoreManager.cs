using System;
using System.Collections.Generic;

public static class ScoreManager
{
    private static List<ScoreEntry> scores = new List<ScoreEntry>();

    public static void AddScore(int boardSize, int moves, TimeSpan time)
    {
        int playerNumber = scores.Count + 1;
        scores.Add(new ScoreEntry
        {
            PlayerName = $"Gracz {playerNumber}",
            BoardSize = boardSize,
            Moves = moves,
            Time = time
        });
    }

    public static IEnumerable<ScoreEntry> GetScores()
    {
        return scores;
    }
}

public class ScoreEntry
{
    public string PlayerName { get; set; }
    public int BoardSize { get; set; }
    public int Moves { get; set; }
    public TimeSpan Time { get; set; }
}
