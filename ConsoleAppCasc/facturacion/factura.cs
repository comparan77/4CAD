using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.operation;

namespace ConsoleAppCasc.facturacion
{
    internal class factura
    {
        private string _referencia;
        private string _capacidadFlete;
        private double _maniobraEntradaSalida;
        private int _maniobraEntradaSalidaCantidad;
        private double _maniobraEntradaSalidaTotal;
        private double _etiquetaAduana19x10;
        private double _etiquetaAduana10x10;
        private int _piezas19x10;
        private int _piezas10x10;
        private List<Transporte> _lstTransporte;
        private List<Maniobra> _lstManiobra;
        private List<OtrosServicios> _lstOtrosServicios;
        private Emplayado _PEmplayado;
        private Tarima _PTarima;
        private double _total;

        public Entrada_inventario PEntInv { get; set; }

        public factura()
        {
            this._referencia = string.Empty;
            this._etiquetaAduana10x10 = 0;
            this._etiquetaAduana19x10 = 0;
            this._piezas19x10 = 0;
            this._piezas10x10 = 0;
            this._lstTransporte = new List<Transporte>();
            this._lstManiobra = new List<Maniobra>();
            this._lstOtrosServicios = new List<OtrosServicios>();
            this._PEmplayado = new Emplayado();
            this._PTarima = new Tarima();
            this._total = 0;
        }

        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string CapacidadFlete { get { return _capacidadFlete; } set { _capacidadFlete = value; } }
        public double ManiobraEntradaSalida { get { return _maniobraEntradaSalida; } set { _maniobraEntradaSalida = value; } }
        public int ManiobraEntradaSalidaCantidad { get { return _maniobraEntradaSalidaCantidad; } set { _maniobraEntradaSalidaCantidad = value; } }
        public double ManiobraEntradaSalidaTotal { get { return _maniobraEntradaSalidaTotal; } set { _maniobraEntradaSalidaTotal = value; } }
        public double EtiquetaAduana19x10 { get { return _etiquetaAduana19x10; } set { _etiquetaAduana19x10 = value; } }
        public double EtiquetaAduana10x10 { get { return _etiquetaAduana10x10; } set { _etiquetaAduana10x10 = value; } }
        public int Piezas19x10 { get { return _piezas19x10; } set { _piezas19x10 = value; } }
        public int Piezas10x10 { get { return _piezas10x10; } set { _piezas10x10 = value; } }
        public List<Transporte> LstTransporte { get { return _lstTransporte; } set { _lstTransporte = value; } }
        public List<Maniobra> LstManiobra { get { return _lstManiobra; } set { _lstManiobra = value; } }
        public List<OtrosServicios> LstOtros { get { return _lstOtrosServicios; } set { _lstOtrosServicios = value; } }
        public Emplayado PEmplayado { get { return _PEmplayado; } set { _PEmplayado = value; } }
        public Tarima PTarima { get { return _PTarima; } set { _PTarima = value; } }
        public double Total { get { return _total; } set { _total = value; } }
    }
}
