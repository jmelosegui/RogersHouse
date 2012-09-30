-- =============================================
-- Script Template
-- =============================================
INSERT INTO [RogerHouse].[dbo].[Languages]

select * from
(
	SELECT 'en' as Culture, 'English' as Name Union
	SELECT 'nl', 'Dutch' 
) T 
where Culture not in (select [Culture] from [RogerHouse].[dbo].[Languages])