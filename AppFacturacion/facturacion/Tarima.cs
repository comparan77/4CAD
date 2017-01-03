using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppCasc.facturacion
{
    internal class Tarima
    {
        private double _valor;
        private int _cantidad;

        public Tarima()
        {
            _valor = 0;
            _cantidad = 0;
        }

        public double Valor { get { return _valor; } set { _valor = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
    }
}
