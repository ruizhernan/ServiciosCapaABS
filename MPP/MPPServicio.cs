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
        public class MPPServicio : IGestor<BEServicio>
        {
            string archivo = "Servicios.xml";
        public bool Baja(BEServicio objeto)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

               
                XElement servicioEliminar = xmlDoc.Descendants()
                    .Where(e => e.Name.LocalName == "Servicio" || e.Name.LocalName == "ServicioPremium")
                    .FirstOrDefault(e => Convert.ToDateTime(e.Element("Fecha").Value) == objeto.Fecha);

                if (servicioEliminar != null)
                {
                    
                    servicioEliminar.Remove();

                    
                    xmlDoc.Save(archivo);
                    return true;
                }

                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al dar de baja el servicio: " + ex.Message);
                return false;
            }
        }

        public bool Modificar (BEServicio objeto)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

                // Buscar el elemento que coincide con la fecha del servicio a modificar
                XElement servicioModificar = xmlDoc.Descendants()
                    .Where(e => e.Name.LocalName == "Servicio" || e.Name.LocalName == "ServicioPremium")
                    .FirstOrDefault(e => Convert.ToDateTime(e.Element("Fecha").Value) == objeto.Fecha);

                if (servicioModificar != null)
                {
                    // Actualizar los valores en el elemento
                    servicioModificar.Element("Aceite").Value = objeto.Aceite;
                    servicioModificar.Element("LitrosAceite").Value = objeto.LitrosAceite.ToString();
                    servicioModificar.Element("Precio").Value = objeto.Precio.ToString();

                    // Guardar los cambios en el archivo
                    xmlDoc.Save(archivo);
                    return true;
                }

                return false; // Si no se encontró el servicio a modificar
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el servicio: " + ex.Message);
                return false;
            }
        }

            public bool Guardar(BEServicio objeto)
            {
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

                
                     XElement nuevoServicio;

                if (objeto is BEServicioPremium servicioPremium)
                {
                    nuevoServicio = new XElement("ServicioPremium",
                        new XElement("Fecha", objeto.Fecha.ToString("yyyy-MM-dd")),
                        new XElement("Cliente",
                            new XElement("Nombre", servicioPremium.ClienteServicio.Nombre),
                            new XElement("Apellido", servicioPremium.ClienteServicio.Apellido),
                            new XElement("DNI", servicioPremium.ClienteServicio.DNI),
                            new XElement("FechaNac", servicioPremium.ClienteServicio.FechaNac.ToString("yyyy-MM-dd")),
                            new XElement("Mail", servicioPremium.ClienteServicio.Mail)
                        ),
                        new XElement("Aceite", servicioPremium.Aceite),
                        new XElement("LitrosAceite", servicioPremium.LitrosAceite),
                        new XElement("Precio", servicioPremium.Precio),
                        new XElement("Filtro", servicioPremium.Filtro)
                    );
                }
                else
                {
                    nuevoServicio = new XElement("Servicio",
                        new XElement("Fecha", objeto.Fecha.ToString("yyyy-MM-dd")),
                        new XElement("Cliente",
                            new XElement("Nombre", objeto.ClienteServicio.Nombre),
                            new XElement("Apellido", objeto.ClienteServicio.Apellido),
                            new XElement("DNI", objeto.ClienteServicio.DNI),
                            new XElement("FechaNac", objeto.ClienteServicio.FechaNac.ToString("yyyy-MM-dd")),
                            new XElement("Mail", objeto.ClienteServicio.Mail)
                        ),
                        new XElement("Aceite", objeto.Aceite),
                        new XElement("LitrosAceite", objeto.LitrosAceite),
                        new XElement("Precio", objeto.Precio)
                    );
                }

                xmlDoc.Root.Add(nuevoServicio);
                xmlDoc.Save(archivo);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el servicio: " + ex.Message);
                return false;
            }
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
            try
            {
                XDocument xmlDoc = XDocument.Load(archivo);

                var servicios = xmlDoc.Descendants()
            .Where(e => e.Name.LocalName == "Servicio" || e.Name.LocalName == "ServicioPremium")
            .Select(s => new
            {
                Tipo = s.Name.LocalName,
                Fecha = Convert.ToDateTime(s.Element("Fecha").Value),
                ClienteNombre = s.Element("Cliente").Element("Nombre").Value,
                Aceite = s.Element("Aceite").Value,
                LitrosAceite = Convert.ToDecimal(s.Element("LitrosAceite").Value),
                Precio = Convert.ToDecimal(s.Element("Precio").Value)
            })
            .ToList<object>();

                return servicios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar todos los servicios: " + ex.Message);
                return new List<object>();
            }
        }
        public List<BEServicio> ListarServiciosBEServicio()
        {
           
            var servicios = ListarServicios();

           
            var serviciosBEServicio = servicios.Select(obj =>
            {
               
                BEServicio servicio = new BEServicio();

               
                servicio.Fecha = (DateTime)obj.GetType().GetProperty("Fecha").GetValue(obj, null);
                servicio.Aceite = (string)obj.GetType().GetProperty("Aceite").GetValue(obj, null);
                servicio.LitrosAceite = (decimal)obj.GetType().GetProperty("LitrosAceite").GetValue(obj, null);
                servicio.Precio = (decimal)obj.GetType().GetProperty("Precio").GetValue(obj, null);

               
                servicio.ClienteServicio = new BECliente
                {
                  
                    Nombre = (string)obj.GetType().GetProperty("ClienteNombre").GetValue(obj, null),
                   
                };

                return servicio;
            }).ToList();

            return serviciosBEServicio;
        }

    }
    }
