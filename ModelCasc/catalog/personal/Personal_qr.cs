using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ModelCasc.catalog.personal
{
    public class Personal_qr
    {
        #region Campos
        protected int _id;
        protected string _idf;
        protected int _id_personal;
        protected DateTime _fecha_alta;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Idf { get { return _idf; } set { _idf = value; } }
        public int Id_personal { get { return _id_personal; } set { _id_personal = value; } }
        [JsonIgnore()]
        public DateTime Fecha_alta { get { return _fecha_alta; } set { _fecha_alta = value; } }
        public int Id_bodega { get; set; }
        public string Mensaje { get; set; }
        public Personal PPersonal { get; set; }
        public Personal_registro PPerReg { get; set; }
        #endregion

        #region Constructores
        public Personal_qr()
		{
			this._idf = String.Empty;
			this._id_personal = 0;
            this.Mensaje = "Registro exitoso";
		}
        #endregion
    }
}
