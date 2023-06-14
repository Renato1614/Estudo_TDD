CREATE PROCEDURE [dbo].[spTurmaEditar]
        @Id int,
        @Curso_Id int,
        @Turma varchar(255),
        @Ano int
AS
	UPDATE Turma SET
    Curso_id = @Curso_Id,
    Turma = @Turma,
    Ano = @Ano
    WHERE Id=@Id
RETURN 0
