using BlazorCardGame.Domain.Models;
namespace BlazorCardGame.Domain.Patterns.Strategy.DrawStrategy
{
    public interface IDrawStrategy<T> where T : ICard
    {
        List<T> DrawCards(int count);
    }
}