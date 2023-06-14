CREATE TABLE [dbo].[Aluno_Turma]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Aluno_id] INT NULL, 
    [Turma_id] INT NULL, 
    CONSTRAINT [FK_Aluno_Turma_Aluno] FOREIGN KEY (Aluno_id) REFERENCES Aluno(id), 
    CONSTRAINT [FK_Aluno_Turma_Turma] FOREIGN KEY (turma_id) REFERENCES Turma(id)
)
