using System.Data.SqlClient;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository : RepositoryBase
    {

        public int MaxNumeroNotaFiscal()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                Connection.Open();
                cmd.Connection = Connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT MAX(NumeroNotaFiscal) FROM NotaFiscal";

                object result = cmd.ExecuteScalar();

                if (System.Convert.IsDBNull(result)) {
                    return 0;
                } else {
                    return (int)result;
                }
            }
        }

        public void Salvar(Domain.NotaFiscal notaFiscal)
        {
            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.Connection = Connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "P_NOTA_FISCAL";
                cmd.Parameters.Add(new SqlParameter("@pId", notaFiscal.Id) { Direction = System.Data.ParameterDirection.InputOutput });
                cmd.Parameters.Add(new SqlParameter("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal));
                cmd.Parameters.Add(new SqlParameter("@pSerie", notaFiscal.Serie));
                cmd.Parameters.Add(new SqlParameter("@pNomeCliente", notaFiscal.NomeCliente));
                cmd.Parameters.Add(new SqlParameter("@pEstadoDestino", notaFiscal.EstadoDestino));
                cmd.Parameters.Add(new SqlParameter("@pEstadoOrigem", notaFiscal.EstadoOrigem));

                int ret = cmd.ExecuteNonQuery();
                notaFiscal.Id = (int)cmd.Parameters["@pId"].Value;

                cmd.Parameters.Clear();

                foreach (Domain.NotaFiscalItem notaFiscalItem in notaFiscal.ItensDaNotaFiscal)
                {
                    cmd.CommandText = "P_NOTA_FISCAL_ITEM";
                    cmd.Parameters.Add(new SqlParameter("@pId", notaFiscalItem.Id));
                    cmd.Parameters.Add(new SqlParameter("@pIdNotaFiscal", notaFiscal.Id));
                    cmd.Parameters.Add(new SqlParameter("@pCfop", notaFiscalItem.Cfop));
                    cmd.Parameters.Add(new SqlParameter("@pTipoIcms", notaFiscalItem.TipoIcms));
                    cmd.Parameters.Add(new SqlParameter("@pBaseIcms", notaFiscalItem.BaseIcms));
                    cmd.Parameters.Add(new SqlParameter("@pAliquotaIcms", notaFiscalItem.AliquotaIcms));
                    cmd.Parameters.Add(new SqlParameter("@pValorIcms", notaFiscalItem.ValorIcms));
                    cmd.Parameters.Add(new SqlParameter("@pBaseIpi", notaFiscalItem.BaseIpi));
                    cmd.Parameters.Add(new SqlParameter("@pAliquotaIpi", notaFiscalItem.AliquotaIpi));
                    cmd.Parameters.Add(new SqlParameter("@pValorIpi", notaFiscalItem.ValorIpi));
                    cmd.Parameters.Add(new SqlParameter("@pDesconto", notaFiscalItem.Desconto));
                    cmd.Parameters.Add(new SqlParameter("@pNomeProduto", notaFiscalItem.NomeProduto));
                    cmd.Parameters.Add(new SqlParameter("@pCodigoProduto", notaFiscalItem.CodigoProduto));

                    ret = cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                }

                Connection.Close();
            }
        }
    }
}
