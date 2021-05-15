IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DavidMoralesGdocsAppDBs')
BEGIN
    CREATE DATABASE [DavidMoralesGdocsAppDBs]
END

GO

USE [DavidMoralesGdocsAppDBs]

GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Companies')
BEGIN
	CREATE TABLE [dbo].[Companies](
		[CompanyId] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[CreatedBy] [nvarchar](max) NOT NULL,
		[CreatedAt] [datetime2](7) NOT NULL,
		[UpdatedBy] [nvarchar](max) NULL,
		[UpdatedAt] [datetime2](7) NULL,
	 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
	(
		[CompanyId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='people')
BEGIN
	CREATE TABLE [dbo].[People](
		[PersonId] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[LastName] [nvarchar](50) NOT NULL,
		[Phone] [nvarchar](22) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[CreatedBy] [nvarchar](max) NOT NULL,
		[CreatedAt] [datetime2](7) NOT NULL,
		[UpdatedBy] [nvarchar](max) NULL,
		[UpdatedAt] [datetime2](7) NULL,
	 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
	(
		[PersonId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([CompanyId])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Companies_CompanyId]
GO

