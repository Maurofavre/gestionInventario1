using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregados 
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms; 
   

namespace gestionInventario1
{
    internal class ClsProductos1
    {
        //creamos objeto para conectarnos con la bd
        private OleDbConnection conexion = new OleDbConnection();
        //para enviar las ordenes a la bd 
        private OleDbCommand comando = new OleDbCommand();
        //nos sirve para adaptar los datos que estan mal en la bd  
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = baseDatos\\inventario.accdb";
        private string Tabla = "Productos";


        public void Listar(DataGridView Grilla)
        {
            try
            {
                //recibe la cadena de conexion
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                //comandos de ordenes
                comando.Connection = conexion;
                //ponemos el tipo de comando, (3 tipos
                //text(envia instrucciones sql),
                //tabletDirect(trae la tabla))
                comando.CommandType = CommandType.TableDirect;
                //nombre de la tabla que vamos a traer 
                comando.CommandText = Tabla;



                //adaptamos el comando configurado
                adaptador = new OleDbDataAdapter(comando);
                //objeto de clase dataset para poder cargar los datos 
                DataSet ds = new DataSet();
                //adaptamos el dataset
                adaptador.Fill(ds);

                //dATAsOURCE TOMA EL CONTENDIDO COMPLETO DEL DATASET
                Grilla.DataSource = ds.Tables[0];

                //cerramos la conexion 
                conexion.Close();
            }
            catch(Exception e ) 
            {
                MessageBox.Show("ERROR EN BD " + e.ToString());
            }
        }

        public void Agregar(Stock stock)
        {
            try
            {
                //recibe la cadena de conexion
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                // Asocia el comando SQL a la conexión y define el tipo de comando (SQL)
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;


                comando.CommandText = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Categoria) VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Categoria)";

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@Nombre", stock.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", stock.Descripcion);
                comando.Parameters.AddWithValue("@Precio", stock.Precio);
                comando.Parameters.AddWithValue("@Stock", stock.Cantidad);
                comando.Parameters.AddWithValue("@Categoria", stock.Categoria);


                comando.CommandText = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Categoria) VALUES (?, ?, ?, ?, ?)";

                // Usa parámetros en el orden correcto
                comando.Parameters.AddWithValue("?", stock.Nombre);
                comando.Parameters.AddWithValue("?", stock.Descripcion);
                comando.Parameters.AddWithValue("?", stock.Precio);
                comando.Parameters.AddWithValue("?", stock.Cantidad);
                comando.Parameters.AddWithValue("?", stock.Categoria);

                // Ejecuta el comando (INSERT INTO) para insertar los datos en la base de datos
                comando.ExecuteNonQuery();

                // Cerrar la conexión
                conexion.Close();

                MessageBox.Show("Producto agregado correctamente.");

            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR EN BD " + e.ToString());
            }   
        }

        public void Eliminar(Stock stock)
        {
            try
            {
                //recibe la cadena de conexion
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();

                // Asocia el comando SQL a la conexión y define el tipo de comando (SQL)
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;


                comando.CommandText = "DELETE FROM Productos where Codigo = ?";

                comando.Parameters.AddWithValue("?", stock.Id);
           

                // Ejecuta el comando (INSERT INTO) para insertar los datos en la base de datos
                comando.ExecuteNonQuery();

                // Cerrar la conexión
                conexion.Close();

                MessageBox.Show("Eliminado Correctamente.");

            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR EN BD " + e.ToString());
            }
        }


    }
}
