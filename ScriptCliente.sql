DROP TABLE CLIENTE
CREATE TABLE CLIENTE
(
	EMAIL VARCHAR(20) NOT NULL,
	ID_USUARIO INT NOT NULL,
	NOME VARCHAR(50) NOT NULL,
	LOGOTIPO VARCHAR(MAX) NOT NULL,

	PRIMARY KEY (EMAIL),
)

CREATE PROCEDURE PROC_IN_CLIENTE @xml xml
AS

	INSERT INTO CLIENTE(EMAIL, NOME, LOGOTIPO)
	  SELECT
	      Pers.value('(Email)[1]', 'varchar(20)') as 'Email',
		  Pers.value('(Nome)[1]', 'Varchar(25)') as 'Nome',
		  Pers.value('(Logotipo)[1]', 'varchar(20)') as 'Logotipo'
	  FROM
		 @xml.nodes('/Clientes') as EMP(Pers)
GO

CREATE PROCEDURE PROC_SE_CLIENTE @Email varchar(25)
AS

	  SELECT * FROM CLIENTE WHERE EMAIL = @Email
GO

CREATE PROCEDURE PROC_UP_CLIENTE @Email varchar(25), @Nome varchar(20), @Logotipo varchar(max)
AS

	  UPDATE CLIENTE SET NOME = @Nome, LOGOTIPO = @Logotipo where EMAIL = @Email
GO

CREATE PROCEDURE PROC_DE_CLIENTE @Email varchar(25)
AS

	  DELETE CLIENTE where EMAIL = @Email
GO
