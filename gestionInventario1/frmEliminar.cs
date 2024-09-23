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
    public partial class frmEliminar : Form
    {
        public frmEliminar()
        {
            InitializeComponent();
        }

        // Método para crear el objeto Stock con los datos a eliminar

        private Stock eliminarDatos()
        {
            Stock stockNuevo = new Stock();

            int codigoStock = 1;
            //si tiene codigo lo usa, sino le pone 1 
            int.TryParse(txtCodigo.Text, out codigoStock);


            stockNuevo.Id = codigoStock;


            return stockNuevo;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            ClsProductos1 produc = new ClsProductos1();
            produc.Eliminar(eliminarDatos());

            txtCodigo.Clear();
            
        }
    }
}