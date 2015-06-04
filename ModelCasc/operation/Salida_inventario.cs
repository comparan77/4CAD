using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_inventario
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int _id_entrada_inventario;
        protected int _id_usuario;
        protected string _referencia;
        protected string _pedimento;
        protected string _codigo;
        protected string _orden;
        protected int _pallet;
        protected int _bulto;
        protected int _pieza;
        protected DateTime _fecha;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Pedimento { get { return _pedimento; } set { _pedimento = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Orden { get { return _orden; } set { _orden = value; } }
        public int Pallet { get { return _pallet; } set { _pallet = value; } }
        public int Bulto { get { return _bulto; } set { _bulto = value; } }
        public int Pieza { get { return _pieza; } set { _pieza = value; } }
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        #endregion

        #region Constructores
        public Salida_inventario()
        {
            this._id_entrada = 0;
            this._id_entrada_inventario = 0;
            this._id_usuario = 0;
            this._referencia = String.Empty;
            this._pedimento = String.Empty;
            this._codigo = String.Empty;
            this._orden = String.Empty;
            this._pallet = 0;
            this._bulto = 0;
            this._pieza = 0;
            this._fecha = default(DateTime);
        }
        #endregion
    }
}
