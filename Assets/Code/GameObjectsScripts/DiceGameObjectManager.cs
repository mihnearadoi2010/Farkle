using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameObjectManager : MonoBehaviour
{
    [SerializeField] private DieObject[] diceObjects = new DieObject[6]; // used sice 2d arrays dont work in inspector
    [SerializeField] private Sprite[] dieSprites = new Sprite[6];
    [SerializeField] private Sprite[] selectedDieSprites = new Sprite[6];
    public DieObject[,] dice { get; } = new DieObject[3, 2]; // actual dice array since there are 3 on the x and 2 on the y
    public List<DieObject> SelectedDice { get; set; } = new List<DieObject>();

    private Player player;
    public Vector2 dieHoveringOverCoords { get; set; }

    private void Update()
    {
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
    }

    public void Setup(Player player)
    {
        this.player = player;

        dice[0, 0] = diceObjects[0];
        dice[0, 0].Die = player.dice[0];

        dice[1, 0] = diceObjects[1];
        dice[1, 0].Die = player.dice[1];

        dice[2, 0] = diceObjects[2];
        dice[2, 0].Die = player.dice[2];

        dice[0, 1] = diceObjects[3];
        dice[0, 1].Die = player.dice[3];

        dice[1, 1] = diceObjects[4];
        dice[1, 1].Die = player.dice[4];

        dice[2, 1] = diceObjects[5];
        dice[2, 1].Die = player.dice[5];

        dieHoveringOverCoords = Vector2.zero;
        dice[(int)dieHoveringOverCoords.x, (int)dieHoveringOverCoords.y].IsHoveredOver = true;
    }
}
