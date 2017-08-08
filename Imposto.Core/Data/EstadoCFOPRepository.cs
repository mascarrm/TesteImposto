using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Imposto.Core.Data
{
    class EstadoCFOPRepository : RepositoryBase
    {

        public Domain.EstadoCFOP CarregarEstadoCFOP(string estadoOrigem, string estadoDestino)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                EstadoRepository estadoRepository = new Data.EstadoRepository();
                List<Domain.Estado> estados = estadoRepository.CarregarEstados();

                Domain.EstadoCFOP estadoCfop = null;

                Connection.Open();
                cmd.Connection = Connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "P_ESTADOCFOP_CARREGAR";
                cmd.Parameters.Add(new SqlParameter("@pEstadoOrigem", estadoOrigem));
                cmd.Parameters.Add(new SqlParameter("@pEstadoDestino", estadoDestino));

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Domain.Estado estOrigem = estados.Find(x => x.Sigla.Equals((string)reader["EstadoOrigem"]));
                    Domain.Estado estDestino = estados.Find(x => x.Sigla.Equals((string)reader["EstadoDestino"]));
                    string codigoCfop = (string)reader["CodigoCFOP"];
                    string descricao = "";
                    if (System.Convert.IsDBNull(reader["Descricao"]))
                    {
                        descricao = (string)reader["Descricao"];
                    }

                    double fatorBase = double.Parse(reader["FatorBase"].ToString());
                    Domain.CFOP cfop = new Domain.CFOP(codigoCfop, descricao, fatorBase);

                    estadoCfop = new Domain.EstadoCFOP(estOrigem, estDestino, cfop);
                }

                Connection.Close();

                return estadoCfop;
            }
        }
    }
}
