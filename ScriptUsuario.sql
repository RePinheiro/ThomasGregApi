drop table USUARIO
CREATE TABLE USUARIO
(
	ID int IDENTITY(1,1) ,
	USUARIO VARCHAR(15) NOT NULL,
	SENHA VARCHAR(15) NOT NULL

	CONSTRAINT PK_USUARIO PRIMARY KEY (ID,USUARIO)
)

CREATE PROCEDURE PROC_IN_USUARIO @Usuario varchar(15), @Senha varchar(15)
AS
	INSERT INTO USUARIO VALUES(@Usuario, @Senha)
GO

CREATE PROCEDURE PROC_SE_USUARIO @Usuario varchar(15)
AS
	SELECT * FROM USUARIO where USUARIO = @Usuario  
GO

CREATE PROCEDURE PROC_UP_USUARIO @Usuario varchar(15), @SenhaAntiga varchar(20), @SenhaNova varchar(25)
AS
	DECLARE @COUNT AS INTEGER
	SET @COUNT = (SELECT COUNT(*) FROM USUARIO WHERE USUARIO = @Usuario AND SENHA = @SenhaAntiga)
	
	IF @COUNT > 0
	  UPDATE USUARIO SET SENHA = @SenhaNova where USUARIO = @Usuario

GO

CREATE PROCEDURE PROC_DE_USUARIO @Usuario varchar(15)
AS
	DELETE USUARIO where USUARIO = @Usuario
GO

select * from USUARIO

PROC_SE_USUARIO 'repinheiro'
