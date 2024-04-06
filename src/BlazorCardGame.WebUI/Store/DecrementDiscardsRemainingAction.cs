public class DecrementDiscardsRemainingAction
{
    public int Amount { get; } = 1;
    public DecrementDiscardsRemainingAction() { }
    public DecrementDiscardsRemainingAction(int amount)
    {
        Amount = amount;
    }
}