using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private int CalculateScore(List<Die> selectedDice)
    {
        if (selectedDice.Count == 0 || selectedDice == null)
        {
            return 0;
        }

        List<Die> scoredDice = new(); // add dice here so we don't score them twice
        int selectedScore = 0;
    }

    private int GetStraightScore(List<Die> selectedDice, List<Die> scoredDice)
    {
        if (scoredDice.Count >= 1) { return 0; }

        var die1 = selectedDice.FirstOrDefault(d => d.Value == 1);
        var die2 = selectedDice.FirstOrDefault(d => d.Value == 2);
        var die3 = selectedDice.FirstOrDefault(d => d.Value == 3);
        var die4 = selectedDice.FirstOrDefault(d => d.Value == 4);
        var die5 = selectedDice.FirstOrDefault(d => d.Value == 5);
        var die6 = selectedDice.FirstOrDefault(d => d.Value == 6);


        if (die1 != null && die2 != null && die3 != null && die4 != null && die5 != null && die6 != null)
        {
            scoredDice.Add(die1);
            scoredDice.Add(die2);
            scoredDice.Add(die3);
            scoredDice.Add(die4);
            scoredDice.Add(die5);
            scoredDice.Add(die6);

            return 1500;
        }
        return 0;
    }

    private int GetSmallStraightScore1_5(List<Die> selectedDice, List<Die> scoredDice)
    {
        if (scoredDice.Count >= 2) { return 0; }

        var die1 = selectedDice.FirstOrDefault(d => d.Value == 1);
        var die2 = selectedDice.FirstOrDefault(d => d.Value == 2);
        var die3 = selectedDice.FirstOrDefault(d => d.Value == 3);
        var die4 = selectedDice.FirstOrDefault(d => d.Value == 4);
        var die5 = selectedDice.FirstOrDefault(d => d.Value == 5);

        if (die1 != null && die2 != null && die3 != null && die4 != null && die5 != null)
        {
            scoredDice.Add(die1);
            scoredDice.Add(die2);
            scoredDice.Add(die3);
            scoredDice.Add(die4);
            scoredDice.Add(die5);

            return 500;
        }
        return 0;
    }

    private int GetSmallStraightScore2_6(List<Die> selectedDice, List<Die> scoredDice)
    {
        if (scoredDice.Count >= 2) { return 0; }

        var die2 = selectedDice.FirstOrDefault(d => d.Value == 2);
        var die3 = selectedDice.FirstOrDefault(d => d.Value == 3);
        var die4 = selectedDice.FirstOrDefault(d => d.Value == 4);
        var die5 = selectedDice.FirstOrDefault(d => d.Value == 5);
        var die6 = selectedDice.FirstOrDefault(d => d.Value == 6);

        if (die2 != null && die3 != null && die4 != null && die5 != null && die6 != null)
        {
            scoredDice.Add(die2);
            scoredDice.Add(die3);
            scoredDice.Add(die4);
            scoredDice.Add(die5);
            scoredDice.Add(die6);

            return 750;
        }
        return 0;
    }

    private int MoreOfAKind(List<Die> selectedDice, List<Die> scoredDice)
    {
        if (scoredDice.Count >= 4) { return 0; }





        return 0;
    }



    
}
