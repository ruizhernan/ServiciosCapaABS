using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEServicio : Entidad
    {
       
        public DateTime Fecha { get; set; }
        public BECliente ClienteServicio { get; set; }
        public string Aceite { get; set; }
        public decimal LitrosAceite { get; set; }
        public decimal Precio { get; set; }

    }
}
