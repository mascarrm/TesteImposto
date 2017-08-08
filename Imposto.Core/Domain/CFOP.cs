using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    class CFOP
    {
        public CFOP(string codigo, string descricao, double fatorBase)
        {
            this.Codigo = codigo;
            this.Descricao = descricao;
            this.FatorBase = fatorBase;
        }

        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public double FatorBase  { get; set; }
    }
}
