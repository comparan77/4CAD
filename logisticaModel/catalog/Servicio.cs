using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Servicio
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _descripcion;
        protected int _id_periodo;
        protected int _periodo_valor;
        protected string _campo_cantidad;
        protected string _campo_importe;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public int Id_periodo { get { return _id_periodo; } set { _id_periodo = value; } }
        public int Periodo_valor { get { return _periodo_valor; } set { _periodo_valor = value; } }
        public string Campo_cantidad { get { return _campo_cantidad; } set { _campo_cantidad = value; } }
        public string Campo_importe { get { return _campo_importe; } set { _campo_importe = value; } }
        public Servicio_periodo PServPer { get; set; }
        #endregion

        #region Constructores
        public Servicio()
        {
            this._nombre = String.Empty;
            this._descripcion = String.Empty;
            this._id_periodo = 0;
            this._periodo_valor = 0;
            this._campo_cantidad = String.Empty;
            this._campo_importe = String.Empty;
        }
        #endregion
    }
}
