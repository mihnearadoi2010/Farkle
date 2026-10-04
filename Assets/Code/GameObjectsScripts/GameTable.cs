using Unity.VisualScripting;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    private Table table { get; } = new();

    [SerializeField] private DiceGameObjectManager npc;
    [SerializeField] private UnityPlayer player;

    private void Start()
    {
        //npc.Setup(table.player2);
        player.diceGameObjectManager.Setup(table.player1);
    }

    private void PlayerScoredAndContinued(DieObject[] dice)
    {
        // since the player actually has to choose the dice it will use a delegate that calls this function so it can give the dice values and score and continue
    }

    private void PlayerScoredAndPassed(DieObject[] dice)
    {
        // since the player actually has to choose the dice it will use a delegate that calls this function so it can give the dice values and score and pass
        npc.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
    }

    private void NpcScoredAndContinued()
    {
        // delegates in npc script will call this so we can actually display what the npc is doing. this time we dont need to send in any dice sice thats already choosen by game logic. we have no player input
    }

    private void NpcScoredAndPassed()
    {
        // delegates in npc script will call this so we can actually display what the npc is doing. this time we dont need to send in any dice sice thats already choosen by game logic.we have no player input
        npc.gameObject.SetActive(false);
        player.gameObject.SetActive(true); // even if npc instantly does the calculations and choices we only what the player's dice to be active AFTER the animation of what the npc did are done
    }

}
