public interface IDrawStrategy<T> where T : ICard
{
    List<T> DrawCards(int count);
}