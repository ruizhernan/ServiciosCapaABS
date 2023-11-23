using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLServicioBasico : BLLServicio
    {
        public override decimal CalcularDescuento(BEServicio oBEServicio)
        {
            decimal dto = oBEServicio.Precio * 0.1m;
            return dto;
        }
    }
}
