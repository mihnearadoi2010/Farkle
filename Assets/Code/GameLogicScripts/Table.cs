using System.Collections.Generic;
using System.Linq;

public class Table
{
    public Player player1 { get; private set; } = new Player();
    public Player player2 { get; private set; } = new Player();
    public Player currentPlayer { get; private set; }
    private ScoreCalculator scoreCalculator = new();

    public Table()
    {
        currentPlayer = player1;
        ThrowCurrentPlayerDice();
    }

    private void ThrowCurrentPlayerDice()
    {
        foreach (var die in currentPlayer.CurrentDice)
        {
            die.Roll();
        }
        CheckForBust();
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

        ThrowCurrentPlayerDice();
    }

    private void CheckForBust()
    {
        if (scoreCalculator.CalculateScore(currentPlayer.dice.ToList()) == 0)
        {
            currentPlayer.BustScore();

            if (currentPlayer == player1)
            {
                currentPlayer = player2;
            }
            else
            {
                currentPlayer = player1;
            }

            ThrowCurrentPlayerDice();
        }
    }
}
