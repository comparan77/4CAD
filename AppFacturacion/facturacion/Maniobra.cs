using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppCasc.facturacion
{
    internal class Maniobra
    {
        private double _valor;
        private int _cantidad;
        private double _porcentaje;

        private double _tarifa;
        private string _tipo;

        public Maniobra(double valor, int cantidad, double tarifa)
        {
            this._valor = valor;
            this._cantidad = cantidad;
            this._porcentaje = Math.Round(this._valor / tarifa * 100, 0);
        }

        public Maniobra(string tipo, double tarifa)
        {
            this._tipo = tipo;
            this._tarifa = tarifa;
        }

        public double Valor { get { return _valor; } set { _valor = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public double Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }

        public double Tarifa { get { return _tarifa; } set { _tarifa = value; } }
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
    }
}
