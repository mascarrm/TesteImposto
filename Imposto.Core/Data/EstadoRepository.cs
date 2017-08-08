using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Data
{
    public class EstadoRepository : RepositoryBase
    {
        public List<Domain.Estado> CarregarEstados()
        {
            List<Domain.Estado> lista = new List<Domain.Estado>();

            lista.Add(new Domain.Estado("AC - Acre", "AC", 0));
            lista.Add(new Domain.Estado("AL - Alagoas", "AL", 0));
            lista.Add(new Domain.Estado("AP - Amapá", "AP", 0));
            lista.Add(new Domain.Estado("AM - Amazonas", "AM", 0));
            lista.Add(new Domain.Estado("BA - Bahia", "BA", 0));
            lista.Add(new Domain.Estado("CE - Ceará", "CE", 0));
            lista.Add(new Domain.Estado("DF - Distrito Federal", "DF", 0));
            lista.Add(new Domain.Estado("ES - Espírito Santo", "ES", 0.1));
            lista.Add(new Domain.Estado("GO - Goiás", "GO", 0));
            lista.Add(new Domain.Estado("MA - Maranhão", "MA", 0));
            lista.Add(new Domain.Estado("MT - Mato Grosso", "MT", 0));
            lista.Add(new Domain.Estado("MS - Mato Grosso do Sul", "MS", 0));
            lista.Add(new Domain.Estado("MG - Minas Gerais", "MG", 0.1));
            lista.Add(new Domain.Estado("PA - Pará", "PA", 0));
            lista.Add(new Domain.Estado("PB - Paraíba", "PB", 0));
            lista.Add(new Domain.Estado("PR - Paraná", "PR", 0));
            lista.Add(new Domain.Estado("PE - Pernambuco", "PE", 0));
            lista.Add(new Domain.Estado("PI - Piauí", "PI", 0));
            lista.Add(new Domain.Estado("RJ - Rio de Janeiro", "RJ", 0.1));
            lista.Add(new Domain.Estado("RN - Rio Grande do Norte", "RN", 0));
            lista.Add(new Domain.Estado("RS - Rio Grande do Sul", "RS", 0));
            lista.Add(new Domain.Estado("RO - Rondônia", "RO", 0));
            lista.Add(new Domain.Estado("RR - Roraima", "RR", 0));
            lista.Add(new Domain.Estado("SC - Santa Catarina", "SC", 0));
            lista.Add(new Domain.Estado("SP - São Paulo", "SP", 0.1));
            lista.Add(new Domain.Estado("SE - Sergipe", "SE", 0));
            lista.Add(new Domain.Estado("TO - Tocantins", "TO", 0));


            return lista;
        }
    }
}
