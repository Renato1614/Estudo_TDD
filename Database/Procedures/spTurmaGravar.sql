CREATE PROCEDURE [dbo].[spTurmaGravar]
 @Curso_Id int,
        @Turma varchar(255),
        @Ano int
AS
	INSERT INTO Turma(Curso_id,Turma,Ano)
    VALUES(@Curso_Id,@Turma,@Ano)
RETURN 0
