public class IncrementAnteAction
{
    public int Amount { get; } = 1;
    public IncrementAnteAction() { }
    public IncrementAnteAction(int amount)
    {
        Amount = amount;
    }
}