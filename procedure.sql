use master;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'CategorizeTrades')
DROP PROCEDURE CategorizeTrades;
GO

CREATE PROCEDURE CategorizeTrades
(
	@Value as MONEY,
	@ClientSector as VARCHAR(MAX)
) 
AS
BEGIN
	DECLARE @TradeValue AS MONEY = 1000000;
	DECLARE @TradeCategoryDescription AS VARCHAR(MAX) = 'LOWRISK';
	DECLARE @TradeCategoryId AS INT = 1;

	SET @ClientSector = UPPER(@ClientSector);

	IF @Value > @TradeValue		
		IF @ClientSector = 'PUBLIC'		
			SET @TradeCategoryId = 2;
		IF @ClientSector = 'PRIVATE'
			SET @TradeCategoryId = 3;	
	
	INSERT INTO Trades (Value, ClientSector, TradeCategoryId) VALUES (@Value, @ClientSector, @TradeCategoryId);

	--Return message
	IF @TradeCategoryId > 1
		IF @TradeCategoryId = 2
			SET @TradeCategoryDescription = 'MEDIUMRISK'
		IF @TradeCategoryId = 3
			SET @TradeCategoryDescription = 'HIGHRISK'		

	PRINT CONVERT(VARCHAR, @Value) + ' - ' + @ClientSector + ' - '	+ @TradeCategoryDescription;
END;


/*
--Test cases:

exec CategorizeTrades @Value = 2000000, @ClientSector = 'Private';
exec CategorizeTrades @Value = 400000, @ClientSector = 'Public';
exec CategorizeTrades @Value = 500000, @ClientSector = 'Public';
exec CategorizeTrades @Value = 3000000, @ClientSector = 'Public';

*/