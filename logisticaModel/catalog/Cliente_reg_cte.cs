using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Cliente_reg_cte
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected int _id_cliente_regimen;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public int Id_cliente_regimen { get { return _id_cliente_regimen; } set { _id_cliente_regimen = value; } }
        #endregion

        #region Constructores
        public Cliente_reg_cte()
        {
            this._id_cliente = 0;
            this._id_cliente_regimen = 0;
        }
        #endregion
    }
}
