using UnityEngine;

public class GameTable : MonoBehaviour
{
    public Table table { get; } = new();

    [SerializeField] private DiceGameObjectManager npc;
    [SerializeField] private UnityPlayer player;

    private void Start()
    {
        //npc.Setup(table.player2);
        player.Setup(this);
    }

    private void Update()
    {
        if (table.currentPlayer == table.player1)
        {
            player.gameObject.SetActive(true);
            npc.gameObject.SetActive(false);
        }
        if (table.currentPlayer == table.player2)
        {
            player.gameObject.SetActive(false);
            npc.gameObject.SetActive(true);
        }
    }

}
