using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public enum enumTipo
    {
        SER //Servicio
        , PRF //Proforma
    }
    public class Folio
    {
        #region Campos
        protected int _id;
        protected string _tipo;
        protected int _anio_actual;
        protected int _actual;
        protected int _digitos;
        protected int _folio_inicial;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        public int Anio_actual { get { return _anio_actual; } set { _anio_actual = value; } }
        public int Actual { get { return _actual; } set { _actual = value; } }
        public int Digitos { get { return _digitos; } set { _digitos = value; } }
        public int Folio_inicial { get { return _folio_inicial; } set { _folio_inicial = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Folio()
        {
            this._tipo = String.Empty;
            this._anio_actual = 0;
            this._actual = 0;
            this._digitos = 0;
            this._folio_inicial = 0;
            this._IsActive = false;
        }
        #endregion

    }
}
