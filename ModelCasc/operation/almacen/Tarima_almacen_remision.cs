using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_remision
    {
        #region Campos
        protected int _id;
        protected int _id_usuario_elaboro;
        protected int _id_tarima_almacen_trafico;
        protected DateTime _fecha;
        protected string _folio;
        protected string _mercancia_codigo;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_usuario_elaboro { get { return _id_usuario_elaboro; } set { _id_usuario_elaboro = value; } }
        public int Id_tarima_almacen_trafico { get { return _id_tarima_almacen_trafico; } set { _id_tarima_almacen_trafico = value; } }
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Mercancia_codigo { get { return _mercancia_codigo; } set { _mercancia_codigo = value; } }
        public List<Tarima_almacen_remision_detail> PLstTARDet { get; set; }
        public Tarima_almacen_trafico PTarAlmTrafico { get; set; }
        public int TarimaTotal { get; set; }
        public int PiezaTotal { get; set; }
        public int CajaTotal { get; set; }
        public int CargaTotal { get; set; }
        //public Cliente_vendor PClienteVendor { get; set; }
        public Usuario PUsuario { get; set; }
        #endregion

        #region Constructores
        public Tarima_almacen_remision()
        {
            this._id_usuario_elaboro = 0;
            this._id_tarima_almacen_trafico = 0;
            this._fecha = default(DateTime);
            this._folio = String.Empty;
            this._mercancia_codigo = String.Empty;
        }
        #endregion
    }
}
