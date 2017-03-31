using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ModelCasc.operation
{
    public class Entrada_aud_mer : UsrActivity, IAuditoriaCAEApp
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_pre_carga;
        protected string _informa;
        protected string _referencia;
        protected string _notificado;
        protected bool _entrada_unica;
        protected int _no_entrada;
        protected int _bulto_declarado;
        protected int _bulto_recibido;
        protected int _bulto_abierto;
        protected int _bulto_danado;
        protected int _pallet;
        protected string _acta_informativa;
        protected DateTime _fecha;
        protected string _vigilante;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_pre_carga { get { return _id_entrada_pre_carga; } set { _id_entrada_pre_carga = value; } }
        public string Informa { get { return _informa; } set { _informa = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Notificado { get { return _notificado; } set { _notificado = value; } }
        public bool Entrada_unica { get { return _entrada_unica; } set { _entrada_unica = value; } }
        public int No_entrada { get { return _no_entrada; } set { _no_entrada = value; } }
        public int Bulto_declarado { get { return _bulto_declarado; } set { _bulto_declarado = value; } }
        public int Bulto_recibido { get { return _bulto_recibido; } set { _bulto_recibido = value; } }
        public int Bulto_abierto { get { return _bulto_abierto; } set { _bulto_abierto = value; } }
        public int Bulto_danado { get { return _bulto_danado; } set { _bulto_danado = value; } }
        public int Pallet { get { return _pallet; } set { _pallet = value; } }
        public string Acta_informativa { get { return _acta_informativa; } set { _acta_informativa = value; } } 
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Vigilante { get { return _vigilante; } set { _vigilante = value; } }
        public List<Entrada_aud_mer_files> PLstEntAudMerFiles { get; set; }

        [JsonIgnore()]
        public int Id_fk { get { return this._id_entrada_pre_carga; } set { this._id_entrada_pre_carga = value; } }
        [JsonIgnore()]
        public List<IAudImage> PLstAudImg
        {
            get
            {
                return PLstEntAudMerFiles.Cast<IAudImage>().ToList();
            }
            set
            {
                PLstEntAudMerFiles = value.Cast<Entrada_aud_mer_files>().ToList();
            }
        }
        [JsonIgnore()]
        public string Relato { get { return this.Acta_informativa; } set { this.Acta_informativa = value; } }
        [JsonIgnore()]
        public string Vigilancia { get { return this.Vigilante; } set { this.Vigilante = value; } }
        public string prefixImg
        {
            get { return "aud_mer_"; }
        }
        [JsonIgnore()]
        public IAuditoriaCAECppMng Mng { get { return new Entrada_aud_merMng(); } }
        #endregion

        #region Constructores
        public Entrada_aud_mer()
		{
			this._id_entrada_pre_carga = 0;
			this._informa = String.Empty;
			this._referencia = String.Empty;
			this._notificado = String.Empty;
			this._entrada_unica = false;
			this._no_entrada = 0;
            this._bulto_declarado = 0;
            this._bulto_recibido = 0;
			this._bulto_abierto = 0;
			this._bulto_danado = 0;
			this._pallet = 0;
			this._acta_informativa = String.Empty;
			this._vigilante = String.Empty;
		}
        #endregion
    }
}
