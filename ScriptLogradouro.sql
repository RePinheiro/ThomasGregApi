CREATE TABLE LOGRADOURO
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	LOGRADOURO VARCHAR(25) NOT NULL,
	EMAIL VARCHAR(20) NOT NULL
)

CREATE PROCEDURE PROC_IN_LOGRADOURO @xml xml
AS

	INSERT INTO LOGRADOURO(LOGRADOURO, EMAIL)
	  SELECT
		  Pers.value('(Logradouro)[1]', 'Varchar(25)') as 'Logradouro',
		  Pers.value('(Email)[1]', 'varchar(20)') as 'Email'
	  FROM
		 @xml.nodes('/Logradouro') as EMP(Pers)
GO

CREATE PROCEDURE PROC_SE_LOGRADOURO @Email varchar(25), @Id int = null
AS
	IF @Id <> null
		SELECT * FROM LOGRADOURO where EMAIL = @Email AND ID = @Id
	ELSE
		SELECT * FROM LOGRADOURO where EMAIL = @Email 
	  
GO

CREATE PROCEDURE PROC_UP_LOGRADOURO @Email varchar(25), @Id varchar(20), @NovoValor varchar(25)
AS
	  UPDATE LOGRADOURO SET LOGRADOURO = @NovoValor where EMAIL = @Email and ID = @id
GO

CREATE PROCEDURE PROC_DE_LOGRADOURO @Email varchar(25), @Id int = null
AS
	IF @Id <> null
		DELETE LOGRADOURO where EMAIL = @Email AND ID = @Id
	ELSE
		DELETE LOGRADOURO where EMAIL = @Email 
GO
