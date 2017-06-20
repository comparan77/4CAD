using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ModelCasc.catalog.personal
{
    public class Personal_archivos
    {
        #region Campos
        protected int _id;
        protected int _id_personal;
        protected int _id_archivo_tipo;
        protected string _ruta;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_personal { get { return _id_personal; } set { _id_personal = value; } }
        public int Id_archivo_tipo { get { return _id_archivo_tipo; } set { _id_archivo_tipo = value; } }
        public string Ruta { get { return _ruta; } set { _ruta = value; } }
        public MemoryStream stream { get; set; }
        #endregion

        #region Constructores
        public Personal_archivos()
        {
            this._id_personal = 0;
            this._id_archivo_tipo = 0;
            this._ruta = String.Empty;
        }
        #endregion
    }
}
