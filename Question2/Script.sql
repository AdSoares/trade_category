-- Drop all tables to allow recreate
IF OBJECT_ID('dbo.Trade', 'U') IS NOT NULL 
  DROP TABLE dbo.Trade; 

IF OBJECT_ID('dbo.TradeRiskRule', 'U') IS NOT NULL 
  DROP TABLE dbo.TradeRiskRule

IF OBJECT_ID('dbo.TradeCategory', 'U') IS NOT NULL 
  DROP TABLE dbo.TradeCategory

-- Create table to store trades
CREATE TABLE dbo.Trade (
    TradeId INT IDENTITY(1,1) PRIMARY KEY,
    Value DECIMAL(18, 2),
    ClientSector VARCHAR(50)
);

-- Create table to store risk classification rules
CREATE TABLE dbo.TradeRiskRule (
    TradeRiskRulesId INT IDENTITY(1,1) PRIMARY KEY,
    ValueStartInterval decimal(18, 2) NULL,
	  ValueFinishInterval decimal(18, 2) NULL,
	  ClientSectorExpected VARCHAR(50) NULL,
	  RiskCalculated VARCHAR(50) NOT NULL
);
GO

-- Create table to store trade categories (output)
CREATE TABLE dbo.TradeCategory (
    TradeId INT,
    Risk VARCHAR(50)
);
GO

-- Stored procedure to initialize table with data
CREATE OR ALTER PROCEDURE dbo.InitializeData
AS
BEGIN
    -- Insert sample trades
    INSERT INTO dbo.Trade ([Value], ClientSector) VALUES (2000000, 'Private');
    INSERT INTO dbo.Trade ([Value], ClientSector) VALUES (400000, 'Public');
    INSERT INTO dbo.Trade ([Value], ClientSector) VALUES (500000, 'Public');
    INSERT INTO dbo.Trade ([Value], ClientSector) VALUES (3000000, 'Public');

	-- Insert Trade Risk Rules
	INSERT INTO dbo.TradeRiskRule (ValueStartInterval, ValueFinishInterval, ClientSectorExpected, RiskCalculated) VALUES (null, 999999, 'Public', 'LOWRISK');
    INSERT INTO dbo.TradeRiskRule (ValueStartInterval, ValueFinishInterval, ClientSectorExpected, RiskCalculated) VALUES (1000000, null, 'Public', 'MEDIUMRISK');
    INSERT INTO dbo.TradeRiskRule (ValueStartInterval, ValueFinishInterval, ClientSectorExpected, RiskCalculated) VALUES (1000000, null, 'Private', 'HIGHRISK');
	
    -- Clear existing categorized trades
    TRUNCATE TABLE dbo.TradeCategory;
END;
GO

-- Stored procedure to categorize trades
CREATE OR ALTER PROCEDURE dbo.ClassifyTradesRisk
AS
BEGIN

	DECLARE @MAX_DECIMAL decimal(18,2)
	set @MAX_DECIMAL = 99999999999999.99

    -- Categorize trades based on rules
    INSERT INTO dbo.TradeCategory (TradeId, Risk)
    SELECT TradeId,
           RiskCalculated AS Risk
    FROM 
		dbo.Trade TRA
		LEFT JOIN dbo.TradeRiskRule TRR 
			ON TRA.[Value] >= ISNULL(TRR.ValueStartInterval, 0) AND
			   TRA.[Value] <= ISNULL(TRR.ValueFinishInterval, @MAX_DECIMAL) AND
			   UPPER(TRA.ClientSector) = UPPER(TRR.ClientSectorExpected);
END;
GO

-- Stored procedure to display trades risk result
CREATE OR ALTER PROCEDURE dbo.OutputTradesRiskResult
AS
BEGIN
	
	-- Display risks calculated by trade
	SELECT
		TRA.*,
		TRC.Risk
	FROM
		dbo.Trade TRA
		INNER JOIN dbo.TradeCategory TRC ON TRC.TradeId = TRA.TradeId

	--SELECT * FROM dbo.TradeRiskRule
END;
GO

-- Execute stored procedures to complete task
EXEC dbo.InitializeData
EXEC dbo.ClassifyTradesRisk
EXEC dbo.OutputTradesRiskResult