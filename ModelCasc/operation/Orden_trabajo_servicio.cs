using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.operation.liverpool;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    [Serializable]
    public class Orden_trabajo_servicio
    {
        #region Campos
        protected int _id;
        protected int _id_orden_trabajo;
        protected int _id_servicio;
        protected int _id_etiqueta_tipo;
        protected int _piezas;
        protected string _ref1;
        protected string _ref2;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_orden_trabajo { get { return _id_orden_trabajo; } set { _id_orden_trabajo = value; } }
        public int Id_servicio { get { return _id_servicio; } set { _id_servicio = value; } }
        public int Id_etiqueta_tipo { get { return _id_etiqueta_tipo; } set { _id_etiqueta_tipo = value; } } 
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public string Ref1 { get { return _ref1; } set { _ref1 = value; } }
        public string Ref2 { get { return _ref2; } set { _ref2 = value; } }
        public Servicio PServ { get; set; }
        public Entrada_liverpool PEntLiv { get; set; }
        public List<Maquila> PLstMaq { get; set; }
        public List<Maquila_paso> PLstPasos { get; set; }
        public int PiezasMaq { get; set; }
        public int PasosMaq { get; set; }
        public int Faltantes { get; set; }
        public int Sobrantes { get; set; }
        #endregion

        #region Constructores
        public Orden_trabajo_servicio()
        {
            this._id_orden_trabajo = 0;
            this._id_servicio = 0;
            this._id_etiqueta_tipo = 0;
            this._piezas = 0;
            this._ref1 = String.Empty;
            this._ref2 = String.Empty;
        }
        #endregion

        
    }
}
