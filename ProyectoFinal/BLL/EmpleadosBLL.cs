using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.BLL
{
    internal class EmpleadosBLL
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo {  get; set; }
        public int Departamento { get; set; }
        public byte[] FotoEmpleado { get; set; }
    }
}
