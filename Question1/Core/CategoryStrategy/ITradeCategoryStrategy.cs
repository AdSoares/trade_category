using TradeCategory.Core.Domain;

namespace TradeCategory.Core.CategoryStrategy
{
    public interface ITradeCategoryStrategy
    {
        string? CategorizeTrade(ITrade trade);
    }
}
