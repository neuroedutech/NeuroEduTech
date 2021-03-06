USE [master]
GO
/****** Object:  Database [neuroEduTech]    Script Date: 02/11/2017 14:03:43 ******/
CREATE DATABASE [neuroEduTech]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'neuroEduTech', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\neuroEduTech.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'neuroEduTech_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\neuroEduTech_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [neuroEduTech] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [neuroEduTech].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [neuroEduTech] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [neuroEduTech] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [neuroEduTech] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [neuroEduTech] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [neuroEduTech] SET ARITHABORT OFF 
GO
ALTER DATABASE [neuroEduTech] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [neuroEduTech] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [neuroEduTech] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [neuroEduTech] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [neuroEduTech] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [neuroEduTech] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [neuroEduTech] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [neuroEduTech] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [neuroEduTech] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [neuroEduTech] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [neuroEduTech] SET  DISABLE_BROKER 
GO
ALTER DATABASE [neuroEduTech] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [neuroEduTech] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [neuroEduTech] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [neuroEduTech] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [neuroEduTech] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [neuroEduTech] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [neuroEduTech] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [neuroEduTech] SET RECOVERY FULL 
GO
ALTER DATABASE [neuroEduTech] SET  MULTI_USER 
GO
ALTER DATABASE [neuroEduTech] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [neuroEduTech] SET DB_CHAINING OFF 
GO
ALTER DATABASE [neuroEduTech] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [neuroEduTech] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'neuroEduTech', N'ON'
GO
USE [neuroEduTech]
GO
/****** Object:  StoredProcedure [dbo].[sp_c_perguntas_e_respostas]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_c_perguntas_e_respostas]
(
	@pergunta varchar(MAX),
	@resposta1 varchar(MAX),
	@resposta2 varchar(MAX) = '',
	@resposta3 varchar(MAX) = '',
	@resposta4 varchar(MAX) = '',
	@outputmsg [varchar](80) OUTPUT
)
as 
begin

	DECLARE @idPergunta int 

	insert into Perguntas
	values(@pergunta)

	select top 1 @idPergunta = idPergunta from Perguntas order by 1 desc

	insert into Respostas
	values(@resposta1, @idPergunta)

	insert into Respostas
	values(@resposta2, @idPergunta)
	
	insert into Respostas
	values(@resposta3, @idPergunta)
	
	insert into Respostas
	values(@resposta4, @idPergunta)

	SET @outputmsg = 'Pergunta cadastrada com sucesso'

end
return 
GO
/****** Object:  StoredProcedure [dbo].[sp_c_respostas_crencas]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_c_respostas_crencas]
(
	@codUsuario int = 1,
	@codResposta1 bigint,
	@codResposta2 bigint,
	@codResposta3 bigint,
	@codResposta4 bigint,
	@resposta1 int,
	@crenca1 int, 
	@resposta2 int,
	@crenca2 int, 
	@resposta3 int,
	@crenca3 int, 
	@resposta4 int,
	@crenca4 int,
	@outputmsg [varchar](80) OUTPUT
)

AS
begin
 insert into resposta_aluno
 values (@codUsuario, @codResposta1, @resposta1, @crenca1)

 insert into resposta_aluno
 values (@codUsuario, @codResposta2, @resposta2, @crenca2)
 
 insert into resposta_aluno
 values (@codUsuario, @codResposta3, @resposta3, @crenca3)
 
 insert into resposta_aluno
 values (@codUsuario, @codResposta4, @resposta4, @crenca4)

 SET @outputmsg = 'Resposta enviada com sucesso'
 end
 return
GO
/****** Object:  StoredProcedure [dbo].[sp_v_perguntas_e_respostas]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_v_perguntas_e_respostas]
(
	@idPergunta int
)
as
select p.idPergunta as idResposta, p.txDescricao from perguntas p where p.idPergunta = @idPergunta
union all
select r.idResposta,r.txDescricao from respostas r where r.codPergunta = @idPergunta


--exec sp_v_perguntas_e_respostas
GO
/****** Object:  Table [dbo].[Perguntas]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Perguntas](
	[idPergunta] [bigint] IDENTITY(1,1) NOT NULL,
	[txDescricao] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPergunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[resposta_aluno]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resposta_aluno](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codUsuario] [int] NULL,
	[codResposta] [bigint] NULL,
	[resposta] [int] NOT NULL,
	[crenca] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Respostas]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Respostas](
	[idResposta] [bigint] IDENTITY(1,1) NOT NULL,
	[txDescricao] [varchar](200) NULL,
	[codPergunta] [bigint] NULL,
 CONSTRAINT [PK_Respostas] PRIMARY KEY CLUSTERED 
(
	[idResposta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 02/11/2017 14:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[txnomeUsuario] [varchar](50) NULL,
	[txemail] [varchar](100) NULL,
	[txsenha] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[resposta_aluno]  WITH CHECK ADD  CONSTRAINT [FK_codResposta_Aluno] FOREIGN KEY([codResposta])
REFERENCES [dbo].[Respostas] ([idResposta])
GO
ALTER TABLE [dbo].[resposta_aluno] CHECK CONSTRAINT [FK_codResposta_Aluno]
GO
ALTER TABLE [dbo].[resposta_aluno]  WITH CHECK ADD  CONSTRAINT [FK_codUsuario_Aluno] FOREIGN KEY([codUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[resposta_aluno] CHECK CONSTRAINT [FK_codUsuario_Aluno]
GO
ALTER TABLE [dbo].[Respostas]  WITH CHECK ADD  CONSTRAINT [FK_RESPOSTAPERGUNTA] FOREIGN KEY([codPergunta])
REFERENCES [dbo].[Perguntas] ([idPergunta])
GO
ALTER TABLE [dbo].[Respostas] CHECK CONSTRAINT [FK_RESPOSTAPERGUNTA]
GO
USE [master]
GO
ALTER DATABASE [neuroEduTech] SET  READ_WRITE 
GO
