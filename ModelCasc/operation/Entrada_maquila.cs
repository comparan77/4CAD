using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_maquila
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected int _id_entrada;
        protected int _id_usuario;
        protected int _id_entrada_inventario;
        protected DateTime _fecha_trabajo;
        protected int _pallet;
        protected int _bulto;
        protected int _pieza;
        protected int _pieza_danada;
        protected int _bulto_faltante;
        protected int _bulto_sobrante;
        protected int _pieza_faltante;
        protected int _pieza_sobrante;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public DateTime Fecha_trabajo { get { return _fecha_trabajo; } set { _fecha_trabajo = value; } }
        public int Pallet { get { return _pallet; } set { _pallet = value; } }
        public int Bulto { get { return _bulto; } set { _bulto = value; } }
        public int Pieza { get { return _pieza; } set { _pieza = value; } }
        public int Pieza_danada { get { return _pieza_danada; } set { _pieza_danada = value; } }
        public int Bulto_faltante { get { return _bulto_faltante; } set { _bulto_faltante = value; } }
        public int Bulto_sobrante { get { return _bulto_sobrante; } set { _bulto_sobrante = value; } }
        public int Pieza_faltante { get { return _pieza_faltante; } set { _pieza_faltante = value; } }
        public int Pieza_sobrante { get { return _pieza_sobrante; } set { _pieza_sobrante = value; } }
        public List<Entrada_maquila_detail> LstEntMaqDet { get; set; }
        public bool Maquila_abierta { get; set; } 
        #endregion

        #region Constructores
        public Entrada_maquila()
        {
            this._id_cliente = 0;
            this._id_entrada = 0;
            this._id_usuario = 0;
            this._id_entrada_inventario = 0;
            this._fecha_trabajo = default(DateTime);
            this._pallet = 0;
            this._bulto = 0;
            this._pieza = 0;
            this._pieza_danada = 0;
            this._bulto_faltante = 0;
            this._bulto_sobrante = 0;
            this._pieza_faltante = 0;
            this._pieza_sobrante = 0;
        }
        #endregion
    }
}
