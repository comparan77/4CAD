using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using ModelCasc.catalog;
using System.Text.RegularExpressions;

namespace ModelCasc.operation
{
    internal class Entrada_fondeoMng: dbTable
    {
        #region Campos
        protected Entrada_fondeo _oEntrada_fondeo;
        protected List<Entrada_fondeo> _lst;
        private StringBuilder _sbQry { get; set; }
        #endregion

        #region Propiedades
        public Entrada_fondeo O_Entrada_fondeo { get { return _oEntrada_fondeo; } set { _oEntrada_fondeo = value; } }
        public List<Entrada_fondeo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_fondeoMng()
        {
            this._oEntrada_fondeo = new Entrada_fondeo();
            this._lst = new List<Entrada_fondeo>();
        }

        public Entrada_fondeoMng(StringBuilder sb)
            : this()
        {
            this._sbQry = sb;
            this._sbQry.AppendLine();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_fondeo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oEntrada_fondeo.Fecha);
            GenericDataAccess.AddInParameter(this.comm, "?P_importador", DbType.String, this._oEntrada_fondeo.Importador);
            GenericDataAccess.AddInParameter(this.comm, "?P_aduana", DbType.String, this._oEntrada_fondeo.Aduana);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_fondeo.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_factura", DbType.String, this._oEntrada_fondeo.Factura);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oEntrada_fondeo.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.String, this._oEntrada_fondeo.Orden);
            GenericDataAccess.AddInParameter(this.comm, "?P_vendor", DbType.String, this._oEntrada_fondeo.Vendor);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oEntrada_fondeo.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_valorfactura", DbType.Double, this._oEntrada_fondeo.Valorfactura);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oEntrada_fondeo.Folio);
        }

