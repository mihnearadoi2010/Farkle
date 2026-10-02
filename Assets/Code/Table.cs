
using NUnit.Framework;
using System.Collections.Generic;

public class Table
{
    private Player player1 = new Player();
    private Player player2 = new Player();
    private Player currentPlayer;

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

    private void scoreAndContinue(List<Die> selectedDice)
    {
        
    }


}
