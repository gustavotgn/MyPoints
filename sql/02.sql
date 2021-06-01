USE `Identity`;
CREATE TABLE IF NOT EXISTS Address (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Street NVARCHAR(255) NOT NULL,
    City NVARCHAR(255) NOT NULL,
    State CHAR(2) NOT NULL,
    PostalCode CHAR(8) NOT NULL,
    Number nvarchar(20),
    Complements NVARCHAR(255),
    CreatedDate DATETIME NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS User (
  Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Name nvarchar(80),
  Email varchar(255),
  AddressId INT,
  FOREIGN KEY (AddressId) REFERENCES Address(Id),
  `Password` NVARCHAR(20) NOT NULL,
  CreatedDate DATETIME NOT NULL DEFAULT NOW()
);
