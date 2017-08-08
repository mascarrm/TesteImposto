USE [Teste]
GO
IF OBJECT_ID('dbo.P_NOTA_FISCAL_TOTAIS_CFOP') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_NOTA_FISCAL_TOTAIS_CFOP
    IF OBJECT_ID('dbo.P_NOTA_FISCAL_TOTAIS_CFOP') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_NOTA_FISCAL_TOTAIS_CFOP >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL_TOTAIS_CFOP APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE [dbo].[P_NOTA_FISCAL_TOTAIS_CFOP]
AS
BEGIN
	SELECT 
		 [Cfop] AS CFOP
		,SUM([BaseIcms]) AS VALOR_TOTAL_BASE_ICMS
		,SUM([ValorIcms]) AS VALOR_TOTAL_ICMS
		,SUM([BaseIpi]) AS VALOR_TOTAL_BASE_IPI
		,SUM([ValorIpi]) AS VALOR_TOTAL_IPI
	FROM
		[NotaFiscalItem]
	GROUP BY
		[Cfop]
END
GO
GRANT EXECUTE ON dbo.P_NOTA_FISCAL_TOTAIS_CFOP TO [public]
go
IF OBJECT_ID('dbo.P_NOTA_FISCAL_TOTAIS_CFOP') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL_TOTAIS_CFOP CRIADA >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_NOTA_FISCAL_TOTAIS_CFOP >>>'
go