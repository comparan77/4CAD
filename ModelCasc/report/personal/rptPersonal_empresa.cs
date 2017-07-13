using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.personal
{
    public class rptPersonal_empresa
    {
        public int Id_empresa { get; set; }
        public string Empresa { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Curp { get; set; }
        public string Nss { get; set; }
        public bool Genero { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public int Edad { get; set; }

        public DateTime Fecha { get; set; }
        public string entrada { get; set; }
        public string salida_comida { get; set; }
        public string entrada_comida { get; set; }
        public string salida { get; set; }
        public string entrada_extra { get; set; }
        public string salida_extra { get; set; }
    }
}
