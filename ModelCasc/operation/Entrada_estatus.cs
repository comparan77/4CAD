using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_estatus
    {
        #region Campos
        protected int _id;
        protected int _id_usuario;
        protected int _id_entrada_inventario;
        protected int? _id_entrada_maquila;
        protected int? _id_salida_remision;
        protected int _id_estatus_proceso;
        protected DateTime? _fecha;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public int? Id_entrada_maquila { get { return _id_entrada_maquila; } set { _id_entrada_maquila = value; } }
        public int? Id_salida_remision { get { return _id_salida_remision; } set { _id_salida_remision = value; } }
        public int Id_estatus_proceso { get { return _id_estatus_proceso; } set { _id_estatus_proceso = value; } }
        public DateTime? Fecha { get { return _fecha; } set { _fecha = value; } }
        #endregion

        #region Constructores
        public Entrada_estatus()
		{
			this._id_usuario = 0;
			this._id_entrada_inventario = 0;
            this._id_entrada_maquila = null;
            this._id_salida_remision = null;
			this._id_estatus_proceso = 0;
            this._fecha = default(DateTime);
		}
        #endregion
    }
}
