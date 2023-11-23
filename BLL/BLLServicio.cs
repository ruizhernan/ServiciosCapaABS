using Abs;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;

namespace BLL
{
    public class BLLServicio : IGestor<BEServicio>
    {
       public BLLServicio() {
        oMPPServicio = new MPPServicio();
        }
        MPPServicio oMPPServicio;
        public bool Baja(BEServicio objeto)
        {
            return oMPPServicio.Baja(objeto);
        }

        public virtual decimal CalcularDescuento(BEServicio oBEServicio)
        {
            decimal dto = oBEServicio.Precio * 0.10m;
            return dto;
        }

        public bool Guardar(BEServicio objeto)
        {
            return oMPPServicio.Guardar(objeto);
        }

        public BEServicio ListarObjeto(BEServicio objeto)
        {
            throw new NotImplementedException();
        }

        public List<BEServicio> ListarTodo()
        {
            throw new NotImplementedException();
        }
        public List<object> ListarServicios()
        {
            return oMPPServicio.ListarServicios();
        }
        public List<BEServicio> ListarServiciosBEServicio()
        {
            return oMPPServicio.ListarServiciosBEServicio();
        }
    }
}
