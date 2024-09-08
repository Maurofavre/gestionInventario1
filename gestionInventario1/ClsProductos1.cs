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

    }
}
