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

        public frmDepartamentos()
        {
            //Utiliza la clase DAL Departamentos -> Pasa objeto que tiene infor de la GUI
            oDepartamentosDAL = new DepartamentosDAL(); //Instanciando el objeto
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Evento al hacer click en 'Agregar'
            MessageBox.Show("Conectado ... ");
            oDepartamentosDAL.Agregar(RecuperarInformacion());
        }

        //Metodo que devuelve un objeto
        private DepartamentoBLL RecuperarInformacion()
        {
            //Crear una instancia utilizando clase
            DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL();

            int ID = 0; int.TryParse(txtID.Text, out ID);

            oDepartamentoBLL.ID = ID;

            oDepartamentoBLL.Departamento = txtNombreDepartamento.Text;

            return oDepartamentoBLL;

        }
    }
}
