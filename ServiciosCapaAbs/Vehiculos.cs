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
using System.Windows.Forms;

namespace _2doParcialLUG
{
    public partial class Vehiculos : Form
    {
        public Vehiculos()
        {
            InitializeComponent();
            oBEVehiculo = new BEVehiculo();
            oBLLVehiculo = new BLLVehiculo();
        }

        BEVehiculo oBEVehiculo;
        BLLVehiculo oBLLVehiculo;

        private void Vehiculos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCoral;

            dataGridView1.DataSource = oBLLVehiculo.ListarTodo();
           
        }
    }
}
