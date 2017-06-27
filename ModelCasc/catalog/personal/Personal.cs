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
        protected string _nombre;
        protected string _paterno;
        protected string _materno;
        protected string _rfc;
        protected string _curp;
        protected string _nss;
        protected bool _genero;
        protected int _id_personal_empresa;
        protected int _id_personal_rol;
        protected bool _boletinado;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Paterno { get { return _paterno; } set { _paterno = value; } }
        public string Materno { get { return _materno; } set { _materno = value; } }
        public string Rfc { get { return _rfc; } set { _rfc = value; } }
        public string Curp { get { return _curp; } set { _curp = value; } } 
        public string Nss { get { return _nss; } set { _nss = value; } }
        public bool Genero { get { return _genero; } set { _genero = value; } }
        public int Id_personal_empresa { get { return _id_personal_empresa; } set { _id_personal_empresa = value; } }
        public int Id_personal_rol { get { return _id_personal_rol; } set { _id_personal_rol = value; } }
        public bool Boletinado { get { return _boletinado; } set { _boletinado = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        public List<Personal_archivos> lstArchivos { get; set; }
        public Personal_qr PQr { get; set; }
        public Personal_registro PerReg { get; set; }
        public Personal_empresa PerEmp { get; set; }
        public string RutaFiles { get; set; }
        #endregion

        #region Constructores
        public Personal()
        {
            this._nombre = String.Empty;
            this._paterno = String.Empty;
            this._materno = String.Empty;
            this._rfc = String.Empty;
            this._curp = String.Empty;
            this._nss = String.Empty;
            this._genero = false;
            this._id_personal_empresa = 0;
            this._id_personal_rol = 0;
            this._boletinado = false;
            this._IsActive = false;
        }
        #endregion
    }
}
