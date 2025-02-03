CREATE TABLE EmailQueue (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    EmailTo VARCHAR(MAX) NOT NULL,
    EmailFrom VARCHAR(255) NOT NULL,
    EmailTitle VARCHAR(255) NOT NULL,
    EmailBody VARCHAR(MAX) NOT NULL,
    IsHtml bit NOT NULL,
    EmailDescription  VARCHAR(MAX) NOT NULL,
    RefNumber VARCHAR(255) NOT NULL,
    AddedDate CreatedAt DATETIME DEFAULT GETDATE(),
    AddedBy VARCHAR(255) NOT NULL,
    UpdatedDate UpdatedAt DATETIME null,
    UpdatedBy VARCHAR(255) null
);
go