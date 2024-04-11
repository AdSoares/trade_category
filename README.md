# UBS AG, .NET Developer Test

## Question 1

This solution was build using a console application with .NET 6.0, C# and Microsoft Visual Studio 2022.

This answer uses the Strategy design pattern to encapsulate the rules for trades risk calculation. This approach allows us to easily add, remove, or modify rules without impacting the rest of the code.

To add a new rule, you need a new class implementing ITradeCategoryStrategy interface and the rules for risk calculation that you desire. You also need put this new class in the list of strategies of PortfolioRiskClassifier class. If you need remove a rule of risk classification, only remove the respective class from the list of strategies of PortfolioRiskClassifier class.

This answer also uses the SOLID principles, particularly the Open/Closed Principle, by allowing the addition of new category rules without modifying existing code. Additionally, it uses object-oriented principles and design patterns to create a flexible and maintainable solution.

## Question 2

This SQL script was build using Microsoft SQL Server 2019 (Express Edition) on Windows Server 2016.

The script execute the following tasks:

- Drop tables to allow reacreate
- Create all tables need for solution
- Create stored procedures used by solution
- Execute all stored procedure to display result

To add a new rule, you need insert a new record into TradeRiskRule. If complexity increase, you need change the TradeRiskRule table schema and change the stored procedure ClassifyTradesRisk to implementing the new complexity.

To remove a rule, you only need delete the respective record from TradeRiskRule.
