CREATE TABLE [dbo].[Pages]
(
	PageId int Identity(1,1) NOT NULL, 
	[Path] nvarchar(50) Not Null,
	Title nvarchar(75) Not Null,
	Body nvarchar(max) Not Null,
	Visible bit not null,
	ShowInMenu bit not null,
	[Order] int not null
)
