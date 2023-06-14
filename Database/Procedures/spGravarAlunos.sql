CREATE PROCEDURE [dbo].[spGravarAlunos]
	@Nome varchar(255),
	@Usuario varchar(255),
	@Senha Varchar(MAX)
AS
	insert into Aluno(Nome,Usuario,Senha)
	VALUES(@Nome,@Usuario,@Senha)
RETURN 0
