using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service
{
    public class EstadoService
    {
        public List<Domain.Estado> CarregarEstados()
        {
            Data.EstadoRepository estadoRepository = new Data.EstadoRepository();
            return estadoRepository.CarregarEstados();
        }
    }
}
