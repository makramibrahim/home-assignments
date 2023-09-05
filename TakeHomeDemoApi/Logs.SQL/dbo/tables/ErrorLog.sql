CREATE TABLE [dbo].[ErrorLog]
(
  [Id] INT NOT NULL CONSTRAINT pkErrorLog PRIMARY KEY IDENTITY (1, 1),
  [CreatedDate] DATETIME,
  [HttpMethod] NVARCHAR(20),
  [RequestURI] NVARCHAR(500),
  [RequestHeaders] NVARCHAR(max),
  [RequestJSON] NVARCHAR(max),
  [ErrorMessage] NVARCHAR(max),
  [Referer] varchar(500),
  [Origin] varchar(500),
  [SystemAccessId] int
)

