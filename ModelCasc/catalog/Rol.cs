using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public enum enumRol
    {
        Mantenimiento = 1,
        Administracion,
        Operacion,
        Ejecutivo,
        GteAlmacen,
        GteMaquila,
        Super,
        AlmAvonSuper,
        Contabilidad,
        Reportes,
        EjecutivoAux,
        GteRecHum,
        CtrlAcceso,
        PrevPerdidas
    }

    public class Rol
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _descripcion;
        private bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Rol()
        {
            this._nombre = String.Empty;
            this._descripcion = string.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
