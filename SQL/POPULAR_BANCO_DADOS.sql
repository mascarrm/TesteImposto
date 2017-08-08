DECLARE @vIdNota int = 0
DECLARE @vNumeroNotaFiscal int = 1
DECLARE @vSerie int = 2
DECLARE @vNomeCliente varchar(50) = 'TESTE'
DECLARE @vEstadoDestino varchar(50) = 'SP'
DECLARE @vEstadoOrigem varchar(50) = 'RJ'
DECLARE @vCount INT = 0

DECLARE	@vIdItem int   
DECLARE @vCfop varchar(5)
DECLARE @vTipoIcms varchar(20)
DECLARE @vBaseIcms decimal(18,5)
DECLARE @vAliquotaIcms decimal(18,5)
DECLARE @vValorIcms decimal(18,5)
DECLARE @vNomeProduto varchar(50)
DECLARE @vCodigoProduto varchar(20)

WHILE (@vCount <= 1000) 
BEGIN
	SET @vIdNota = 0
	SET @vNumeroNotaFiscal  = @vCount + 1
	SET @vSerie = 1
	SET @vNomeCliente = 'TESTE ' + CONVERT(VARCHAR, @vCount)

	IF (@vCount % 2) = 0
	BEGIN		
		SET @vEstadoOrigem = 'RJ'
		SET @vEstadoDestino = 'SP'
	END
	ELSE
	BEGIN		
		SET @vEstadoOrigem = 'MG'
		SET @vEstadoDestino = 'PE'
	END

	EXEC [dbo].[P_NOTA_FISCAL] 
		@pId = @vIdNota OUTPUT,
		@pNumeroNotaFiscal = @vNumeroNotaFiscal,
		@pSerie = @vSerie,
		@pNomeCliente = @vNomeCliente,
		@pEstadoDestino = @vEstadoDestino,
		@pEstadoOrigem = @vEstadoOrigem
		
	SET @vIdItem = 0   

	IF (@vCount % 2) = 0
		SET @vCfop = '6.102'
	ELSE
		SET @vCfop = '5.100'
	
	SET @vTipoIcms = '60'
	SET @vBaseIcms = 100.00
	SET @vAliquotaIcms = 10
	SET @vValorIcms = 10
	SET @vNomeProduto = 'PRODUTO DE CARGA'
	SET @vCodigoProduto = '123-5548-555-22'

	EXEC [dbo].[P_NOTA_FISCAL_ITEM]
		@pId = @vIdItem,
		@pIdNotaFiscal = @vIdNota,
		@pCfop = @vCfop,
		@pTipoIcms = @vTipoIcms,
		@pBaseIcms = @vBaseIcms,
		@pAliquotaIcms = @vAliquotaIcms,
		@pValorIcms = @vValorIcms,
		@pNomeProduto = @vNomeProduto,
		@pCodigoProduto = @vCodigoProduto

	SET @vCount = @vCount + 1
END


INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.000', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.001', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.002', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.003', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.004', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.005', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.006', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.007', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.008', 1.0)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.009', 0.9)
INSERT INTO [dbo].[CFOP] (Codigo, FatorBase) VALUES ('6.010', 1.0)

INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'MG', '6.002')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'PA', '6.010')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'PB', '6.003')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'PE', '6.001')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'PI', '6.005')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'PR', '6.004')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'RJ', '6.000')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'RO', '6.006')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'SE', '6.009')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('MG', 'TO', '6.008')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'MG', '6.002')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'PA', '6.010')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'PB', '6.003')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'PE', '6.001')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'PI', '6.005')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'PR', '6.004')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'RJ', '6.000')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'RO', '6.006')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'SE', '6.009')
INSERT INTO [dbo].[EstadosCFOP] (EstadoOrigem, EstadoDestino, CodigoCFOP) VALUES ('SP', 'TO', '6.008')