        protected void BindByDataRow(DataRow dr, Entrada_fondeo o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
                o.Importador = dr["importador"].ToString();
                o.Aduana = dr["aduana"].ToString();
                o.Referencia = dr["referencia"].ToString();
                o.Factura = dr["factura"].ToString();
                o.Codigo = dr["codigo"].ToString();
                o.Orden = dr["orden"].ToString();
                o.Vendor = dr["vendor"].ToString();
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["valorfactura"] != DBNull.Value)
                {
                    Double.TryParse(dr["valorfactura"].ToString(), out doble);
                    o.Valorfactura = doble;
                    doble = 0;
                }
                o.Folio = dr["folio"].ToString();
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_fondeo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_fondeo o = new Entrada_fondeo();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(1);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_fondeo);
                    
                    DataTable dtVendor = ds.Tables[1];
                    this._oEntrada_fondeo.LstClienteVendor = new List<Cliente_vendor>();
                    foreach (DataRow drVendor in dtVendor.Rows)
                    {
                        Cliente_vendor oCV = new Cliente_vendor();
                        int.TryParse(drVendor["id"].ToString(), out entero);
                        oCV.Id = entero;
                        entero = 0;
                        oCV.Codigo = drVendor["codigo"].ToString();
                        oCV.Nombre = drVendor["nombre"].ToString();
                        this._oEntrada_fondeo.LstClienteVendor.Add(oCV);
                    }

                    DataTable dtMercancia = ds.Tables[2];
                    this._oEntrada_fondeo.LstClienteMercancia = new List<Cliente_mercancia>();
                    foreach (DataRow drMercancia in dtMercancia.Rows)
                    {
                        Cliente_mercancia oCM = new Cliente_mercancia();
                        int.TryParse(drMercancia["id"].ToString(), out entero);
                        oCM.Id = entero;
                        entero = 0;
                        oCM.Codigo = drMercancia["codigo"].ToString();
                        oCM.Nombre = drMercancia["nombre"].ToString();
                        this._oEntrada_fondeo.LstClienteMercancia.Add(oCM);
                    }

                    DataTable dtInventario = ds.Tables[3];
                    Entrada_inventarioMng oEIMng = new Entrada_inventarioMng();
                    Entrada_inventario oEI = new Entrada_inventario();
                    if (dtInventario.Rows.Count == 1)
                    {
                        DataRow drEI = dtInventario.Rows[0];
                        oEIMng.BindByDataRow(drEI, oEI);
                        DataTable dtInvDet = ds.Tables[4];
                        Entrada_inventario_detailMng oEIDMng = new Entrada_inventario_detailMng();
                        List<Entrada_inventario_detail> lstEID = new List<Entrada_inventario_detail>();
                        foreach (DataRow drEID in dtInvDet.Rows)
                        {
                            Entrada_inventario_detail oEID = new Entrada_inventario_detail();
                            oEIDMng.BindByDataRow(drEID, oEID);
                            lstEID.Add(oEID);
                        }
                        oEI.LstEntInvDet = lstEID;

                        DataTable dtInvLote = ds.Tables[5];
                        Entrada_inventario_loteMng oEILMng = new Entrada_inventario_loteMng();
                        List<Entrada_inventario_lote> lstEIL = new List<Entrada_inventario_lote>();
                        foreach (DataRow drEIL in dtInvLote.Rows)
                        {
                            Entrada_inventario_lote oEIL = new Entrada_inventario_lote();
                            oEILMng.BindByDataRow(drEIL, oEIL);
                            lstEIL.Add(oEIL);
                        }
                        oEI.LstEntInvLote = lstEIL;

                    }



                    this._oEntrada_fondeo.PEntInv = oEI;

                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                else
                    throw new Exception("No existe información para el registro solicitado");
            }
            catch
            {
                throw;
            }
        }

        public override void add()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_fondeo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void InitializeInsert(int id_usuario)
        {
            this._sbQry.Append("delete from entrada_fondeo_paso where id = " + id_usuario + ";").AppendLine();
            this._sbQry.Append("INSERT INTO entrada_fondeo_paso (id, fecha, importador, aduana, referencia, factura, codigo, orden, vendor, piezas, valorfactura) VALUES");
            this._sbQry.AppendLine();
        }

        internal void AddValuesInsert(int id_usuario, bool IsLastValue = false)
        {
            try
            {
                Regex rgx = new Regex(@"^[0-9]{4}-[0-9]{7}$");
                if (!rgx.IsMatch(this._oEntrada_fondeo.Referencia))
                    throw new Exception("La referencia " + this._oEntrada_fondeo.Referencia + ", no tiene el formato correcto");

                this._sbQry.Append("(");
                //this._sbQry.Append(",'" + this._oPu_paso.Fecha_fact.ToString("yyyy-MM-dd") + "'");
                this._sbQry.Append(id_usuario);
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Fecha.ToString("yyyy-MM-dd") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Importador.Replace("'", "") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Aduana.Replace("'", "") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Referencia.Replace("'", "") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Factura.Replace("'", "") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Codigo.Replace("'", "") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Orden.Replace("'", "") + "'");
                this._sbQry.Append(",'" + this.O_Entrada_fondeo.Vendor.Replace("'", "") + "'");
                this._sbQry.Append("," + this.O_Entrada_fondeo.Piezas);
                this._sbQry.Append("," + this.O_Entrada_fondeo.Valorfactura);
                //this._sbQry.Append(",'" + this.O_Entrada_fondeo.Folio.Replace("'", "") + "'");
                this._sbQry.Append(")" + (IsLastValue ? ";" : ",")).AppendLine();

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal int execInserts()
        {
            int rowInserted = 0;
            try
            {
                if (this._sbQry.Length > 0)
                {
                    this.comm = GenericDataAccess.CreateCommand(this._sbQry.ToString());
                    rowInserted = GenericDataAccess.ExecuteNonQuery(this.comm);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return rowInserted;
        }

        internal void validaCodigos()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_fondeo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_fondeo o = new Entrada_fondeo();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void validaVendors()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_fondeo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_fondeo o = new Entrada_fondeo();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal int insertFromFondeoPaso(IDbTransaction trans)
        {
            int rowAfected = 0;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(7);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                rowAfected = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch (Exception)
            {

                throw;
            }
            return rowAfected;
        }

        internal void selByReferencia(bool withDetail = true)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(withDetail ? 8 : 10);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_fondeo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_fondeo o = new Entrada_fondeo();
                    BindByDataRow(dr, o);
                    if (withDetail)
                        o.Estatus = dr["estatus"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal bool ExisxteEntradaPrevia()
        {
            bool Existe = false;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(9);
                int numero = Convert.ToInt32( GenericDataAccess.ExecuteScalar(this.comm));
                Existe = numero > 0;
            }
            catch
            {
                throw;
            }
            return Existe;
        }

        internal void dltFondeoPaso(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_fondeo");
                addParameters(11);
                if (trans != null)
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
