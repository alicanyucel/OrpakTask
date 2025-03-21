USE [master]
GO
/****** Object:  Database [Orpak]    Script Date: 19.03.2025 17:14:55 ******/
CREATE DATABASE [Orpak] ON  PRIMARY 
( NAME = N'Orpak', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Orpak.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Orpak_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Orpak_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Orpak] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Orpak].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Orpak] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Orpak] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Orpak] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Orpak] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Orpak] SET ARITHABORT OFF 
GO
ALTER DATABASE [Orpak] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Orpak] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Orpak] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Orpak] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Orpak] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Orpak] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Orpak] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Orpak] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Orpak] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Orpak] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Orpak] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Orpak] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Orpak] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Orpak] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Orpak] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Orpak] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Orpak] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Orpak] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Orpak] SET  MULTI_USER 
GO
ALTER DATABASE [Orpak] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Orpak] SET DB_CHAINING OFF 
GO
USE [Orpak]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[CategoryID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
/****** Object:  StoredProcedure [dbo].[sp_AddCategory]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddCategory]
    @CategoryName NVARCHAR(100)
AS
BEGIN
    INSERT INTO Category (CategoryName)
    VALUES (@CategoryName);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AddItem]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddItem]
    @ItemName NVARCHAR(100),
    @CategoryId INT,
    @Quantity INT,
    @UnitPrice DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Items (ItemName, CategoryId, Quantity, UnitPrice, LastUpdated)
    VALUES (@ItemName, @CategoryId, @Quantity, @UnitPrice, GETDATE());
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCategory]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteCategory]
    @CategoryId INT
AS
BEGIN
    DELETE FROM Category
    WHERE CategoryId = @CategoryId;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteItem]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteItem]
    @ItemId INT
AS
BEGIN
    DELETE FROM Items
    WHERE ItemId = @ItemId;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCategories]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetCategories]
AS
BEGIN
    SELECT CategoryId, CategoryName
    FROM Category;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetItems]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetItems]
AS
BEGIN
    SELECT ItemId, ItemName, CategoryId, Quantity, UnitPrice, LastUpdated
    FROM Items;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateCategory]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateCategory]
    @CategoryId INT,
    @CategoryName NVARCHAR(100)
AS
BEGIN
    UPDATE Category
    SET CategoryName = @CategoryName
    WHERE CategoryId = @CategoryId;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateItem]    Script Date: 19.03.2025 17:14:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateItem]
    @ItemId INT,
    @ItemName NVARCHAR(100),
    @CategoryId INT,
    @Quantity INT,
    @UnitPrice DECIMAL(18, 2)
AS
BEGIN
    UPDATE Items
    SET ItemName = @ItemName,
        CategoryId = @CategoryId,
        Quantity = @Quantity,
        UnitPrice = @UnitPrice,
        LastUpdated = GETDATE()
    WHERE ItemId = @ItemId;
END;
GO
USE [master]
GO
ALTER DATABASE [Orpak] SET  READ_WRITE 
GO
