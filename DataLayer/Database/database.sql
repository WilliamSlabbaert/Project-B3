IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Publishers' AND xtype='U')
    CREATE TABLE [dbo].[Publishers] (
        [Id]   INT          IDENTITY (1, 1) NOT NULL,
        [Name] VARCHAR (50) NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Authors' AND xtype='U')
    CREATE TABLE [dbo].[Authors] (
        [Id]           INT          IDENTITY (1, 1) NOT NULL,
        [Firstname]    VARCHAR (50) NULL,
        [Lastname]     VARCHAR (50) NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Comicstrips' AND xtype='U')
    CREATE TABLE [dbo].[Comicstrips] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Titel]  VARCHAR (50) NULL,
    [Serie]  VARCHAR (50) NULL,
    [Number] INT          NULL,
    [Publisher_Id] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ComicstripAuthors' AND xtype='U')
    CREATE TABLE [dbo].[ComicstripAuthors] (
        [Comicstrip_Id] INT NOT NULL,
        [Author_Id]     INT NOT NULL
    );
GO