ALTER TABLE [dbo].[RoomPhotos]
	ADD CONSTRAINT [FK_RoomPhotos_Rooms] 
	FOREIGN KEY (RoomId)
	REFERENCES Rooms (RoomId)	

