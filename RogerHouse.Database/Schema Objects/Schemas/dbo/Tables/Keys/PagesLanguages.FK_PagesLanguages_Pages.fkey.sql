ALTER TABLE [dbo].[PagesLanguages]
	ADD CONSTRAINT [FK_PagseLanguages_Pages] 
	FOREIGN KEY (PageId)
	REFERENCES Pages (PageId)	

