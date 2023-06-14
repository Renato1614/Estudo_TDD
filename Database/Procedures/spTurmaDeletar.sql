CREATE PROCEDURE [dbo].[spTurmaDeletar]
	@Id int
AS
	DELETE FROM Turma WHERE Id=@Id
RETURN 0
