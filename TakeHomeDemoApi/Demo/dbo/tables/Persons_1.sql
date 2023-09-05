CREATE TABLE [dbo].[Persons]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    GivenName NVARCHAR(255) NOT NULL,
    Surname NVARCHAR(255) NOT NULL,
    Gender INT NOT NULL, -- Assuming Gender is stored as a string
    BirthDate DATE,
    BirthLocation NVARCHAR(255),
    DeathDate DATE,
    DeathLocation NVARCHAR(255),
    Timestamp DATETIME NOT NULL DEFAULT GETDATE()
)