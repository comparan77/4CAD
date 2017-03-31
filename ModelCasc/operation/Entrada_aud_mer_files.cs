using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_aud_mer_files : IAudImage
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_aud_mer;
        protected string _path;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_aud_mer { get { return _id_entrada_aud_mer; } set { _id_entrada_aud_mer = value; } }
        public string Path { get { return _path; } set { _path = value; } }
        public int Id_operation_aud
        {
            get
            {
                return _id_entrada_aud_mer;
            }
            set
            {
                _id_entrada_aud_mer = value;
            }
        }
        #endregion

        #region Constructores
        public Entrada_aud_mer_files()
        {
            this._id_entrada_aud_mer = 0;
            this._path = String.Empty;
        }
        #endregion
    }
}
