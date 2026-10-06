using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnityPlayer : MonoBehaviour
{
    public DiceGameObjectManager diceGameObjectManager { get; private set; }
    [SerializeField] private InputActionReference moveThroughDice;
    [SerializeField] private InputActionReference selectDie;
    [SerializeField] private InputActionReference scoreAndContinue;
    [SerializeField] private InputActionReference scoreAndPass;

    private float moveCooldown = 0.20f;
    float nextMoveTime;

    private void Awake()
    {
        diceGameObjectManager = GetComponent<DiceGameObjectManager>();
    }

    private void Update()
    {
        MoveThroughDice();
    }

    #region Enable/Disable
    private void OnEnable()
    {
        moveThroughDice.action.Enable();
        selectDie.action.Enable();
        scoreAndContinue.action.Enable();
        scoreAndPass.action.Enable();

        selectDie.action.performed += OnSelectDie;
        scoreAndContinue.action.performed += OnScoreAndContinue;
    }

    private void OnDisable()
    {
        moveThroughDice.action.Disable();
        selectDie.action.Disable();
        scoreAndContinue.action.Disable();
        scoreAndPass.action.Disable();

        selectDie.action.performed -= OnSelectDie;
        scoreAndContinue.action.performed -= OnScoreAndContinue;
    }
    #endregion

    public void Setup(GameTable gameTable)
    {
        diceGameObjectManager.Setup(gameTable.table, gameTable.table.player1);
    }

    private void MoveThroughDice()
    {
        Vector2 moveDir = Vector2Int.RoundToInt(moveThroughDice.action.ReadValue<Vector2>());
        if (moveDir == Vector2Int.zero)
        {
            nextMoveTime = 0;
            return;
        }
        if (Time.time <= nextMoveTime)
        {
            return;
        }

        DieObject bestInLine = null;
        DieObject bestAlternative = null;

        float bestInLineDist = float.MaxValue;
        float bestAlternativeScore = float.MaxValue;
        Vector2 startingPosition = diceGameObjectManager.HoveredDie.transform.position;

        float lineTolerance = 0.2f; // changes how much is considered in line

        foreach (var die in diceGameObjectManager.diceObjects)
        {
            if (die == diceGameObjectManager.HoveredDie) { continue; }
            if (!diceGameObjectManager.player.CurrentDice.Contains(die.Die)) { continue; }

            Vector2 direction = (Vector2)die.transform.position - startingPosition;

            var dotProduct = Vector2.Dot(direction, moveDir);
            if (dotProduct <= 0) { continue; }

            var crossProduct = Mathf.Abs(direction.x * moveDir.y - moveDir.x * direction.y); // weird math shit that says how much the dice in the pressed direction
            
            if (crossProduct < lineTolerance)
            {
                if (dotProduct < bestInLineDist) { bestInLineDist = dotProduct; bestInLine = die; }
            }
            else
            {
                var score = dotProduct + 2f * crossProduct;
                if (score < bestAlternativeScore)
                {
                    bestAlternativeScore = score;
                    bestAlternative = die;
                }
            }
        }

        var bestDie = bestInLine != null ? bestInLine : bestAlternative;
        if (bestDie == null) { return; }

        diceGameObjectManager.HoveredDie.IsHoveredOver = false;
        diceGameObjectManager.HoveredDie = bestDie;
        bestDie.IsHoveredOver = true;
        nextMoveTime = Time.time + moveCooldown;
    }

    private void OnSelectDie(InputAction.CallbackContext ctx)
    {
        if (!diceGameObjectManager.HoveredDie.IsSelected)
        {
            diceGameObjectManager.SelectedDice.Add(diceGameObjectManager.HoveredDie);
        }
        else
        {
            diceGameObjectManager.SelectedDice.Remove(diceGameObjectManager.HoveredDie);
        }
        diceGameObjectManager.HoveredDie.IsSelected = !diceGameObjectManager.HoveredDie.IsSelected;
    }
    private void OnScoreAndContinue(InputAction.CallbackContext ctx)
    {
        if (diceGameObjectManager.SelectedDice.Contains(diceGameObjectManager.HoveredDie))
        {
            diceGameObjectManager.SetFirstDiceHover();
        }

        diceGameObjectManager.ScoreAndContinue();
    }
    
}
