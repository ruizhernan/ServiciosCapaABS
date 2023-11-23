using Abs;
using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MPP
{
    public class MPPVehiculo : IGestor<BEVehiculo>
    {
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
            try
            {
                XDocument xmlDoc = XDocument.Load("Vehiculos.xml");

                var linq = from vehiculo in xmlDoc.Descendants("Vehiculo")
                            select new BEVehiculo
                            {
                                Codigo = Convert.ToInt32(vehiculo.Element("Codigo").Value.Trim()),
                                Marca = vehiculo.Element("Marca").Value.Trim(),
                                Modelo = vehiculo.Element("Modelo").Value.Trim(),
                                Patente = vehiculo.Element("Patente").Value.Trim(),
                                Km = Convert.ToInt32(vehiculo.Element("Km").Value.Trim()),
                                Categoria = vehiculo.Element("Categoria").Value.Trim()
                            };

                List<BEVehiculo> listaVehiculos = linq.ToList();
                return listaVehiculos;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al cargar el archivo XML de vehículos: " + ex.Message);
                return null;
            }
        }
    }
}
