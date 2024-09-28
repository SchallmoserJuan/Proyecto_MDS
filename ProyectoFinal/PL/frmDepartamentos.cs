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
    public partial class frmDepartamentos : Form
    {
        DepartamentosDAL oDepartamentosDAL;

        //Constructor que se ejecuta apenas se inicia la GUI
        public frmDepartamentos()
        {
            //Utiliza la clase DAL Departamentos -> Pasa objeto que tiene infor de la GUI
            oDepartamentosDAL = new DepartamentosDAL(); //Instanciando el objeto
            InitializeComponent();
            LlenarGrid();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Evento al hacer click en 'Agregar'
            oDepartamentosDAL.Agregar(RecuperarInformacion());
            LlenarGrid();
        }

        //Metodo que devuelve un objeto
        private DepartamentoBLL RecuperarInformacion()
        {
            //Crear una instancia utilizando clase
            DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL();

            int ID = 0; int.TryParse(txtID.Text, out ID);

            oDepartamentoBLL.ID = ID;

            oDepartamentoBLL.Departamento = txtNombre.Text;

            return oDepartamentoBLL;

        }

        private void Seleccionar(object sender, DataGridViewCellMouseEventArgs e)
        {
            int indice = e.RowIndex;

            txtID.Text = dgvDepartamentos.Rows[indice].Cells[0].Value.ToString();
            txtNombre.Text = dgvDepartamentos.Rows[indice].Cells[1].Value.ToString();
        }

        //Evento para borrar
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            oDepartamentosDAL.Eliminar(RecuperarInformacion());
            LlenarGrid();
        }

        //Evento para modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            oDepartamentosDAL.Modificar(RecuperarInformacion());
            LlenarGrid();
        }

        //Metodo
        public void LlenarGrid()
        {
            dgvDepartamentos.DataSource = oDepartamentosDAL.MostrarDepartamentos().Tables[0]; //Actualizar la tabla

        }
    }
}
