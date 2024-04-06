public class DecrementHandsRemainingAction
{
    public int Amount { get; } = 1;
    public DecrementHandsRemainingAction() { }
    public DecrementHandsRemainingAction(int amount)
    {
        Amount = amount;
    }
}