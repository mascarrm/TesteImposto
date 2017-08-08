USE [Teste]
GO

/****** Object:  Table [dbo].[CFOP]    Script Date: 07/08/2017  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CFOP](
	[Codigo] [varchar](10) NOT NULL,
	[Descricao] [varchar](100) NULL,
	[FatorBase] [decimal](18, 5) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[EstadosCFOP]    Script Date: 07/08/2017  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EstadosCFOP](
	[EstadoOrigem] [char](2) NOT NULL,
	[EstadoDestino] [char](2) NOT NULL,
	[CodigoCFOP] [varchar](10) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[NotaFiscal]    Script Date: 07/08/2017  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NotaFiscal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroNotaFiscal] [int] NULL,
	[Serie] [int] NULL,
	[NomeCliente] [varchar](50) NULL,
	[EstadoDestino] [varchar](50) NULL,
	[EstadoOrigem] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[NotaFiscalItem]    Script Date: 07/08/2017  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NotaFiscalItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdNotaFiscal] [int] NULL,
	[Cfop] [varchar](5) NULL,
	[TipoIcms] [varchar](20) NULL,
	[BaseIcms] [decimal](18, 5) NULL,
	[AliquotaIcms] [decimal](18, 5) NULL,
	[ValorIcms] [decimal](18, 5) NULL,
	[BaseIpi] [decimal](18, 5) NULL,
	[AliquotaIpi] [decimal](18, 5) NULL,
	[ValorIpi] [decimal](18, 5) NULL,
	[Desconto] [decimal](18, 5) NULL,
	[NomeProduto] [varchar](50) NULL,
	[CodigoProduto] [varchar](20) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


