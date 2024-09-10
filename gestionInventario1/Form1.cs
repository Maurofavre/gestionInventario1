using System;
using System.Collections;
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
    public partial class Form1 : Form
    {

        Stock stockNuevo;

        public Form1()
        {
            InitializeComponent();
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Listar(Grilla);
            
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Listar(Grilla);
            objProductos.ProbarConexion(); 
        }

        private Stock guardarDatos()
        {
            Stock stockNuevo = new Stock();

            int codigoStock = 1;
            //si tiene codigo lo usa, sino le pone 1 
            int.TryParse(txtCod.Text, out codigoStock);


            stockNuevo.Id = codigoStock;
            stockNuevo.Nombre = txtNombre.Text;
            stockNuevo.Descripcion = txtDescripcion.Text;
            stockNuevo.Precio = int.Parse(txtPrecio.Text);
            stockNuevo.Cantidad = int.Parse(txtStock.Text);
            stockNuevo.Categoria = txtCate.Text;

            return stockNuevo;
        }
        private Stock eliminarDatos()
        {
            Stock stockNuevo = new Stock();

            int codigoStock = 1;
            //si tiene codigo lo usa, sino le pone 1 
            int.TryParse(txtCod.Text, out codigoStock);


            stockNuevo.Id = codigoStock;
            

            return stockNuevo;
        }


        private void btnCargar_Click(object sender, EventArgs e)
        {
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Agregar(guardarDatos());
            objProductos.Listar(Grilla);
            
            limpiar();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Eliminar(eliminarDatos());
            objProductos.Listar(Grilla);
            limpiar();
        }
        public void limpiar()
        {
            txtCate.Clear(); 
            txtDescripcion.Clear(); 
            txtNombre.Clear();
            txtPrecio.Clear();  
            txtStock.Clear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Modificar(guardarDatos());
            objProductos.Listar(Grilla);

            limpiar();

        }
    }
}
