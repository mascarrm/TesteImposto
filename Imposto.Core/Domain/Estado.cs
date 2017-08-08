using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Estado
    {
        public Estado(string nome, string sigla, double desconto)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            this.Desconto = desconto;
        }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public double Desconto { get; set; }
    }
}
