CREATE DATABASE IF NOT EXISTS `Catalog`;
Use `Catalog`;
CREATE TABLE IF NOT EXISTS Product (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Description varchar(50),
    Price decimal(15, 2),
    IsActive tinyint,
    Count int NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS `Order` (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Price decimal(15, 2),
    UserId INT NOT NULL,
    ProductId INT NOT NULL,
    TransactionId INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(Id),
    DeliveryAddress nvarchar(775),
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);


INSERT INTO Product (Description,Price,IsActive,Count)
SELECT 'Charge Cable',50,1,50
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Charge Cable'
    );

INSERT INTO Product (Description,Price,IsActive,Count)
SELECT 'Iphone 10',5000,1,5
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Iphone 10'
    );

INSERT INTO Product (Description,Price,IsActive,Count)
SELECT 'Bike',550,1,0
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Bike'
    );

INSERT INTO Product (Description,Price,IsActive,Count)
SELECT 'Fridge',2999.90,1,7
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Fridge'
    );

INSERT INTO Product (Description,Price,IsActive,Count)
SELECT 'Air Fryer',289.90,0,70
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Air Fryer'
    );
