public class IncrementRoundAction
{
    public int Amount { get; } = 1;
    public IncrementRoundAction() { }
    public IncrementRoundAction(int amount)
    {
        Amount = amount;
    }
}