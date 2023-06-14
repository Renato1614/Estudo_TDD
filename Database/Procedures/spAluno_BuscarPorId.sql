CREATE PROCEDURE [dbo].[spAluno_BuscarPorId]
	@Id int
AS
	SELECT * FROM Aluno WHERE Id = @Id
RETURN 0
