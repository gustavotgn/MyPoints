CREATE DATABASE IF NOT EXISTS `Account`;
Use `Account`;
CREATE TABLE IF NOT EXISTS Account (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Amount decimal(15, 2),
    UserId int NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);
CREATE TABLE IF NOT EXISTS TransactionType (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Description nvarchar(30) NOT NULL
);

CREATE TABLE IF NOT EXISTS Transaction (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    ProductId int NULL,
    Value decimal(15, 2),
    AmountAfter decimal(15, 2),
    TransactionTypeId int NOT NULL,
    FOREIGN KEY (TransactionTypeId) REFERENCES TransactionType(Id),
    AccountId int NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Account(Id),
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);

INSERT INTO TransactionType (Id, Description)
SELECT 1,
    'Recharge'
WHERE NOT EXISTS (
        SELECT 1
        FROM TransactionType
        WHERE id = 1
    );
INSERT INTO TransactionType (Id, Description)
SELECT 2,
    'Buy'
WHERE NOT EXISTS (
        SELECT 1
        FROM TransactionType
        WHERE id = 2
    );