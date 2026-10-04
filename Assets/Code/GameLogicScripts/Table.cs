using System.Collections.Generic;
using System.Linq;

public class Table
{
    private Player player1 = new Player();
    private Player player2 = new Player();
    private Player currentPlayer;
    private ScoreCalculator scoreCalculator;

    public Table()
    {
        currentPlayer = player1;
    }

    private void ThrowCurrentPlayerDice()
    {
        foreach (var die in currentPlayer.CurrentDice)
        {
            die.Roll();
        }
    }

    public void scoreAndContinue(List<Die> selectedDice)
    {
        var score = scoreCalculator.CalculateScore(selectedDice);
        if (score == 0) { return; }

        currentPlayer.CurrentDice = currentPlayer.CurrentDice.Where(d => !selectedDice.Contains(d)).ToList();
        if (currentPlayer.CurrentDice.Count == 0)
        {
            currentPlayer.CurrentDice = currentPlayer.dice.ToList();
        }
        ThrowCurrentPlayerDice();

        currentPlayer.SelectedScore = score;
        currentPlayer.ScoreSelectedScore();
    }

    public void scoreAndPass(List<Die> selectedDice)
    {
        var score = scoreCalculator.CalculateScore(selectedDice);
        if (score == 0) { return; }

        currentPlayer.CurrentDice = currentPlayer.dice.ToList();
        
        ThrowCurrentPlayerDice();

        currentPlayer.SelectedScore = score;
        currentPlayer.ScoreRoundScore();

        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else
        {
            currentPlayer = player1;
        }
    }

}
