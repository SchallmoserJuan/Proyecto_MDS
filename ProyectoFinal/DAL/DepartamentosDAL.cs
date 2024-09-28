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
            return conexion.ejecutarComandoSinRetornoDatos("Insert into departamentos (departamento) values ('"+oDepartamentosBLL.Departamento +"')");
        }

        //Metodo para borrar
        public int Eliminar(DepartamentoBLL oDepartamentosBLL)
        {
            conexion.ejecutarComandoSinRetornoDatos("DELETE FROM Departamentos WHERE ID ="+oDepartamentosBLL.ID);

            return 1;
        }

        //Metodo
        public DataSet MostrarDepartamentos()
        {
            MySqlCommand sentencia = new MySqlCommand("SELECT * FROM departamentos");
            return conexion.EjecutarSentencia(sentencia);
        }
    }
}
