using MySql.Data.MySqlClient;
using ProyectoFinal.DAL;
using System;

namespace ProyectoFinal.BLL
{
    internal class EmpleadosBLL
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public int Departamento { get; set; }
        public byte[] FotoEmpleado { get; set; }

        private EmpleadosDAL empleadosDAL = new EmpleadosDAL();

        public bool AgregarEmpleado()
        {
            try
            {
                return empleadosDAL.Agregar(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar empleado: {ex.Message}");
                return false;
            }
        }

        public bool ModificarEmpleado()
        {
            try
            {
                return empleadosDAL.Modificar(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar empleado: {ex.Message}");
                return false;
            }
        }
    }

    internal class EmpleadosDAL
    {
        private conexionDAL conexion = new conexionDAL();

        public bool Agregar(EmpleadosBLL empleado)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO empleados (id, nombre, primerapellido, segundoapellido, correo, departamento, foto) VALUES (@ID, @Nombre, @PrimerApellido, @SegundoApellido, @Correo, @Departamento, @FotoEmpleado)"))
                {
                    cmd.Parameters.AddWithValue("@ID", empleado.ID);
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

        public bool Modificar(EmpleadosBLL empleado)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE empleados SET nombre = @Nombre, primerapellido = @PrimerApellido, segundoapellido = @SegundoApellido, correo = @Correo, departamento = @Departamento, foto = @FotoEmpleado WHERE id = @ID"))
                {
                    cmd.Parameters.AddWithValue("@ID", empleado.ID);
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
                Console.WriteLine($"Error al modificar empleado: {ex.Message}");
                return false;
            }
        }
    }
}
