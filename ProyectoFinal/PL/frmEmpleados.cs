using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinal.DAL;
using ProyectoFinal.BLL;
using MySql.Data.MySqlClient;

namespace ProyectoFinal.PL
{
    /// <summary>
    /// Formulario para la gestión de empleados (CRUD operations)
    /// </summary>
    public partial class frmEmpleados : Form
    {
        // Variables para manejo de imágenes
        byte[] imagenByte;
        private byte[] imagenTemporal;

        public frmEmpleados()
        {
            InitializeComponent();
        }

        // Eventos de navegación
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Inicialización del formulario
        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            // Cargar departamentos en el ComboBox
            DepartamentosDAL objDepartamentos = new DepartamentosDAL();
            cbxDepartamento.DataSource = objDepartamentos.MostrarDepartamentos().Tables[0];
            cbxDepartamento.DisplayMember = "departamento";
            cbxDepartamento.ValueMember = "id";

            CargarDatos();

            // Configuración del DataGridView
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;
        }

        // Manejo de la imagen del empleado
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Selecciona una imagen";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picFoto.Image = Image.FromStream(ofd.OpenFile());
                MemoryStream memoria = new MemoryStream();
                picFoto.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
                imagenByte = memoria.ToArray();
            }
        }

        // Operaciones CRUD
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            RecolectarDatos();
            CargarDatos();
            LimpiarControles();
        }

        // Recolectar datos de los controles
        private void RecolectarDatos()
        {
            try
            {
                EmpleadosBLL objEmpleados = new EmpleadosBLL();

                // Validaciones de campos requeridos
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(txtPrimerApellido.Text))
                {
                    MessageBox.Show("Ingrese un primer apellido válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(txtCorreo.Text))
                {
                    MessageBox.Show("Ingrese un correo válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Asignación de valores
                objEmpleados.Nombre = txtNombre.Text;
                objEmpleados.PrimerApellido = txtPrimerApellido.Text;
                objEmpleados.SegundoApellido = txtSegundoApellido.Text;
                objEmpleados.Correo = txtCorreo.Text;

                // Validación del departamento
                if (cbxDepartamento.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un departamento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int IDDepartamento;
                if (!int.TryParse(cbxDepartamento.SelectedValue.ToString(), out IDDepartamento))
                {
                    MessageBox.Show("Departamento inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                objEmpleados.Departamento = IDDepartamento;
                objEmpleados.FotoEmpleado = imagenByte;

                // Agregar empleado
                if (objEmpleados.AgregarEmpleado())
                {
                    MessageBox.Show("Empleado agregado correctamente", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al agregar empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al recolectar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Actualización de la vista de datos
        private void CargarDatos()
        {
            DAL.EmpleadosDAL empleadosDAL = new DAL.EmpleadosDAL();
            DataSet dsEmpleados = empleadosDAL.MostrarEmpleados();
            dgvEmpleados.DataSource = dsEmpleados.Tables[0];

            if (dgvEmpleados.Columns["foto"] != null)
            {
                dgvEmpleados.Columns["foto"].Visible = false;
            }
        }

        // Limpieza de controles
        private void LimpiarControles()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtCorreo.Clear();
            cbxDepartamento.SelectedIndex = -1;
            picFoto.Image = null;
            imagenByte = null;
        }

        // Selección de empleado en el grid
        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmpleados.Rows[e.RowIndex];

                // Cargar datos en controles
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtPrimerApellido.Text = row.Cells["PrimerApellido"].Value.ToString();
                txtSegundoApellido.Text = row.Cells["SegundoApellido"].Value.ToString();
                txtCorreo.Text = row.Cells["Correo"].Value.ToString();
                cbxDepartamento.SelectedValue = row.Cells["Departamento"].Value;

                // Cargar foto
                byte[] foto = row.Cells["foto"].Value as byte[];
                if (foto != null)
                {
                    using (MemoryStream ms = new MemoryStream(foto))
                    {
                        picFoto.Image = Image.FromStream(ms);
                    }
                    imagenTemporal = foto;
                }
                else
                {
                    picFoto.Image = null;
                    imagenTemporal = null;
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarEmpleado();
            CargarDatos();
            LimpiarControles();
        }

        private void ModificarEmpleado()
        {
            try
            {
                EmpleadosBLL objEmpleados = new EmpleadosBLL();

                // Validaciones
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
                    string.IsNullOrEmpty(txtPrimerApellido.Text) || string.IsNullOrEmpty(txtCorreo.Text))
                {
                    MessageBox.Show("Todos los campos son requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Asignación de valores
                if (!int.TryParse(txtID.Text, out int codigoEmpleado))
                {
                    MessageBox.Show("ID inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                objEmpleados.ID = codigoEmpleado;
                objEmpleados.Nombre = txtNombre.Text;
                objEmpleados.PrimerApellido = txtPrimerApellido.Text;
                objEmpleados.SegundoApellido = txtSegundoApellido.Text;
                objEmpleados.Correo = txtCorreo.Text;

                // Validación del departamento
                if (!int.TryParse(cbxDepartamento.SelectedValue?.ToString(), out int IDDepartamento))
                {
                    MessageBox.Show("Seleccione un departamento válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                objEmpleados.Departamento = IDDepartamento;
                objEmpleados.FotoEmpleado = imagenByte ?? imagenTemporal;

                if (objEmpleados.ModificarEmpleado())
                {
                    MessageBox.Show("Empleado modificado correctamente", "Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al modificar empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al modificar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminación de empleado
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                int clienteId = Convert.ToInt32(dgvEmpleados.SelectedRows[0].Cells["Id"].Value);

                if (MessageBox.Show("¿Estás seguro de que deseas borrar este registro?", "Confirmar Borrado",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (BorrarClienteDeLaBaseDeDatos(clienteId))
                    {
                        dgvEmpleados.Rows.RemoveAt(dgvEmpleados.SelectedRows[0].Index);
                        MessageBox.Show("Registro eliminado correctamente.", "Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al intentar borrar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para borrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool BorrarClienteDeLaBaseDeDatos(int clienteId)
        {
            bool exito = false;
            conexionDAL conexionDAL = new conexionDAL();

            using (MySqlConnection sqlCon = conexionDAL.EstablecerConexion())
            {
                try
                {
                    sqlCon.Open();
                    MySqlCommand comando = new MySqlCommand("DELETE FROM empleados WHERE Id = @Id", sqlCon);
                    comando.Parameters.AddWithValue("@Id", clienteId);
                    exito = comando.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return exito;
        }

        // Cancelar operación actual
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            dgvEmpleados.ClearSelection();
        }

        private void LimpiarCampos()
        {
            txtID.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            cbxDepartamento.Text = string.Empty;
            picFoto.Image = null;
        }
    }
}