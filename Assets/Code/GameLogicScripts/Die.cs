using System;

public class Die
{
    private static readonly Random random = new Random();
    public int Value { get; private set; }

    public Die()
    {
        Roll();
    }

    public void Roll()
    {
        Value = random.Next(1, 7);
    }
}
