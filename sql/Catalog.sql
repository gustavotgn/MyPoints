CREATE DATABASE IF NOT EXISTS `Catalog`;
Use `Catalog`;
CREATE TABLE IF NOT EXISTS Product (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Description varchar(50),
    Value decimal(15, 2),
    IsActive tinyint,
    Count int NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);
CREATE TABLE IF NOT EXISTS OrderStatus (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Description nvarchar(30) NOT NULL
);
CREATE TABLE IF NOT EXISTS OrderAddress (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Street NVARCHAR(255) NOT NULL,
    City NVARCHAR(255) NOT NULL,
    State CHAR(2) NOT NULL,
    PostalCode CHAR(8) NOT NULL,
    Number nvarchar(20),
    Complements NVARCHAR(255),
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);
CREATE TABLE IF NOT EXISTS `Order` (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Value decimal(15, 2),
    UserId INT NOT NULL,
    ProductId INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(Id),
    StatusId INT NOT NULL,
    FOREIGN KEY (StatusId) REFERENCES OrderStatus(Id),
    TransactionId INT NULL,
    AddressId INT NULL,
    FOREIGN KEY (AddressId) REFERENCES OrderAddress(Id),
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS OrderNotification (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Property NVARCHAR(255) NOT NULL,
    Message NVARCHAR(255) NOT NULL,
    StatusId INT NOT NULL,
    FOREIGN KEY (StatusId) REFERENCES OrderStatus(Id),
    OrderId INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES `Order`(Id)
);

INSERT INTO Product (Description, Value, IsActive, Count)
SELECT 'Charge Cable',
    50,
    1,
    50
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Charge Cable'
    );

INSERT INTO Product (Description, Value, IsActive, Count)
SELECT 'Iphone 10',
    5000,
    1,
    5
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Iphone 10'
    );

INSERT INTO Product (Description, Value, IsActive, Count)
SELECT 'Bike',
    550,
    1,
    0
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Bike'
    );

INSERT INTO Product (Description, Value, IsActive, Count)
SELECT 'Fridge',
    2999.90,
    1,
    7
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Fridge'
    );

INSERT INTO Product (Description, Value, IsActive, Count)
SELECT 'Air Fryer',
    289.90,
    0,
    70
WHERE NOT EXISTS (
        SELECT 1
        FROM Product
        WHERE Description = 'Air Fryer'
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 1,
    'Order in Process'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 1
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 2,
    'Invalid order'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 2
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 3,
    'Making payment'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 3
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 4,
    'Payment made'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 4
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 5,
    'Payment rejected'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 5
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 6,
    'Out for delivery'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 6
    );

INSERT INTO OrderStatus (Id, Description)
SELECT 7,
    'Delivered'
WHERE NOT EXISTS (
        SELECT 1
        FROM OrderStatus
        WHERE id = 7
    );