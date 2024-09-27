using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Trabajar con SQL
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ProyectoFinal.DAL
{
    internal class conexionDAL
    {
        //Creacion de metodo
        public bool PruebaConectar(){
            //Test para probar la conexion
            try {
                MySqlConnection Conexion = new MySqlConnection("server=localhost;port=3306;database=dbsistema;user=root;password=admin;");
                MySqlCommand Comando = new MySqlCommand();

                Comando.CommandText = "SELECT * FROM empleados";
                Comando.Connection = Conexion;
                Conexion.Open();
                Comando.ExecuteNonQuery(); //
                Conexion.Close();

                return true;

            } catch
            {
                return false;
            }
        }
    }
}
