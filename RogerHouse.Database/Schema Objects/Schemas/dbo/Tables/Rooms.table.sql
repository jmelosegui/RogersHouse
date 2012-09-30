CREATE TABLE [dbo].[Rooms]
(
	RoomId int Identity(1,1) NOT NULL, 
	Name nvarchar(17) Not Null,
	Price decimal (6,2) Not null,
	PriceDescription nvarchar (17),
	Size int Not Null default (0),
	[Location] NVARCHAR (35)  NOT NULL,
	[Description] nvarchar (max) Null
)
