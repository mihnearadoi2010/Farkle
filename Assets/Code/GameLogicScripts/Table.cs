using System.Collections.Generic;
using System.Linq;

public class Table
{
    public Player player1 { get; private set; } = new Player();
    public Player player2 { get; private set; } = new Player();
    public Player currentPlayer { get; private set; }
    public ScoreCalculator scoreCalculator { get; } = new();

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

        currentPlayer.SelectedScore = score;
        currentPlayer.ScoreSelectedScore();

        ThrowCurrentPlayerDice();
    }

    public void scoreAndPass(List<Die> selectedDice)
    {
        var score = scoreCalculator.CalculateScore(selectedDice);
        if (score == 0) { return; }

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
        if (scoreCalculator.HasAnyScoringDice(currentPlayer.CurrentDice))
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
