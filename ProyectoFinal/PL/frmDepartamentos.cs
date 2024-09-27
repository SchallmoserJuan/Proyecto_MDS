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
        public frmDepartamentos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Evento al hacer click en 'Agregar'
            RecuperarInformacion();
            conexionDAL conexion = new conexionDAL();
            MessageBox.Show("Conectado ... " + conexion.PruebaConectar("Insert into departamentos (departamento) values ('Diseño')"));
        }

        //Metodo,
        private void RecuperarInformacion()
        {
            //Crear una instancia utilizando clase
            DepartamentoBLL oDepartamento = new DepartamentoBLL();
            int ID = 0; int.TryParse(txtID.Text, out ID);

            oDepartamento.ID = ID;

            oDepartamento.Departamento = txtNombreDepartamento.Text;

            //Imprimiendo los datos recibidos
            MessageBox.Show(oDepartamento.ID.ToString());
            MessageBox.Show(oDepartamento.Departamento);

        }
    }
}
