    using BE;
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

    namespace _2doParcialLUG
    {
    public partial class Servicios : Form
    {
        public Servicios()
        {
            InitializeComponent();
            oBECliente = new BECliente();
            oBLLCliente = new BLLCliente();
            oBEVehiculo = new BEVehiculo();
            oBLLVehiculo = new BLLVehiculo();
            oBEServicio = new BEServicio();
            oBLLServicio = new BLLServicio();
            oBLLServicioPremium = new BLLServicioPremium();
        }

        BECliente oBECliente;
        BLLCliente oBLLCliente;
        BEVehiculo oBEVehiculo;
        BLLVehiculo oBLLVehiculo;
        BEServicio oBEServicio;
        BLLServicio oBLLServicio;
        BLLServicioPremium oBLLServicioPremium;
        decimal precioAceiteSemi = 4000;
        decimal precioAceiteSintetico = 6000;

        private void Servicios_Load(object sender, EventArgs e)
        {

           

            cmbCliente.ValueMember = "DNI";
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.DataSource = oBLLCliente.ListarTodo();

            cmbAceite.Items.Add("Semi");
            cmbAceite.Items.Add("Sintetico");

            CargarDTGV();


        }

        private void CargarDTGV()
        {
            dataGridView1.DataSource = oBLLServicio.ListarServicios();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCoral;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Refresh();
            MostrarEstadisticas();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
        }

        private void Asignar()
        {
            decimal precioAceiteSemi = 4000;
            decimal precioAceiteSintetico = 6000;
            oBEServicio = new BEServicio
            {
                Fecha = dtpFecha.Value,
                Aceite = cmbAceite.SelectedItem.ToString(),
                LitrosAceite = Convert.ToDecimal(txtLitros.Text),
            
                
            };
            if (oBEServicio.Aceite == "Semi" )
            {
                
                decimal total = precioAceiteSemi * oBEServicio.LitrosAceite;
                oBEServicio.Precio = total;
                decimal dto = oBLLServicio.CalcularDescuento(oBEServicio);
                
                oBEServicio.Precio = total - dto; 
            }
            else
            {
                decimal total = precioAceiteSintetico * oBEServicio.LitrosAceite;
                oBEServicio.Precio = total;
                oBEServicio.Precio = total - oBLLServicio.CalcularDescuento(oBEServicio);
            }
                

                oBECliente = cmbCliente.SelectedItem as BECliente;
            oBEServicio.ClienteServicio = oBECliente;
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
           
            List<BEServicio> serviciosBEServicio = oBLLServicio.ListarServiciosBEServicio();
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                BEServicio servicioBaja = serviciosBEServicio[dataGridView1.SelectedRows[0].Index];

              
                if (oBLLServicio.Baja(servicioBaja))
                {
                    MessageBox.Show("Servicio dado de baja exitosamente.");
                    CargarDTGV(); 
                }
                else
                {
                    MessageBox.Show("Error al dar de baja el servicio.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un servicio para dar de baja.");
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            List<BEServicio> serviciosBEServicio = oBLLServicio.ListarServiciosBEServicio();
            BEServicio servicioModif = serviciosBEServicio[dataGridView1.SelectedRows[0].Index];
           
            cmbCliente.SelectedItem = servicioModif.ClienteServicio.Nombre;
            cmbAceite.SelectedItem = servicioModif.Aceite;
            txtLitros.Text = servicioModif.LitrosAceite.ToString();

            
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Asignar();

                oBEServicio = new BEServicio
                {
                    Fecha = dtpFecha.Value,
                    Aceite = cmbAceite.SelectedItem.ToString(),
                    LitrosAceite = Convert.ToDecimal(txtLitros.Text),


                };
                if (oBEServicio.Aceite == "Semi")
                {

                    decimal total = precioAceiteSemi * oBEServicio.LitrosAceite;
                    oBEServicio.Precio = total;
                    decimal dto = oBLLServicio.CalcularDescuento(oBEServicio);

                    oBEServicio.Precio = total - dto;
                }
                else
                {
                    decimal total = precioAceiteSintetico * oBEServicio.LitrosAceite;
                    oBEServicio.Precio = total;
                    oBEServicio.Precio = total - oBLLServicio.CalcularDescuento(oBEServicio);
                }


                oBECliente = cmbCliente.SelectedItem as BECliente;
                oBEServicio.ClienteServicio = oBECliente;
                if (radioButton2.Checked)
                {
                    BEServicioPremium servicioPremium = new BEServicioPremium
                    {
                        Fecha = oBEServicio.Fecha,
                        ClienteServicio = oBEServicio.ClienteServicio,
                        Aceite = oBEServicio.Aceite,
                        LitrosAceite = oBEServicio.LitrosAceite,
                        Precio = oBEServicio.Precio,
                        Filtro = true
                    };
                    if (servicioPremium.Aceite == "Semi")
                    {

                        decimal total = precioAceiteSemi * oBEServicio.LitrosAceite;
                        servicioPremium.Precio = total;
                        decimal dto = oBLLServicioPremium.CalcularDescuento(servicioPremium);
                        MessageBox.Show(" " + dto);
                        servicioPremium.Precio = total - dto;
                    }
                    else
                    {
                        decimal total = precioAceiteSintetico * oBEServicio.LitrosAceite;
                        servicioPremium.Precio = total;
                        servicioPremium.Precio = total - oBLLServicioPremium.CalcularDescuento(servicioPremium);
                    }

                    oBLLServicio.Guardar(servicioPremium);
                }
                else
                {

                    oBLLServicio.Guardar(oBEServicio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el servicio: " + ex.Message);
            }
            CargarDTGV();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void MostrarEstadisticas()
        {
           
            List<BEServicio> servicios = oBLLServicio.ListarServiciosBEServicio();

           
            var estadisticas = CalcularEstadisticas(servicios);

           
            ConfigurarChart(estadisticas);
        }
        private (string TipoMasContratado, decimal MontoMasContratado, string TipoMenosContratado, decimal MontoMenosContratado) CalcularEstadisticas(List<BEServicio> servicios)
        {
            
            var servicioMasContratado = servicios
                .GroupBy(s => s.Aceite)  
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

          
            var servicioMenosContratado = servicios
                .GroupBy(s => s.Aceite)  
                .OrderBy(g => g.Count())
                .FirstOrDefault();

           

            return (
                TipoMasContratado: servicioMasContratado?.Key ?? "N/A",
                MontoMasContratado: servicioMasContratado?.Sum(s => s.Precio) ?? 0,
                TipoMenosContratado: servicioMenosContratado?.Key ?? "N/A",
                MontoMenosContratado: servicioMenosContratado?.Sum(s => s.Precio) ?? 0
            );
        }
        private void ConfigurarChart((string TipoMasContratado, decimal MontoMasContratado, string TipoMenosContratado, decimal MontoMenosContratado) estadisticas)
        {
           
            chart1.Series.Clear();

           
            Series serieMasContratado = new Series("ServicioMasContratado");
            Series serieMenosContratado = new Series("ServicioMenosContratado");

           
            serieMasContratado.Points.AddXY(estadisticas.TipoMasContratado, estadisticas.MontoMasContratado);
            serieMasContratado.Color = Color.Blue; 
            serieMasContratado.Label = estadisticas.TipoMasContratado;

            
            serieMenosContratado.Points.AddXY(estadisticas.TipoMenosContratado, estadisticas.MontoMenosContratado);
            serieMenosContratado.Color = Color.Red;
            serieMenosContratado.Label = estadisticas.TipoMenosContratado;

           
            chart1.Series.Add(serieMasContratado);
            chart1.Series.Add(serieMenosContratado);

          
            chart1.ChartAreas[0].AxisX.Title = "Tipo de Servicio";
            chart1.ChartAreas[0].AxisY.Title = "Monto";

            
            chart1.Update();
        }


    }
    
    }
