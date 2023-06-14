CREATE PROCEDURE [dbo].[spDeletarAluno]
	@Id int
AS
	DELETE FROM Aluno WHERE Id=@Id
RETURN 0
