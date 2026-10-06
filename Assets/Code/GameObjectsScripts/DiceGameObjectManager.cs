using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceGameObjectManager : MonoBehaviour
{
    [SerializeField] public DieObject[] diceObjects = new DieObject[6];
    [SerializeField] private Sprite[] dieSprites = new Sprite[6];
    [SerializeField] private Sprite[] selectedDieSprites = new Sprite[6];
    public List<DieObject> SelectedDice { get; set; } = new List<DieObject>();

    private Table table;
    public Player player { get; private set; }
    public DieObject HoveredDie { get; set; }

    private void Update()
    {
        if (table == null) { return; }
        for(int i = 0; i < diceObjects.Length; i++)
        {
            if (SelectedDice.Contains(diceObjects[i]))
            {
                continue;
            }
            diceObjects[i].ChangeSprite(dieSprites[diceObjects[i].Die.Value - 1]);
        }
        foreach(var die in SelectedDice)
        {
            die.ChangeSprite(selectedDieSprites[die.Die.Value - 1]);
        }
        for (int i = 0; i < diceObjects.Length; i++)
        {
            diceObjects[i].gameObject.SetActive(player.CurrentDice.Contains(diceObjects[i].Die));
        }
    }

    public void Setup(Table table, Player player)
    {
        this.table = table;
        this.player = player;

        for (int i = 0; i < diceObjects.Length; i++)
        {
            diceObjects[i].Die = player.dice[i];
        }

        SetFirstDiceHover();
    }

    public void SetFirstDiceHover() // after player scores choose a dice which he is hovering over after rolling the dice
    {
        if (HoveredDie != null)
        {
            HoveredDie.IsHoveredOver = false;
        }

        HoveredDie = diceObjects.Where(d => player.CurrentDice.Contains(d.Die) && !SelectedDice.Contains(d)).First();

        if (HoveredDie != null)
        {
            HoveredDie.IsHoveredOver = true;
        }
    }

    public void ScoreAndContinue()
    {
        var selectedDieList = new List<Die>();
        for (int i = 0; i < SelectedDice.Count; i++)
        {
            selectedDieList.Add(SelectedDice[i].Die);
        }
        if (table.scoreCalculator.CalculateScore(selectedDieList) == 0)
        {
            return;
        }

        table.scoreAndContinue(selectedDieList);
        SelectedDice = new List<DieObject>();
    }
}
