using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imposto.UnitTest
{
    [TestClass]
    public class NotaFiscalTest
    {
        [TestMethod]
        [ExpectedException(typeof(Imposto.Core.Exceptions.ServiceException))]
        public void GerarNotaFiscalNomeClienteException()
        {
            Imposto.Core.Domain.Pedido pedido = new Imposto.Core.Domain.Pedido();
            Imposto.Core.Service.NotaFiscalService notaFiscalService = new Core.Service.NotaFiscalService();
            pedido.NomeCliente = "";
            notaFiscalService.GerarNotaFiscal(pedido);
        }

        [TestMethod]
        [ExpectedException(typeof(Imposto.Core.Exceptions.ServiceException))]
        public void GerarNotaFiscalItensException()
        {
            Imposto.Core.Domain.Pedido pedido = new Imposto.Core.Domain.Pedido();
            Imposto.Core.Service.NotaFiscalService notaFiscalService = new Core.Service.NotaFiscalService();
            pedido.NomeCliente = "Nome do Cliente";
            notaFiscalService.GerarNotaFiscal(pedido);
        }

        [TestMethod]
        [ExpectedException(typeof(Imposto.Core.Exceptions.ServiceException))]
        public void GerarNotaFiscalEstadoInvalido()
        {
            Imposto.Core.Domain.Pedido pedido = new Imposto.Core.Domain.Pedido();
            Imposto.Core.Service.NotaFiscalService notaFiscalService = new Core.Service.NotaFiscalService();
            pedido.NomeCliente = "Nome do Cliente";
            pedido.EstadoOrigem = new Core.Domain.Estado("XA", "XA", 0);
            pedido.EstadoDestino = new Core.Domain.Estado("XA", "XA", 0);
            pedido.ItensDoPedido.Add(new Core.Domain.PedidoItem());
            notaFiscalService.GerarNotaFiscal(pedido);
        }

        [TestMethod]
        public void GerarNotaFiscalICMSBrinde()
        {
            Imposto.Core.Domain.Pedido pedido = new Imposto.Core.Domain.Pedido();
            Imposto.Core.Domain.NotaFiscal notaFiscal;
            Imposto.Core.Service.NotaFiscalService notaFiscalService = new Core.Service.NotaFiscalService();
            pedido.NomeCliente = "Nome do Cliente";
            pedido.EstadoOrigem = new Core.Domain.Estado("SP", "SP", 0);
            pedido.EstadoDestino = new Core.Domain.Estado("MG", "MG", 0);
            Core.Domain.PedidoItem pedidoItem = new Core.Domain.PedidoItem() { NomeProduto = "Produto 1",
                                                                               CodigoProduto = "COD",
                                                                               ValorItemPedido = 100,
                                                                               Desconto = 0,
                                                                               Brinde = true
                                                                             };
            pedido.ItensDoPedido.Add(pedidoItem);
            notaFiscal = notaFiscalService.GerarNotaFiscal(pedido);
            Assert.AreEqual(notaFiscal.ItensDaNotaFiscal[0].TipoIcms, "60");
        }
    }
}
