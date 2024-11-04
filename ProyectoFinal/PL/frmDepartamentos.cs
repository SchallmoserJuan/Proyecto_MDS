using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinal.BLL;
using ProyectoFinal.DAL;

namespace ProyectoFinal.PL
{
    /// <summary>
    /// Formulario para la gestión de departamentos (CRUD operations)
    /// </summary>
    public partial class frmDepartamentos : Form
    {
        // Objeto para acceso a datos de departamentos
        private readonly DepartamentosDAL oDepartamentosDAL;


        // Constructor del formulario. Inicializa componentes y carga datos iniciales
        public frmDepartamentos()
        {
            oDepartamentosDAL = new DepartamentosDAL();
            InitializeComponent();
            LlenarGrid();
            LimpiarEntradas();
        }

        // Operaciones CRUD


        // Maneja el evento de agregar un nuevo departamento
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            oDepartamentosDAL.Agregar(RecuperarInformacion());
            LlenarGrid();
            LimpiarEntradas();
        }


        /// Recupera la información ingresada en el formulario
        // Objeto DepartamentoBLL con la información del departamento
        private DepartamentoBLL RecuperarInformacion()
        {
            var oDepartamentoBLL = new DepartamentoBLL();

            // Intenta convertir el ID si existe
            int.TryParse(txtID.Text, out int ID);
            oDepartamentoBLL.ID = ID;
            oDepartamentoBLL.Departamento = txtNombre.Text;

            return oDepartamentoBLL;
        }


        // Maneja la selección de una fila en el DataGridView
        private void Seleccionar(object sender, DataGridViewCellMouseEventArgs e)
        {
            int indice = e.RowIndex;
            dgvDepartamentos.ClearSelection();

            if (indice >= 0)
            {
                // Cargar datos en los controles
                txtID.Text = dgvDepartamentos.Rows[indice].Cells[0].Value.ToString();
                txtNombre.Text = dgvDepartamentos.Rows[indice].Cells[1].Value.ToString();

                // Actualizar estado de los botones
                ActualizarEstadoBotones(false, true, true, true);
            }
        }


        // Elimina el departamento seleccionado
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            oDepartamentosDAL.Eliminar(RecuperarInformacion());
            LlenarGrid();
            LimpiarEntradas();
        }


        // Modifica el departamento seleccionado
        private void btnModificar_Click(object sender, EventArgs e)
        {
            oDepartamentosDAL.Modificar(RecuperarInformacion());
            LlenarGrid();
            LimpiarEntradas();
        }

        // Métodos de utilidad


        // Actualiza el DataGridView con los datos de departamentos
        public void LlenarGrid()
        {
            dgvDepartamentos.DataSource = oDepartamentosDAL.MostrarDepartamentos().Tables[0];

            // Configurar encabezados
            dgvDepartamentos.Columns[0].HeaderText = "ID";
            dgvDepartamentos.Columns[1].HeaderText = "Departamento";
        }


        // Limpia los controles del formulario
        public void LimpiarEntradas()
        {
            txtID.Text = string.Empty;
            txtNombre.Text = string.Empty;
            ActualizarEstadoBotones(true, false, false, false);
        }


        // Actualiza el estado de habilitación de los botones
        private void ActualizarEstadoBotones(bool agregar, bool borrar, bool modificar, bool cancelar)
        {
            btnAgregar.Enabled = agregar;
            btnBorrar.Enabled = borrar;
            btnModificar.Enabled = modificar;
            btnCancelar.Enabled = cancelar;
        }

        // Eventos de navegación


        // Cancela la operación actual y limpia el formulario
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarEntradas();
        }


        // Vuelve al formulario principal
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }


        // Cierra la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}