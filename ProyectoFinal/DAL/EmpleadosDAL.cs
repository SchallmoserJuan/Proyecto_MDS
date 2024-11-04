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
    internal class EmpleadosDAL
    {
        // Creando propiedad
        conexionDAL conexion = new conexionDAL();

        // Constructor
        public EmpleadosDAL()
        {
            conexion = new conexionDAL();
        }

        public bool Agregar(EmpleadosBLL empleado)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO empleados (nombre, primerapellido, segundoapellido, correo, departamento, foto) VALUES (@Nombre, @PrimerApellido, @SegundoApellido, @Correo, @Departamento, @FotoEmpleado)"))
                {
                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@PrimerApellido", empleado.PrimerApellido);
                    cmd.Parameters.AddWithValue("@SegundoApellido", empleado.SegundoApellido);
                    cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                    cmd.Parameters.AddWithValue("@Departamento", empleado.Departamento);
                    cmd.Parameters.AddWithValue("@FotoEmpleado", empleado.FotoEmpleado);

                    return conexion.ejecutarComandoSinRetornoDatos(cmd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar empleado: {ex.Message}");
                return false;
            }
        }

        public bool Modificar(EmpleadosBLL oEmpleadosBLL)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE empleados SET nombre = @Nombre, primerapellido = @PrimerApellido, segundoapellido = @SegundoApellido, correo = @Correo, departamento = @Departamento, foto = @FotoEmpleado WHERE id = @ID"))
                {
                    cmd.Parameters.AddWithValue("@ID", oEmpleadosBLL.ID);
                    cmd.Parameters.AddWithValue("@Nombre", oEmpleadosBLL.Nombre);
                    cmd.Parameters.AddWithValue("@PrimerApellido", oEmpleadosBLL.PrimerApellido);
                    cmd.Parameters.AddWithValue("@SegundoApellido", oEmpleadosBLL.SegundoApellido);
                    cmd.Parameters.AddWithValue("@Correo", oEmpleadosBLL.Correo);
                    cmd.Parameters.AddWithValue("@Departamento", oEmpleadosBLL.Departamento);
                    cmd.Parameters.AddWithValue("@FotoEmpleado", oEmpleadosBLL.FotoEmpleado);

                    return conexion.ejecutarComandoSinRetornoDatos(cmd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar empleado: {ex.Message}");
                return false;
            }
        }


        //Metodo
        public DataSet MostrarEmpleados()
        {
            MySqlCommand sentencia = new MySqlCommand("SELECT * FROM empleados");
            return conexion.EjecutarSentencia(sentencia);
        }


    }


}
