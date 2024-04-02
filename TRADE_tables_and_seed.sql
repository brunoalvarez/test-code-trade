
CREATE TABLE TradeCategories
(
    Id INT PRIMARY KEY,
    Category VARCHAR(50)    
);

--initial seed TradeCategories
INSERT INTO TradeCategories (Id, Category) VALUES (1, 'LOWRISK')
INSERT INTO TradeCategories (Id, Category) VALUES (2, 'MEDIUMRISK')
INSERT INTO TradeCategories (Id, Category) VALUES (3, 'HIGHRISK')

CREATE TABLE Trades
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Value MONEY,
    ClientSector VARCHAR(50),
	TradeCategoryId INT,
	FOREIGN KEY (TradeCategoryId) REFERENCES TradeCategories(Id)
);
