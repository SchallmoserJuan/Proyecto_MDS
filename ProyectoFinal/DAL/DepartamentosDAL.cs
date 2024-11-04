using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using ProyectoFinal.BLL;

namespace ProyectoFinal.DAL
{
    internal class DepartamentosDAL
    {
        //Creando propiedad
        conexionDAL conexion = new conexionDAL();

        //Constructor
        public DepartamentosDAL() {

            conexion = new conexionDAL();
        }

        public bool Agregar(DepartamentoBLL oDepartamentosBLL)
        {
            //Se utiliza para evitar concatenacion y sea siempre un string
            return conexion.ejecutarComandoSinRetornoDatos("Insert into departamentos (departamento) values ('" + oDepartamentosBLL.Departamento + "')");


        }

        //Metodo para borrar
        public bool Eliminar(DepartamentoBLL oDepartamentosBLL)
        {
            MySqlCommand MYSQLComando = new MySqlCommand("DELETE FROM departamentos WHERE ID=@ID ");
            MYSQLComando.Parameters.Add("@ID", MySqlDbType.Int32).Value = oDepartamentosBLL.ID;
            return conexion.ejecutarComandoSinRetornoDatos(MYSQLComando);
        }

        //Metodo para modificar
        public bool Modificar(DepartamentoBLL oDepartamentosBLL)
        {
            MySqlCommand MYSQLComando = new MySqlCommand("UPDATE departamentos SET departamento=@departamento WHERE ID=@ID ");
            MYSQLComando.Parameters.Add("@departamento", MySqlDbType.VarChar).Value = oDepartamentosBLL.Departamento;
            MYSQLComando.Parameters.Add("@ID", MySqlDbType.Int32).Value = oDepartamentosBLL.ID;

            return conexion.ejecutarComandoSinRetornoDatos(MYSQLComando);
        }

        //Metodo
        public DataSet MostrarDepartamentos()
        {
            MySqlCommand sentencia = new MySqlCommand("SELECT * FROM departamentos");
            return conexion.EjecutarSentencia(sentencia);
        }
    }
}
