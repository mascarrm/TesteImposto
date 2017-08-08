using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    class EstadoCFOP
    {
        public EstadoCFOP(Estado estadoOrigem, Estado estadoDestino, CFOP cfop)
        {
            this.EstadoOrigem = EstadoOrigem;
            this.EstadoDestino = estadoDestino;
            this.CFOP = cfop;
        }
        public Estado EstadoOrigem { get; set; }
        public Estado EstadoDestino { get; set; }
        public CFOP CFOP { get; set; }
    }
}
