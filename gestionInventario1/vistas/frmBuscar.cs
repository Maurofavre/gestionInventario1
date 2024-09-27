using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestionInventario1
{
    public partial class frmBuscar : Form
    {
        public frmBuscar()
        {
            InitializeComponent();


            radioNombre.CheckedChanged += BuscarPor;
            radioCodigo.CheckedChanged += BuscarPor;
            radioCateg.CheckedChanged += BuscarPor; 

        }


        private void BuscarPor(object sender, EventArgs e)
        {
            if (radioNombre.Checked)
            {
                txtNombre.Enabled = true;
                txtCateg.Enabled = false;
                txtCodigo.Enabled = false;

            }
            else if (radioCateg.Checked)
            {
                txtNombre.Enabled = false;
                txtCateg.Enabled = true;
                txtCodigo.Enabled = false;

            }
            else if (radioCodigo.Checked)
            {
                txtNombre.Enabled = false;
                txtCateg.Enabled = false;
                txtCodigo.Enabled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            // Instancia para la conexion
            ClsProductos1 conexion = new ClsProductos1();
            // DataTable para almacenar datos de la BD 
            DataTable resultadoBusqueda = new DataTable();



            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                string nombre = txtNombre.Text;
                resultadoBusqueda = conexion.BuscarPorNombre(nombre); // Busca por nombre

            }

            if (!string.IsNullOrWhiteSpace(txtCodigo.Text) && int.TryParse(txtCodigo.Text, out int codigo))
            {
                resultadoBusqueda = conexion.BuscarPorCodigo(codigo); // Busca por teléfono
            }


            if (!string.IsNullOrWhiteSpace(txtCateg.Text))
            {
                string categoria = txtCateg.Text;
                resultadoBusqueda = conexion.BuscarPorCate(categoria);

            }
            // Mostrar los resultados en el DataGridView
            dgvBuscar.DataSource = resultadoBusqueda;

        }
    }
}
