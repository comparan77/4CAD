using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppCasc.facturacion
{
    internal class OtrosServicios
    {
        private double _valor;
        private int _cantidad;
        private string _nombre;

        public OtrosServicios()
        {
            _valor = 0;
            _cantidad = 0;
            _nombre = string.Empty;
        }

        public double Valor { get { return _valor; } set { _valor = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
    }
}
