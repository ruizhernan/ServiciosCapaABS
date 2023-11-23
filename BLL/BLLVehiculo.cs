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
    public class BLLVehiculo : IGestor<BEVehiculo>
    {
        public BLLVehiculo()
        {
            oMPPVehiculo = new MPPVehiculo();
        }
        MPPVehiculo oMPPVehiculo;

        public bool Baja(BEVehiculo objeto)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BEVehiculo objeto)
        {
            throw new NotImplementedException();
        }

        public BEVehiculo ListarObjeto(BEVehiculo objeto)
        {
            throw new NotImplementedException();
        }

        public List<BEVehiculo> ListarTodo()
        {
            return oMPPVehiculo.ListarTodo();
        }
    }
}
