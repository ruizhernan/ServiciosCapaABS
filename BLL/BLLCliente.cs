using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abs;
using BE;
using MPP;

namespace BLL
{
    public class BLLCliente : IGestor<BECliente>
    {
        public BLLCliente() {
            oMPPCliente = new MPPCliente();
        }
        MPPCliente oMPPCliente;
        public bool Baja(BECliente objeto)
        {
            return oMPPCliente.Baja(objeto);
        }

        public bool Guardar(BECliente objeto)
        {
            return oMPPCliente.Guardar(objeto);
        }
        public bool Modificar (BECliente objeto)
        {
            return oMPPCliente.Modificar(objeto);
        }

        public BECliente ListarObjeto(BECliente objeto)
        {
            throw new NotImplementedException();
        }

        public List<BECliente> ListarTodo()
        {
            return oMPPCliente.ListarTodo();
        }

        public List<object> ListarClientes()
        {
            return oMPPCliente.ListarClientes();
        }

        public List<BEVehiculo> ObtenerVehiculosCliente(BECliente oBECliente)
        {
            return oMPPCliente.ObtenerVehiculosCliente(oBECliente);
        }
    }
}
