using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace gestionInventario1
{
    public partial class Form1 : Form
    {

        Stock stockNuevo;
        

        public Form1()
        {
            InitializeComponent();
            LlenarGrilla();
            txtCod.Enabled = false;

            btnModificar.Enabled = false;

            limpiar();

        }
        private void btnPrueba_Click(object sender, EventArgs e)
        {
            
          this.Close();
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
        private void btnCargar_Click(object sender, EventArgs e)
        {
            ClsProductos1 objProductos = new ClsProductos1();
            Stock nuevoStock = guardarDatos(); // Guarda el nuevo stock
            objProductos.Agregar(nuevoStock); // Agrega el nuevo producto a la base de datos

            // Actualiza la grilla
            objProductos.Listar(Grilla);

            // Llenar el gráfico, pasando el nuevo stock
            LlenarChart(nuevoStock); // Pasa el objeto nuevoStock como argumento

            limpiar(); // 
        }


        private void LlenarChart(Stock stockNuevo)
        {
            Series series = chart1.Series.FindByName("Stock");

            // Si la serie no existe, la creamos
            if (series == null)
            {
                series = new Series("Stock");
                series.ChartType = SeriesChartType.Pie;
                chart1.Series.Add(series);
            }
            
            // Añade el nuevo punto con el nombre del producto y la cantidad en stock
            series.Points.AddXY(stockNuevo.Nombre, stockNuevo.Cantidad); ;
        }

        private void LimpiarChart()
        {
            chart1.Series.Clear(); // Elimina todas las series del gráfico
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
          frmEliminar eliminar = new frmEliminar();
            eliminar.ShowDialog();
            LlenarGrilla();

            limpiar();

        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Modificar(guardarDatos());
            

            limpiar();
            LlenarGrilla();

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar frmBuscar = new frmBuscar();
            frmBuscar.ShowDialog();
        }
        private void seleccionar(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            int indice1 = e.RowIndex;
            Grilla.ClearSelection();

            if (indice1 >= 0)
            {
                txtCod.Text = Grilla.Rows[indice1].Cells[0].Value.ToString();
                txtNombre.Text = Grilla.Rows[indice1].Cells[1].Value.ToString();
                txtDescripcion.Text = Grilla.Rows[indice1].Cells[2].Value.ToString();
                txtPrecio.Text = Grilla.Rows[indice1].Cells[3].Value.ToString();
                txtStock.Text = Grilla.Rows[indice1].Cells[4].Value.ToString();
                txtCate.Text = Grilla.Rows[indice1].Cells[5].Value.ToString();
            }

            btnModificar.Enabled = true;
            
        }
        public void LlenarGrilla()
        {
            ClsProductos1 objProductos = new ClsProductos1();
            objProductos.Listar(Grilla);
        }
        public void limpiar()
        {
            txtCate.Clear();
            txtDescripcion.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            LimpiarChart();
        }
    }
}

