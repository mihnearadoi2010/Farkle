using UnityEngine;

public class Die
{
    public int Value { get; private set; }

    public Die()
    {
        Roll();
    }

    public void Roll()
    {
        Value = Random.Range(1, 7);
    }
}
