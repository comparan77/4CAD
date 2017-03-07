using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_aud_uni_files
    {
        #region Campos
		protected int _id;
        protected int _id_entrada_aud_uni;
		protected string _path;
		#endregion

		#region Propiedades
		public int Id { get { return _id; } set { _id = value; } } 
		public int Id_entrada_aud_uni { get { return _id_entrada_aud_uni; } set { _id_entrada_aud_uni = value; } } 
		public string Path { get { return _path; } set { _path = value; } } 
		#endregion

		#region Constructores
		public Entrada_aud_uni_files()
		{
            this._id_entrada_aud_uni = 0;
			this._path = String.Empty;
		}
		#endregion
    }
}
