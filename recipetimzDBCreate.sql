USE [master]
GO
/****** Object:  Database [recipetimz]    Script Date: 3/10/2021 10:08:11 AM ******/
CREATE DATABASE [recipetimz]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'recipetimz', FILENAME = N'c:\databases\recipetimz\recipetimz.mdf' , SIZE = 8192KB , MAXSIZE = 20971520KB , FILEGROWTH = 10%)
 LOG ON 
( NAME = N'recipetimz_log', FILENAME = N'c:\databases\recipetimz\recipetimz_log.ldf' , SIZE = 8192KB , MAXSIZE = 1048576KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [recipetimz] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [recipetimz].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [recipetimz] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [recipetimz] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [recipetimz] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [recipetimz] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [recipetimz] SET ARITHABORT OFF 
GO
ALTER DATABASE [recipetimz] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [recipetimz] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [recipetimz] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [recipetimz] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [recipetimz] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [recipetimz] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [recipetimz] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [recipetimz] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [recipetimz] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [recipetimz] SET  DISABLE_BROKER 
GO
ALTER DATABASE [recipetimz] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
GO
ALTER DATABASE [recipetimz] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [recipetimz] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [recipetimz] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [recipetimz] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [recipetimz] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [recipetimz] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [recipetimz] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [recipetimz] SET  MULTI_USER 
GO
ALTER DATABASE [recipetimz] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [recipetimz] SET DB_CHAINING OFF 
GO
ALTER DATABASE [recipetimz] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [recipetimz] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [recipetimz] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'recipetimz', N'ON'
GO
ALTER DATABASE [recipetimz] SET QUERY_STORE = OFF
GO
USE [recipetimz]
GO
/****** Object:  Table [dbo].[ingredient]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredient](
	[id] [uniqueidentifier] NOT NULL,
	[recipeId] [uniqueidentifier] NOT NULL,
	[name] [varchar](255) NOT NULL,
	[quantity] [varchar](255) NULL,
 CONSTRAINT [PK_ingredient] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](255) NOT NULL,
	[description] [varchar](255) NULL,
 CONSTRAINT [PK_recipe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ingredient]  WITH CHECK ADD  CONSTRAINT [FK_ingredient_recipe] FOREIGN KEY([recipeId])
REFERENCES [dbo].[recipe] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ingredient] CHECK CONSTRAINT [FK_ingredient_recipe]
GO
/****** Object:  StoredProcedure [dbo].[AddIngredient]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[AddIngredient]  
(  
   @RecipeId uniqueidentifier,
   @Name varchar (255),  
   @Quantity varchar (255)
)  
as  
BEGIN  
   INSERT INTO ingredient VALUES(newid(), @RecipeId, @Name, @Quantity)  
END 
GO
/****** Object:  StoredProcedure [dbo].[AddRecipe]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddRecipe]  
(  
   @Name varchar(255),  
   @Description varchar(255)
)  
as  
begin  
   INSERT INTO recipe VALUES(newid(), @Name, @Description)  
End 
GO
/****** Object:  StoredProcedure [dbo].[DeleteIngredientById]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteIngredientById]  
(  
   @Id uniqueidentifier  
)  
as   
BEGIN  
   DELETE FROM Ingredient WHERE Id=@Id  
END 
GO
/****** Object:  StoredProcedure [dbo].[DeleteRecipeById]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteRecipeById]  
(  
   @Id uniqueidentifier  
)  
as   
BEGIN  
   DELETE FROM Recipe WHERE Id=@Id  
END 
GO
/****** Object:  StoredProcedure [dbo].[GetIngredientById]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetIngredientById]  
(  
   @Id uniqueidentifier  
)  
as  
BEGIN  
   SELECT * FROM ingredient  
   WHERE id = @Id
END 
GO
/****** Object:  StoredProcedure [dbo].[GetIngredients]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetIngredients]  
as  
BEGIN  
   SELECT * FROM ingredient  
END 
GO
/****** Object:  StoredProcedure [dbo].[GetIngredientsByRecipeId]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GetIngredientsByRecipeId] 
(  
   @RecipeId uniqueidentifier  
)  
as  
BEGIN  
   SELECT * FROM ingredient 
   WHERE recipeId = @RecipeId
END 
GO
/****** Object:  StoredProcedure [dbo].[GetRecipes]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetRecipes]  
as  
BEGIN  
   SELECT * FROM recipe  
END 
GO
/****** Object:  StoredProcedure [dbo].[UpdateIngredient]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[UpdateIngredient]  
(  
   @Id uniqueidentifier,  
   @Name varchar (255),  
   @Quantity varchar (255)
)  
as  
BEGIN  
   UPDATE ingredient   
   SET name=@Name,  
   quantity=@Quantity 
   WHERE Id=@Id  
END 
GO
/****** Object:  StoredProcedure [dbo].[UpdateRecipe]    Script Date: 3/10/2021 10:08:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[UpdateRecipe]  
(  
   @Id uniqueidentifier,  
   @Name varchar (255),  
   @Description varchar (255)
)  
as  
BEGIN  
   Update Recipe   
   SET name=@Name,  
   description=@Description
   WHERE Id=@Id  
END 
GO
USE [master]
GO
ALTER DATABASE [recipetimz] SET  READ_WRITE 
GO
