using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal
    {
        #region Campos
        protected int _id;
        protected string _idf;
        protected string _nombre;
        protected string _paterno;
        protected string _materno;
        protected string _rfc;
        protected string _nss;
        protected int _id_tipo_personal;
        protected bool _activo;
        protected bool _boletinado;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Idf { get { return _idf; } set { _idf = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Paterno { get { return _paterno; } set { _paterno = value; } }
        public string Materno { get { return _materno; } set { _materno = value; } }
        public string Rfc { get { return _rfc; } set { _rfc = value; } }
        public string Nss { get { return _nss; } set { _nss = value; } }
        public int Id_tipo_personal { get { return _id_tipo_personal; } set { _id_tipo_personal = value; } }
        public bool Activo { get { return _activo; } set { _activo = value; } }
        public bool Boletinado { get { return _boletinado; } set { _boletinado = value; } }
        #endregion

        #region Constructores
        public Personal()
        {
            this._idf = String.Empty;
            this._nombre = String.Empty;
            this._paterno = String.Empty;
            this._materno = String.Empty;
            this._rfc = String.Empty;
            this._nss = String.Empty;
            this._id_tipo_personal = 0;
            this._activo = false;
            this._boletinado = false;
        }
        #endregion
    }
}
