using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class Player
{
    public int TotalScore { get; private set; }
    public int RoundScore { get; private set; }
    public int SelectedScore { get; set; }

    public Die[] dice { get; } = new Die[6];
    public List<Die> CurrentDice { get; set; } // dice that haven't been scored

    public Player()
    {
        CurrentDice = dice.ToList();
    }

    public void ScoreSelectedScore()
    {
        RoundScore += SelectedScore;
        SelectedScore = 0;
    }

    public void ScoreRoundScore()
    {
        ScoreSelectedScore();
        TotalScore += RoundScore;
        RoundScore = 0;
    }
}
