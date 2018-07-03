using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.process
{
    public class Proforma_concentrado
    {
        #region Campos
        protected string _cliente;
        protected int _id_cliente;
        protected int _id_servicio;
        protected DateTime _fecha_servicio;
        protected string _nombre_servicio;
        protected int _cantidad;
        protected float _tarifa;
        protected float _total;
        protected string _folio_aplicada;
        #endregion

        #region Propiedades
        public string Cliente { get { return _cliente; } set { _cliente = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public int Id_servicio { get { return _id_servicio; } set { _id_servicio = value; } }
        public DateTime Fecha_servicio { get { return _fecha_servicio; } set { _fecha_servicio = value; } }
        public string Nombre_servicio { get { return _nombre_servicio; } set { _nombre_servicio = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public float Tarifa { get { return _tarifa; } set { _tarifa = value; } }
        public float Total { get { return _total; } set { _total = value; } }
        public string Folio_aplicada { get { return _folio_aplicada; } set { _folio_aplicada = value; } }
        public DateTime Fecha_serv_max { get; set; }
        public DateTime Fecha_serv_min { get; set; }
        #endregion

        #region Constructores
        public Proforma_concentrado()
        {
            this._cliente = String.Empty;
            this._id_cliente = 0;
            this._id_servicio = 0;
            this._fecha_servicio = default(DateTime);
            this._nombre_servicio = String.Empty;
            this._cantidad = 0;
            this._tarifa = 0;
            this._total = 0;
            this._folio_aplicada = string.Empty;
        }
        #endregion
    }
}
