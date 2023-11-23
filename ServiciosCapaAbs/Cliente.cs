using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace _2doParcialLUG
{
    public partial class Cliente : Form
    {
        public Cliente()
        {
            InitializeComponent();
            oBECliente = new BECliente();
            oBLLCliente = new BLLCliente();
            oBEVehiculo = new BEVehiculo();
            oBLLVehiculo = new BLLVehiculo();
        }

        BECliente oBECliente;
        BLLCliente oBLLCliente;
        BEVehiculo oBEVehiculo;
        BLLVehiculo oBLLVehiculo;

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void CargarDTGV()
        {
            dataGridView1.DataSource = oBLLCliente.ListarClientes();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCoral;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Refresh();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Asignar();
            oBLLCliente.Guardar(oBECliente);
            CargarDTGV();

        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            
            comboBox1.ValueMember = null;
            comboBox1.DisplayMember = "Modelo";
            comboBox1.DataSource = oBLLVehiculo.ListarTodo();
            CargarDTGV();
            
            
        }

        private void Asignar()
        {

            oBECliente = new BECliente
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                DNI = Convert.ToInt32(txtDNI.Text),
                FechaNac = dateTimePicker1.Value,
                Mail = txtMail.Text
            };

            if (!ValidarFormatoEmail(oBECliente.Mail))
            {
                MessageBox.Show("El formato del correo electrónico no es válido (debe contener '@' Y .com).", "Error");
                return; 
            }

            oBEVehiculo = comboBox1.SelectedItem as BEVehiculo;
            oBECliente.VehiculoCliente = oBEVehiculo;




        }
        private bool ValidarFormatoEmail(string email)
        {

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
           
            BECliente clienteModificado = new BECliente
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                DNI = Convert.ToInt32(txtDNI.Text),
                FechaNac = dateTimePicker1.Value,
                Mail = txtMail.Text,
                VehiculoCliente = comboBox1.SelectedItem as BEVehiculo
            };

           
            if (oBLLCliente.Modificar(clienteModificado))
            {
                MessageBox.Show("Cliente modificado exitosamente.");
                CargarDTGV(); 
            }
            else
            {
                MessageBox.Show("Error al modificar el cliente.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            List<BECliente> clientes = oBLLCliente.ListarTodo();
            BECliente clienteSeleccionado = clientes[dataGridView1.SelectedRows[0].Index];

           
            txtNombre.Text = clienteSeleccionado.Nombre;
            txtApellido.Text = clienteSeleccionado.Apellido;
            txtDNI.Text = clienteSeleccionado.DNI.ToString();
            dateTimePicker1.Value = clienteSeleccionado.FechaNac;
            txtMail.Text = clienteSeleccionado.Mail;
            comboBox1.SelectedItem = clienteSeleccionado.VehiculoCliente;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
               
                List<BECliente> clientes = oBLLCliente.ListarTodo();
                if (dataGridView1.SelectedRows.Count > 0)
                {
                   
                    BECliente clienteBaja = clientes[dataGridView1.SelectedRows[0].Index];

                    
                    if (oBLLCliente.Baja(clienteBaja))
                    {
                        MessageBox.Show("Cliente dado de baja exitosamente.");
                        CargarDTGV(); 
                    }
                    else
                    {
                        MessageBox.Show("Error al dar de baja el cliente.");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente para dar de baja.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dar de baja el cliente: " + ex.Message);
            }
        }
    }
}
