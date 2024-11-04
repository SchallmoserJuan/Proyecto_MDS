using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinal.PL;

namespace ProyectoFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // boton para abrir el formulario de departamentos
        private void button1_Click(object sender, EventArgs e)
        {
            frmDepartamentos formularioDepartamentos = new frmDepartamentos();
            formularioDepartamentos.Show();
            this.Hide();
        }

        // boton para abrir el formulario de empleados
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmEmpleados formularioEmpleados = new frmEmpleados();
            formularioEmpleados.Show();
            this.Hide();
        }

        // boton para cerrar la aplicacion
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
