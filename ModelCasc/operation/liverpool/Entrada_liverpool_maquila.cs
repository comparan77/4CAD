using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.liverpool
{
    public class Entrada_liverpool_maquila
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_liverpool;
        protected int _piezas;
        protected DateTime _fecha_maq;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_liverpool { get { return _id_entrada_liverpool; } set { _id_entrada_liverpool = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public DateTime Fecha_maq { get { return _fecha_maq; } set { _fecha_maq = value; } }
        public Entrada_liverpool_servicio PEntLivSer { get; set; }
        #endregion

        #region Constructores
        public Entrada_liverpool_maquila()
        {
            this._id_entrada_liverpool = 0;
            this._piezas = 0;
            this._fecha_maq = default(DateTime);
        }
        #endregion
    }
}
