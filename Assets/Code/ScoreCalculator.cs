using System.Collections.Generic;
using System.Linq;
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
        selectedScore += GetStraightScore(selectedDice, scoredDice);
        selectedScore += GetSmallStraightScore1_5(selectedDice, scoredDice);
        selectedScore += GetSmallStraightScore2_6(selectedDice, scoredDice);
        selectedScore += GetMoreOfAKindScore(selectedDice, scoredDice);
        selectedScore += GetOnesScore(selectedDice, scoredDice);
        selectedScore += GetFivesScore(selectedDice, scoredDice);

        if (scoredDice.Count != selectedDice.Count)
        {
            selectedScore = 0;
        }
        return selectedScore;
    }

    private int GetStraightScore(List<Die> selectedDice, List<Die> scoredDice)
    {
        if (scoredDice.Count > 0) { return 0; }

        var die1 = selectedDice.FirstOrDefault(d => d.Value == 1);
        var die2 = selectedDice.FirstOrDefault(d => d.Value == 2);
        var die3 = selectedDice.FirstOrDefault(d => d.Value == 3);
        var die4 = selectedDice.FirstOrDefault(d => d.Value == 4);
        var die5 = selectedDice.FirstOrDefault(d => d.Value == 5);
        var die6 = selectedDice.FirstOrDefault(d => d.Value == 6);

        if (scoredDice.Contains(die1) || scoredDice.Contains(die2) || scoredDice.Contains(die3) || scoredDice.Contains(die4) || scoredDice.Contains(die5) || scoredDice.Contains(die6))
        {
            return 0;
        }

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
        if (scoredDice.Count > 1) { return 0; }

        var die1 = selectedDice.FirstOrDefault(d => d.Value == 1);
        var die2 = selectedDice.FirstOrDefault(d => d.Value == 2);
        var die3 = selectedDice.FirstOrDefault(d => d.Value == 3);
        var die4 = selectedDice.FirstOrDefault(d => d.Value == 4);
        var die5 = selectedDice.FirstOrDefault(d => d.Value == 5);

        if (scoredDice.Contains(die1) || scoredDice.Contains(die2) || scoredDice.Contains(die3) || scoredDice.Contains(die4) || scoredDice.Contains(die5))
        {
            return 0;
        }

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
        if (scoredDice.Count > 1) { return 0; }

        var die2 = selectedDice.FirstOrDefault(d => d.Value == 2);
        var die3 = selectedDice.FirstOrDefault(d => d.Value == 3);
        var die4 = selectedDice.FirstOrDefault(d => d.Value == 4);
        var die5 = selectedDice.FirstOrDefault(d => d.Value == 5);
        var die6 = selectedDice.FirstOrDefault(d => d.Value == 6);

        if (scoredDice.Contains(die2) || scoredDice.Contains(die3) || scoredDice.Contains(die4) || scoredDice.Contains(die5) || scoredDice.Contains(die6))
        {
            return 0;
        }

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

    private int GetMoreOfAKindScore(List<Die> selectedDice, List<Die> scoredDice)
    {
        if (scoredDice.Count > 3) { return 0; }
        var totalScore = 0;

        for (int i = 0; i <= 6; i++)
        {
            if (scoredDice.Count > 3) { break; }
            List<Die> sameValue = selectedDice.Where(d => d.Value == i).Where (d => !scoredDice.Contains(d)).ToList();

            if (sameValue.Count < 3)
            {
                continue;
            }
            else
            {
                var multiplierPower = sameValue.Count - 3;
                int valueMultiplier = (int)Mathf.Pow(2, multiplierPower);


                if (sameValue[0].Value == 1)
                {
                    totalScore += sameValue[0].Value * 1000 * valueMultiplier;
                }
                else
                {
                    totalScore += sameValue[0].Value * 100 * valueMultiplier;
                }

                foreach (var d in sameValue)
                {
                    scoredDice.Add(d);
                }

            }
        }
        return totalScore;
    }

    private int GetOnesScore(List<Die> selectedDice, List<Die> scoredDice)
    {
        var totalScore = 0;

        foreach (var d in selectedDice.Where(die => !scoredDice.Contains(die)))
        {
            if (d.Value == 1)
            {
                totalScore += 100;
                scoredDice.Add(d);
            }
        }
        return totalScore;
    }

    private int GetFivesScore(List<Die> selectedDice, List<Die> scoredDice)
    {
        var totalScore = 0;

        foreach (var d in selectedDice.Where(die => !scoredDice.Contains(die)))
        {
            if (d.Value == 5)
            {
                totalScore += 50;
                scoredDice.Add(d);
            }
        }
        return totalScore;
    }

}
