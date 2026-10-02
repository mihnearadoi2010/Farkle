using System.Collections.Generic;
using System.Linq;

public class Player
{
    public int TotalScore { get; private set; }
    public int RoundScore { get; private set; }
    public int SelectedScore { get; private set; }

    private Die[] dice = new Die[6];
    public List<Die> CurrentDice { get; } // dice that haven't been scored
    public List<Die> selectedDice { get; }

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
