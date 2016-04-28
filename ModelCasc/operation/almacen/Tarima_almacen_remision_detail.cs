using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_remision_detail
    {
        #region Campos
        protected int _id;
        protected int _id_tarima_almacen_remision;
        protected int _id_entrada;
        protected string _mercancia_codigo;
        protected string _rr;
        protected string _estandar;
        protected int _tarimas;
        protected int _cajas;
        protected int _piezas;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_tarima_almacen_remision { get { return _id_tarima_almacen_remision; } set { _id_tarima_almacen_remision = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public string Mercancia_codigo { get { return _mercancia_codigo; } set { _mercancia_codigo = value; } }
        public string Rr { get { return _rr; } set { _rr = value; } }
        public string Estandar { get { return _estandar; } set { _estandar = value; } }
        public int Tarimas { get { return _tarimas; } set { _tarimas = value; } }
        public int Cajas { get { return _cajas; } set { _cajas = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public int Cargadas { get; set; }
        public List<Tarima_almacen> PLstTarAlm;
        #endregion

        #region Constructores
        public Tarima_almacen_remision_detail()
        {
            this._id_tarima_almacen_remision = 0;
            this._id_entrada = 0;
            this._mercancia_codigo = String.Empty;
            this._rr = String.Empty;
            this._estandar = String.Empty;
            this._tarimas = 0;
            this._cajas = 0;
            this._piezas = 0;
        }
        #endregion
    }
}
