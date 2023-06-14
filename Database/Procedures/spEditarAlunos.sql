CREATE PROCEDURE [dbo].[spEditarAlunos]
	@Id int,
	@Nome varchar(255),
	@Usuario Varchar(255)

AS
	UPDATE Aluno 
	SET 
	Nome = @Nome,
	@Usuario = @Usuario
	WHERE Id= @Id
RETURN 0
