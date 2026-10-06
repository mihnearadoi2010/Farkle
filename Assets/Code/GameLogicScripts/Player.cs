using System.Collections.Generic;
using System.Linq;

public class Player
{
    public int TotalScore { get; private set; }
    public int RoundScore { get; private set; }
    public int SelectedScore { get; set; }

    public Die[] dice { get; } = new Die[6];
    public List<Die> CurrentDice { get; set; } // dice that haven't been scored

    public Player()
    {
        for (int i = 0; i < dice.Length; i++ )
        {
            dice[i] = new Die();
        }

        CurrentDice = dice.ToList();
    }

    public void ScoreSelectedScore()
    {
        RoundScore += SelectedScore;
        SelectedScore = 0;
        if (CurrentDice.Count == 0)
        {
            CurrentDice = dice.ToList();
        }
    }

    public void ScoreRoundScore()
    {
        ScoreSelectedScore();
        TotalScore += RoundScore;
        RoundScore = 0;
        CurrentDice = dice.ToList();
    }

    public void BustScore()
    {
        SelectedScore = 0;
        RoundScore = 0;
        CurrentDice = dice.ToList();
    }
}
