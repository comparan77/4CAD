using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc
{
    public class Activity_log
    {
        #region Campos
        protected int _id;
        protected int _usuario_id;
        protected string _tabla;
        protected int _tabla_pk;
        protected string _actividad;
        protected string _comentarios;
        protected DateTime _fecha_hora;
        protected int _anio;
        protected int _mes;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Usuario_id { get { return _usuario_id; } set { _usuario_id = value; } }
        public string Tabla { get { return _tabla; } set { _tabla = value; } }
        public int Tabla_pk { get { return _tabla_pk; } set { _tabla_pk = value; } }
        public string Actividad { get { return _actividad; } set { _actividad = value; } }
        public string Comentarios { get { return _comentarios; } set { _comentarios = value; } }
        public DateTime Fecha_hora { get { return _fecha_hora; } set { _fecha_hora = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public int Mes { get { return _mes; } set { _mes = value; } }
        #endregion

        #region Constructores
        public Activity_log()
		{
			this._usuario_id = 0;
			this._tabla = String.Empty;
			this._tabla_pk = 0;
			this._actividad = String.Empty;
			this._comentarios = String.Empty;
			this._anio = 0;
			this._mes = 0;
		}
        #endregion
    }
}
