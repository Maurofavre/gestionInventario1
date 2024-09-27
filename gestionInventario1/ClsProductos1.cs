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

        //private string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\\Users\\mauro\\source\\repos\\gestionInventario1\\gestionInventario1\\baseDatos\\inventario.accdb";
        private string cadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=baseDatos\inventario.accdb";
        
       private string Tabla = "Productos";

        public void conexiones()    
        {
            //recibe la cadena de conexion
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();

            // Asocia el comando SQL a la conexión y define el tipo de comando (SQL)
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;

        }


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
        public void ProbarConexion()
{
            try
            {
                // Configurar la cadena de conexión
                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
                MessageBox.Show("Conexión a la base de datos exitosa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión si está abierta
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    
        public void Agregar(Stock stock)
        {
            try
            {
               conexiones();

                //Se edita lp que es la query 
                string query = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Categoria) VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Categoria)";

                comando.CommandText = query;
                // Asignar valores a los parámetros
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@Nombre", stock.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", stock.Descripcion);
                comando.Parameters.AddWithValue("@Precio", stock.Precio);
                comando.Parameters.AddWithValue("@Stock", stock.Cantidad);
                comando.Parameters.AddWithValue("@Categoria", stock.Categoria);

                // Ejecuta el comando (INSERT INTO) para insertar los datos en la base de datos
                comando.ExecuteNonQuery();

                MessageBox.Show("Producto agregado correctamente.");

            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR EN BD " + e.ToString());
            }   
            finally
            {
                conexion.Close() ;
            }
        }
        public void Eliminar(Stock stock)
        {
            try
            {
                conexiones(); // Método para abrir la conexión con la base de datos
                comando.CommandText = "DELETE FROM Productos WHERE Codigo = ?";

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("?", stock.Id); // Pasar el parámetro del código del producto

                comando.ExecuteNonQuery(); // Ejecutar el comando

                MessageBox.Show("Producto eliminado correctamente.");
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR EN BD " + e.ToString());
            }
            finally
            {
                conexion.Close(); // Asegúrate de cerrar la conexión después de ejecutar el comando
            }
        }
        public void Modificar(Stock stock)
            {
                try
                {
                conexiones();

                //Query para modificar 
                comando.CommandText = "UPDATE Productos SET Nombre = ?, Descripcion = ?, Precio = ?, Stock = ?, Categoria = ? WHERE Codigo = ?";
                
                //Se actualizan los valores de los campos
                comando.Parameters.AddWithValue("?", stock.Nombre);
                comando.Parameters.AddWithValue("?", stock.Descripcion);
                comando.Parameters.AddWithValue("?", stock.Precio);
                comando.Parameters.AddWithValue("?", stock.Cantidad);
                comando.Parameters.AddWithValue("?", stock.Categoria);
                comando.Parameters.AddWithValue("?", stock.Id); // Código del producto que se va a modificar

                // Ejecuta el comando (Update INTO) para insertar los datos en la base de datos
                comando.ExecuteNonQuery();

                    MessageBox.Show("Modificado correctamente");

                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR EN BD " + e.ToString());
                }
                finally
                {
                    conexion.Close();
                }
            }

        public DataTable BuscarPorNombre(string nombre)
        {
            conexiones(); // Método que abre la conexión

            string query = "SELECT * FROM Productos WHERE Nombre = @Nombre";
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Nombre", nombre);

            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataTable resultados = new DataTable();
            adaptador.Fill(resultados); // Llena el DataTable con los resultados de la consulta

            conexion.Close(); // Cierra la conexión
            return resultados; // Retorna los resultados
        }

        public DataTable BuscarPorCodigo(int codigo)
        {
            conexiones(); // Método que abre la conexión

            string query = "SELECT * FROM Productos WHERE Codigo = @Codigo";
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Nombre", codigo);

            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataTable resultados = new DataTable();
            adaptador.Fill(resultados); // Llena el DataTable con los resultados de la consulta

            conexion.Close(); // Cierra la conexión
            return resultados; // Retorna los resultados
        }


        public DataTable BuscarPorCate(string categoria)
        {
            conexiones(); // Método que abre la conexión

            string query = "SELECT * FROM Productos WHERE Categoria = @Cate";
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Cate", categoria);

            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataTable resultados = new DataTable();
            adaptador.Fill(resultados); // Llena el DataTable con los resultados de la consulta

            conexion.Close(); // Cierra la conexión
            return resultados; // Retorna los resultados
        }

        //Listar los datos para mi chart
        //conectandome a la base de datos y sacando los datos de mi tabla
        public DataTable ListarData()
        {
            conexiones();
            DataTable dt = new DataTable();
            string query = "SELECT Nombre, Categoria FROM Productos";
            comando.CommandText = query;
            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            adaptador.Fill(dt);
            conexion.Close();
            return dt;
        }

    }
}
