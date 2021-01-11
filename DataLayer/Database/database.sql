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
        [Id]           INT          IDENTITY (1, 1) NOT NULL,
        [Title]        VARCHAR (50) NULL,
        [Serie_Id]     INT          NULL,
        [Number]       INT          NULL,
        [Publisher_Id] INT          NULL,
        [Stock] INT NOT NULL DEFAULT 0, 
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ComicstripAuthors' AND xtype='U')
    CREATE TABLE [dbo].[ComicstripAuthors] (
        [Comicstrip_Id] INT NOT NULL,
        [Author_Id]     INT NOT NULL
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ComicstripSeries' AND xtype='U')
    CREATE TABLE [dbo].[ComicstripSeries]
    (
	    [Id]     INT          IDENTITY (1, 1) NOT NULL,
        [Name] VARCHAR(50) NULL
        PRIMARY KEY CLUSTERED ([Id] ASC)
    )
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ComicstripBundles' AND xtype='U')
    CREATE TABLE [dbo].[ComicstripBundles] (
        [Id]           INT          IDENTITY (1, 1) NOT NULL,
        [Title]        VARCHAR (50) NULL,
        [Publisher_Id] INT          NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ComicstripBundleComicstrips' AND xtype='U')
    CREATE TABLE [dbo].[ComicstripBundleComicstrips] (
        [ComicstripBundle_Id] INT NOT NULL,
        [Comicstrip_Id]       INT NOT NULL
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Deliveries' AND xtype='U')
    CREATE TABLE [dbo].[Deliveries]
    (
	    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
        [Supplier] VARCHAR(50) NULL, 
        [Date] DATETIME NULL
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryItems' AND xtype='U')
    CREATE TABLE [dbo].[DeliveryItems]
    (
	    [Id] INT NOT NULL PRIMARY KEY, 
        [Comicstrip_Id] INT NOT NULL, 
        [Quantity] INT NOT NULL DEFAULT 1
    );
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' AND xtype='U')
    CREATE TABLE [dbo].[Orders]
    (
	    [Id] INT NOT NULL PRIMARY KEY, 
        [FirstName] VARCHAR(50) NULL, 
        [LastName] VARCHAR(50) NULL, 
	    [Email] VARCHAR(100 ) NULL,
	    [Phone] VARCHAR(50) NULL, 
        [Date] DATETIME NULL
    )
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderItems' AND xtype='U')
    CREATE TABLE [dbo].[OrderItems] (
        [Id]            INT NOT NULL,
        [Comicstrip_Id] INT NOT NULL,
        [Quantity]      INT DEFAULT ((1)) NOT NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO