CREATE TABLE [dbo].[Turma]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Curso_id] INT NULL, 
    [Turma] VARCHAR(50) NULL, 
    [Ano] INT NULL
)
