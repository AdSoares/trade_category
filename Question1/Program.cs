using System.Text.Json;
using TradeCategory.Core.CategoryStrategy;
using TradeCategory.Core.Domain;

List<ITrade> portfolio = new List<ITrade>
{
    new Trade(2000000, "Private"),
    new Trade(400000, "Public"),
    new Trade(500000, "Public"),
    new Trade(3000000, "Public")
};

// Create a TradeCategorizer
var portfolioRiskClassifier = new PortfolioRiskClassifier();

// Classify trades risk
List<string> tradeCategories = portfolioRiskClassifier.ClassifyRisk(portfolio);

// Input Portfolio
var count = 1;
Console.WriteLine("Input - Porfolio:");

foreach (var trade in portfolio)
{
    Console.WriteLine($"Trade{count} {JsonSerializer.Serialize(trade)}");
    count++;
}

// Output Categories
Console.WriteLine();
Console.WriteLine("Output - Categories:");
Console.WriteLine($"tradeCategories = {JsonSerializer.Serialize(tradeCategories)}");