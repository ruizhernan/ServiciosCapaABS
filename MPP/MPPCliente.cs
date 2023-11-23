using Abs;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MPP
{
    public class MPPCliente : IGestor<BECliente>
    {
        string archivo = "Clientes.xml";
        public bool Baja(BECliente objeto)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

                // Buscar el elemento que coincide con el DNI del cliente a dar de baja
                XElement clienteEliminar = xmlDoc.Descendants("Cliente")
                    .FirstOrDefault(c => c.Element("DNI").Value == objeto.DNI.ToString());

                if (clienteEliminar != null)
                {
                    // Eliminar el elemento del documento
                    clienteEliminar.Remove();

                    // Guardar los cambios en el archivo
                    xmlDoc.Save(archivo);
                    return true;
                }

                return false; // Si no se encontró el cliente a dar de baja
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al dar de baja el cliente: " + ex.Message);
                return false;
            }
        }

        public bool Modificar(BECliente clienteModificado)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

              
                XElement clienteModificar = xmlDoc.Descendants("Cliente")
                    .FirstOrDefault(c => c.Element("DNI").Value == clienteModificado.DNI.ToString());

                if (clienteModificar != null)
                {
                    
                    clienteModificar.Element("Nombre").Value = clienteModificado.Nombre;
                    clienteModificar.Element("Apellido").Value = clienteModificado.Apellido;
                    clienteModificar.Element("FechaNac").Value = clienteModificado.FechaNac.ToString("yyyy-MM-dd");
                    clienteModificar.Element("Mail").Value = clienteModificado.Mail;
                  

                    
                    xmlDoc.Save(archivo);
                    return true;
                }

                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el cliente: " + ex.Message);
                return false;
            }
        }
    

    public bool Guardar(BECliente objeto)
        {


            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

               

                if (xmlDoc.Descendants("Cliente").Any(c => c.Element("DNI").Value == objeto.DNI.ToString()))
                {
                   
                    return false;
                }
                int codigoCliente = GenerarCodigo(xmlDoc);

                XElement nuevoCliente = new XElement("Cliente",
                    new XElement("Codigo", codigoCliente),
                    new XElement("Nombre", objeto.Nombre),
                    new XElement("Apellido", objeto.Apellido),
                    new XElement("DNI", objeto.DNI),
                    new XElement("FechaNac", objeto.FechaNac.ToString("yyyy-MM-dd")),
                    new XElement("Mail", objeto.Mail),
                    new XElement("VehiculoCliente",
                new XElement("Codigo", objeto.VehiculoCliente.Codigo),
                new XElement("Marca", objeto.VehiculoCliente.Marca),
                new XElement("Modelo", objeto.VehiculoCliente.Modelo),
                new XElement("Patente", objeto.VehiculoCliente.Patente),
                new XElement("Km", objeto.VehiculoCliente.Km),
                new XElement("Categoria", objeto.VehiculoCliente.Categoria)
                    )
                );

                xmlDoc.Root.Add(nuevoCliente);
                xmlDoc.Save(archivo);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el cliente: " + ex.Message);
                
                return false;
            }
        }
        private int GenerarCodigo(XDocument xmlDoc)
        {
            int ultimoCodigo = xmlDoc.Descendants("Cliente").Select(e => (int)e.Element("Codigo")).DefaultIfEmpty(0).Max();
            
            while (xmlDoc.Descendants("Cliente").Any(c => c.Element("Codigo").Value == (ultimoCodigo + 1).ToString()))
            {
                ultimoCodigo++;
            }

            return ultimoCodigo + 1;
        }

        public BECliente ListarObjeto(BECliente objeto)
        {
            throw new NotImplementedException();
        }

       
       
        public List<BECliente> ListarTodo()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

                var clientes = xmlDoc.Descendants("Cliente")
                    .Select(c => new BECliente
                    {
                        Codigo = Convert.ToInt32(c.Element("Codigo").Value),
                        Nombre = c.Element("Nombre").Value,
                        Apellido = c.Element("Apellido").Value,
                        DNI = Convert.ToInt32(c.Element("DNI").Value),
                        FechaNac = Convert.ToDateTime(c.Element("FechaNac").Value),
                        Mail = c.Element("Mail").Value,
                        VehiculoCliente = new BEVehiculo
                        {
                            Codigo = Convert.ToInt32(c.Element("VehiculoCliente").Element("Codigo").Value),
                            Marca = c.Element("VehiculoCliente").Element("Marca").Value,
                            Modelo = c.Element("VehiculoCliente").Element("Modelo").Value,
                            Patente = c.Element("VehiculoCliente").Element("Patente").Value,
                            Km = Convert.ToInt32(c.Element("VehiculoCliente").Element("Km").Value),
                            Categoria = c.Element("VehiculoCliente").Element("Categoria").Value
                        }
                    })
                    .ToList();

                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar todos los clientes: " + ex.Message);
                return new List<BECliente>();
            }
        }

        public List<object> ListarClientes()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

                var clientes = xmlDoc.Descendants("Cliente")
                    .Select(c => new
                    {
                        Codigo = Convert.ToInt32(c.Element("Codigo").Value),
                        Nombre = c.Element("Nombre").Value,
                        Apellido = c.Element("Apellido").Value,
                        DNI = Convert.ToInt32(c.Element("DNI").Value),
                        FechaNac = Convert.ToDateTime(c.Element("FechaNac").Value),
                        Mail = c.Element("Mail").Value,
                        CodVehiculo = Convert.ToInt32(c.Element("VehiculoCliente").Element("Codigo").Value),
                        ModeloVehiculo = c.Element("VehiculoCliente").Element("Modelo").Value
                    })
                    .ToList<object>();

                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar todos los clientes: " + ex.Message);
                return new List<object>();
            }
        }
        public List<BEVehiculo> ObtenerVehiculosCliente(BECliente oBECliente)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("Clientes.xml");

                var vehiculosCliente = xmlDoc.Descendants("Cliente")
                    .Where(c => c.Element("DNI").Value == oBECliente.DNI.ToString())
                    .Elements("VehiculoCliente")
                    .Select(v => new BEVehiculo
                    {
                        Codigo = Convert.ToInt32(v.Element("Codigo").Value),
                        Marca = v.Element("Marca").Value,
                        Modelo = v.Element("Modelo").Value,
                        Patente = v.Element("Patente").Value,
                        Km = Convert.ToInt32(v.Element("Km").Value),
                        Categoria = v.Element("Categoria").Value
                    })
                    .ToList();

                return vehiculosCliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener vehículos del cliente: " + ex.Message);
                return new List<BEVehiculo>();
            }
        }
    }
}
