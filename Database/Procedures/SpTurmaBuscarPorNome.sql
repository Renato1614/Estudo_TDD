CREATE PROCEDURE [dbo].[SpTurmaBuscarPorNome]
	@Nome VARCHAR(255),
	@Id int
AS
	SELECT * FROM Turma WHERE Turma=@Nome and Id <> @Id
RETURN 0
