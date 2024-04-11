using TradeCategory.Core.Domain;
using TradeCategory.Core.Utils;

namespace TradeCategory.Core.CategoryStrategy.CategoryRiskRules
{
    public class HighRiskTradeCategorizationStrategy : ITradeCategoryStrategy
    {
        public string? CategorizeTrade(ITrade trade)
        {
            const double VALUE_LIMIT = 1000000;

            if (trade.Value >= VALUE_LIMIT && trade.ClientSector?.ToUpper() == Constants.CLIENT_SECTOR_PRIVATE.ToUpper())
            {
                return Constants.HIGHRISK;
            }

            return null;
        }
    }
}
