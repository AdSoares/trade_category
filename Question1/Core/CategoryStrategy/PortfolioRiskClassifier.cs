using TradeCategory.Core.CategoryStrategy.CategoryRiskRules;
using TradeCategory.Core.Domain;
using TradeCategory.Core.Utils;

namespace TradeCategory.Core.CategoryStrategy
{
    public class PortfolioRiskClassifier
    {
        private readonly List<ITradeCategoryStrategy> _strategies;

        public PortfolioRiskClassifier()
        {
            // Initialize with all category rules that need be verified
            _strategies = new List<ITradeCategoryStrategy>
            {
                new LowRiskTradeCategorizationStrategy(),
                new MediumRiskTradeCategorizationStrategy(),
                new HighRiskTradeCategorizationStrategy()
            };
        }

        public void AddCategorizationStrategy(ITradeCategoryStrategy strategy)
        {
            _strategies.Add(strategy);
        }

        public void RemoveCategorizationStrategy(ITradeCategoryStrategy strategy)
        {
            _strategies.Remove(strategy);
        }

        public List<string> ClassifyRisk(List<ITrade> portfolio)
        {
            var tradeCategories = new List<string>();

            foreach (var trade in portfolio)
            {
                bool categorized = false;

                foreach (var strategy in _strategies)
                {
                    var category = strategy.CategorizeTrade(trade);
                    if (category != null)
                    {
                        tradeCategories.Add(category);
                        categorized = true;
                        break;
                    }
                }

                if (!categorized)
                {
                    tradeCategories.Add(Constants.CATEGORY_RULE_NOT_FOUND);
                }
            }

            return tradeCategories;
        }
    }
}
