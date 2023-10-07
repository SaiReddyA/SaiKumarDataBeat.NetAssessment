----CREATE DATABASE DataBeat

----USE DataBeat
--CREATE TABLE Articles (
--	Id int PRIMARY KEY IDentity(1,1),
--    Articaldata_id VARCHAR(255) ,
--    journal VARCHAR(255),
--    eissn VARCHAR(255),
--    publication_date DATE,
--    article_type VARCHAR(200),
--	author_display VARCHAR(255),
--    abstract_data TEXT,
--    title_display VARCHAR(255),
--    score FLOAT,
--	SearchResult_ID  INT FOREIGN KEY REFERENCES SearchResult(Id)
--);
--CREATE TABLE SearchResult(
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	numFound INT,
--	[start] INT,
--	maxScore INT,
--)

--SELECT * FROM Articles
--SELECT * FROM SearchResult
--INSERT INTO Articles(Articaldata_id, journal, eissn, publication_date, article_type, author_display, abstract_data, title_display, score, SearchResult_ID) 
--OUTPUT INSERTED.Id
--VALUES (@Articaldata_id, @journal, @eissn, @publication_date, @article_type, @author_display, @ abstract_data, @title_display, @score, @SearchResult_ID)
--INSERT INTO Articles(Articaldata_id, journal, eissn, publication_date, article_type, author_display, abstract_data, title_display, score, SearchResult_ID)
--OUTPUT INSERTED.Id  
--VALUES (@Articaldata_id, @journal, @eissn, @publication_date, @article_type, @author_display, @abstract_data, @title_display, @score, SearchResult_ID)

--INSERT INTO SearchResult(numFound, start, maxScore) 
--OUTPUT INSERTED.Id
--VALUES (@numFound, @start, @maxScore)

