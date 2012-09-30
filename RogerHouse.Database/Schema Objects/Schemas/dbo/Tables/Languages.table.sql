CREATE TABLE [dbo].[Languages]
(
	LanguageId int Identity(1,1) NOT NULL, 
	Culture nvarchar(5) Not Null,
	Name nvarchar(50) Not Null
)
