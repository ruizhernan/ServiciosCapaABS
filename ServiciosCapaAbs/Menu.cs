using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2doParcialLUG
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void listarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cliente oCliente = new Cliente();
            oCliente.MdiParent = this;
            oCliente.Show();
            oCliente.WindowState = FormWindowState.Maximized;
        }

        private void listarVehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vehiculos oVehiculos = new Vehiculos();
            oVehiculos.MdiParent = this;
            oVehiculos.Show();
            oVehiculos.WindowState = FormWindowState.Maximized;
        }

        private void crearNuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicios oServicios = new Servicios();
            oServicios.MdiParent = this;
            oServicios.Show();
            oServicios.WindowState = FormWindowState.Maximized;
        }

        private void verServiciosRealizadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
