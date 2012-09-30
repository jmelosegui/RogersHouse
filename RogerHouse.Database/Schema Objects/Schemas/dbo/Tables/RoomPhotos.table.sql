CREATE TABLE [dbo].[RoomPhotos]
(
	PhotoId int Identity (1,1) Not Null,
	RoomId int NOT NULL, 
	[FileName] nvarchar(12) Not NULL,
	[Description] nvarchar(25) Null
)
