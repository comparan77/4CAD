using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_aud_uni_files : IAudImage
    {
        #region Campos
        protected int _id;
        protected int _id_salida_aud_uni;
        protected string _path;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida_aud_uni { get { return _id_salida_aud_uni; } set { _id_salida_aud_uni = value; } }
        public string Path { get { return _path; } set { _path = value; } }
        #endregion

        #region Constructores
        public Salida_aud_uni_files()
        {
            this._id_salida_aud_uni = 0;
            this._path = String.Empty;
        }
        #endregion


        public int Id_operation_aud
        {
            get
            {
                return this._id_salida_aud_uni;
            }
            set
            {
                this._id_salida_aud_uni = value;
            }
        }
    }
}
