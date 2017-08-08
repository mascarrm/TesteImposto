using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;
using Imposto.Core.Exceptions;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        private string DirXMLNotaFiscal = ConfigurationManager.AppSettings["DirXMLNotaFiscal"];

        private Data.NotaFiscalRepository notaFiscalRepository;

        public NotaFiscalService()
        {
            notaFiscalRepository = new Data.NotaFiscalRepository();
        }

        public Domain.NotaFiscal GerarNotaFiscal(Domain.Pedido pedido)
        {
            // Geração da Nota Fiscal com base no pedido
            // Alteração do local do código por interpretação da responsabilidade das classes
            // A nova codificação exige uma consulta no banco e forçaria uma depencia
            if (pedido.NomeCliente.Trim().Length == 0)
            {
                throw new ServiceException("Nome do cliente não informado");
            }
            if (pedido.ItensDoPedido.Count == 0)
            {
                throw new ServiceException("Nenhum item adicionado ao pedido");
            }

            Domain.NotaFiscal notaFiscal = new NotaFiscal();

            notaFiscal.NumeroNotaFiscal = notaFiscalRepository.MaxNumeroNotaFiscal() + 1;
            notaFiscal.Serie = new Random().Next(Int32.MaxValue);
            notaFiscal.NomeCliente = pedido.NomeCliente;

            notaFiscal.EstadoOrigem = pedido.EstadoOrigem.Sigla;
            notaFiscal.EstadoDestino = pedido.EstadoDestino.Sigla;

            Data.EstadoCFOPRepository estadoCFOPRepository = new Data.EstadoCFOPRepository();
            Domain.EstadoCFOP estadoCfop = estadoCFOPRepository.CarregarEstadoCFOP(notaFiscal.EstadoOrigem, notaFiscal.EstadoDestino);

            if (estadoCfop == null)
            {
                throw new ServiceException("Os estados de origem e destino selecionados não são permitidos");
            }

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                notaFiscalItem.Cfop = estadoCfop.CFOP.Codigo;

                if (notaFiscal.EstadoDestino == notaFiscal.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }

                notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido * estadoCfop.CFOP.FatorBase;
                notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;
                notaFiscalItem.AliquotaIpi = 0.1;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.AliquotaIpi = 0;
                }


                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;

                notaFiscalItem.Desconto = itemPedido.Desconto;

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            
            // Geração do arquivo XML
            try
            {
                if (!Directory.Exists(DirXMLNotaFiscal))
                {
                    Directory.CreateDirectory(DirXMLNotaFiscal);
                }

                string fileName = String.Format("{0}{1}.xml", DirXMLNotaFiscal, notaFiscal.NumeroNotaFiscal);
                FileStream notaFiscalStream = File.Open(fileName, FileMode.Create);
                XmlSerializer notaFiscalSerializer = new XmlSerializer(notaFiscal.GetType());
                notaFiscalSerializer.Serialize(notaFiscalStream, notaFiscal);
                notaFiscalStream.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException("Falha ao gerar arquivo XML", e);
            }
            
            // Gravar Nota Fiscal no banco de dados
            try
            {
                notaFiscalRepository.Salvar(notaFiscal);
            }
            catch (Exception e)
            {
                throw new ServiceException("Falha ao gravar no banco de dados", e);
            }

            return notaFiscal;
        }
    }
}
