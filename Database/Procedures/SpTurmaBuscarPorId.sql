CREATE PROCEDURE [dbo].[SpTurmaBuscarPorId]
	@Id int
AS
	SELECT * FROM Turma WHERE Id=@Id
RETURN 0
