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
    }

    private void OnDisable()
    {
        moveThroughDice.action.Disable();
        selectDie.action.Disable();
        scoreAndContinue.action.Disable();
        scoreAndPass.action.Disable();

        selectDie.action.performed -= OnSelectDie;
    }
    #endregion

    private void MoveThroughDice()
    {
        Vector2 moveDir = Vector2Int.RoundToInt(moveThroughDice.action.ReadValue<Vector2>());
        if (moveDir == Vector2Int.zero)
        {
            nextMoveTime = 0;
        }

        if (diceGameObjectManager.dieHoveringOverCoords.x + moveDir.x > diceGameObjectManager.dice.GetLength(0) - 1
            || diceGameObjectManager.dieHoveringOverCoords.x + moveDir.x < 0 
            || diceGameObjectManager.dieHoveringOverCoords.y - moveDir.y > diceGameObjectManager.dice.GetLength(1) -1
            || diceGameObjectManager.dieHoveringOverCoords.y - moveDir.y < 0)
        {
            return;
        }

        if (moveDir != Vector2Int.zero && Time.time >= nextMoveTime)
        {
            diceGameObjectManager.dice[(int)diceGameObjectManager.dieHoveringOverCoords.x, (int)diceGameObjectManager.dieHoveringOverCoords.y].IsHoveredOver = false;
            diceGameObjectManager.dieHoveringOverCoords = new Vector2(diceGameObjectManager.dieHoveringOverCoords.x + moveDir.x, diceGameObjectManager.dieHoveringOverCoords.y - moveDir.y);
            diceGameObjectManager.dice[(int)diceGameObjectManager.dieHoveringOverCoords.x, (int)diceGameObjectManager.dieHoveringOverCoords.y].IsHoveredOver = true;

            nextMoveTime = Time.time + moveCooldown;
        }
    }

    private void OnSelectDie(InputAction.CallbackContext ctx)
    {
        var hoveredDie = diceGameObjectManager.dice[(int)diceGameObjectManager.dieHoveringOverCoords.x, (int)diceGameObjectManager.dieHoveringOverCoords.y];

        if (!hoveredDie.IsSelected)
        {
            diceGameObjectManager.SelectedDice.Add(hoveredDie);
        }
        else
        {
            diceGameObjectManager.SelectedDice.Remove(hoveredDie);
        }
        hoveredDie.IsSelected = !hoveredDie.IsSelected;
    }
}
