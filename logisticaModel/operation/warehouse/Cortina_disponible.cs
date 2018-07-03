using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.operation.warehouse
{
    public class Cortina_disponible
    {
        #region Campos
        protected int _id;
        protected int _id_usuario;
        protected int _id_cliente;
        protected string _cliente;
        protected int _id_bodega;
        protected string _bodega;
        protected int _id_cortina;
        protected string _cortina;
        protected int _por_recibir;
        protected int _tarimas;
        protected DateTime _inicio;
        protected DateTime _fin;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Cliente { get { return _cliente; } set { _cliente = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public string Bodega { get { return _bodega; } set { _bodega = value; } }
        public int Id_cortina { get { return _id_cortina; } set { _id_cortina = value; } }
        public string Cortina { get { return _cortina; } set { _cortina = value; } }
        public int Por_recibir { get { return _por_recibir; } set { _por_recibir = value; } }
        public int Tarimas { get { return _tarimas; } set { _tarimas = value; } }
        //[JsonIgnore()]
        public DateTime Inicio { get { return _inicio; } set { _inicio = value; } }
        //[JsonIgnore()]
        public DateTime Fin { get { return _fin; } set { _fin = value; } }
        #endregion

        #region Constructores
        public Cortina_disponible()
        {
            this._id_usuario = 0;
            this._id_cliente = 0;
            this._cliente = String.Empty;
            this._id_bodega = 0;
            this._bodega = String.Empty;
            this._id_cortina = 0;
            this._cortina = String.Empty;
            this._inicio = default(DateTime);
            this._fin = default(DateTime);
        }
        #endregion
    }
}
