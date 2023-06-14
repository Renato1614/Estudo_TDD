CREATE TABLE [dbo].[Aluno]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY , 
    [Nome] VARCHAR(255) NULL, 
    [Usuario] VARCHAR(50) NULL, 
    [Senha] CHAR(60) NULL
)

GO

