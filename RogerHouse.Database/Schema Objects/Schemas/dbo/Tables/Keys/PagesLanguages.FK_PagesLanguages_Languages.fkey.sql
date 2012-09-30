ALTER TABLE [dbo].[PagesLanguages]
	ADD CONSTRAINT [FK_PageLanguages_Languages] 
	FOREIGN KEY (LanguageID)
	REFERENCES Languages (LanguageID)	

