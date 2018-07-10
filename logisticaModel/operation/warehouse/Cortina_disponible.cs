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
        protected int _id_cortina;
        protected int _id_asn;
        protected int _tarima_x_recibir;
        protected int _tarima_recibida;
        protected DateTime _inicio;
        protected DateTime _fin;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int Id_cortina { get { return _id_cortina; } set { _id_cortina = value; } }
        public int Id_asn { get { return _id_asn; } set { _id_asn = value; } }
        public int Tarima_x_recibir { get { return _tarima_x_recibir; } set { _tarima_x_recibir = value; } }
        public int Tarima_recibida { get { return _tarima_recibida; } set { _tarima_recibida = value; } }
        //[JsonIgnore()]
        public DateTime Inicio { get { return _inicio; } set { _inicio = value; } }
        //[JsonIgnore()]
        public DateTime Fin { get { return _fin; } set { _fin = value; } }
        #endregion

        #region Constructores
        public Cortina_disponible()
        {
            this._id_usuario = 0;
            this._id_cortina = 0;
            this._id_asn = 0;
            this._tarima_x_recibir = 0;
            this._tarima_recibida = 0;
            this._inicio = default(DateTime);
            this._fin = default(DateTime);
        }
        #endregion
    }
}
