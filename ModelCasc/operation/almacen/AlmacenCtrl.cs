using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;
using System.Data;
using Model;

namespace ModelCasc.operation.almacen
{
    public class AlmacenCtrl
    {
        #region Tarima Almacen

        public static void tarimaAlmacenEstandarAdd(Entrada oE, IDbTransaction trans)
        {
            try
            {
                Tarima_almacen_estandar o = oE.PTarAlmEstd;
                o.Id_entrada = oE.Id;
                o.Rr = oE.Referencia;
                Tarima_almacen_estandarMng oMng = new Tarima_almacen_estandarMng()
                {
                    O_Tarima_almacen_estandar = o
                };
                oMng.add(trans);
            }
            catch 
            {
                throw;
            }
        }

        public static void tarimaAlmacenAdd(Entrada oE, IDbTransaction trans)
        {
            try
            {
                int btoResiduo = oE.No_bulto_recibido % oE.PTarAlmEstd.Cajasxtarima;
                int tarCompleta = oE.No_bulto_recibido / oE.PTarAlmEstd.Cajasxtarima;
                Tarima_almacen o;
                Tarima_almacenMng oMng = new Tarima_almacenMng();
                for (int iTar = 1; iTar <= tarCompleta; iTar++)
                {
                    o = new Tarima_almacen()
                    {
                        Id_entrada = oE.Id,
                        Bultos = oE.PTarAlmEstd.Cajasxtarima,
                        Piezas = oE.PTarAlmEstd.Cajasxtarima * oE.PTarAlmEstd.Piezasxcaja,
                        Folio = FolioCtrl.getFolio(enumTipo.TAR, trans),
                        Mercancia_codigo = oE.Mercancia,
                        Mercancia_nombre = oE.PCliente.PClienteMercancia.Nombre,
                        Rr = oE.Referencia,
                        Estandar = oE.PTarAlmEstd.Cajasxtarima + "*" + oE.PTarAlmEstd.Piezasxcaja.ToString()
                    };
                    oMng.O_Tarima_almacen = o;
                    oMng.add(trans);
                }

                if (btoResiduo != 0)
                {
                    o = new Tarima_almacen()
                    {
                        Id_entrada = oE.Id,
                        Bultos = btoResiduo,
                        Piezas = btoResiduo * oE.PTarAlmEstd.Piezasxcaja,
                        Folio = FolioCtrl.getFolio(enumTipo.TAR, trans),
                        Mercancia_codigo = oE.Mercancia,
                        Mercancia_nombre = oE.PCliente.PClienteMercancia.Nombre,
                        Rr = oE.Referencia,
                        Estandar = btoResiduo.ToString() + "*" + oE.PTarAlmEstd.Piezasxcaja.ToString()
                    };
                    oMng.O_Tarima_almacen = o;
                    oMng.add(trans);
                }

                Tarima_almacen_restoMng oTARestoMng = new Tarima_almacen_restoMng();
                foreach (Tarima_almacen itemTAResto in oE.PLstTarAlm)
                {
                    itemTAResto.Id_entrada = oE.Id;
                    itemTAResto.Folio = FolioCtrl.getFolio(enumTipo.TAR, trans);
                    itemTAResto.Mercancia_codigo = oE.Mercancia;
                    itemTAResto.Mercancia_nombre = oE.PCliente.PClienteMercancia.Nombre;
                    itemTAResto.Rr = oE.Referencia;
                    itemTAResto.Id_salida = null;
                    oMng.O_Tarima_almacen = itemTAResto;
                    oMng.add(trans);
                    foreach (Tarima_almacen_resto itemTARestoDet in itemTAResto.PLTAResto)
                    {
                        itemTARestoDet.Id_tarima_almacen = itemTAResto.Id;
                        oTARestoMng.O_Tarima_almacen_resto = itemTARestoDet;
                        oTARestoMng.add(trans);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static List<Tarima_almacen> tarimaAlmacenGetByRR(string rr)
        {
            List<Tarima_almacen> lst = new List<Tarima_almacen>();
            try
            {
                Tarima_almacen o = new Tarima_almacen() { Rr = rr };
                Tarima_almacenMng oMng = new Tarima_almacenMng() { O_Tarima_almacen = o };
                oMng.fillLstByRR(true);
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Tarima_almacen> tarimaAlacenDistinctGetBy(Tarima_almacen o)
        {
            List<Tarima_almacen> lst = new List<Tarima_almacen>();
            try
            {
                Tarima_almacenMng oMng = new Tarima_almacenMng() { O_Tarima_almacen = o };
                oMng.fillLstDistinctBy();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Tarima_almacen> tarimaAlacenFillByCode(string mercancia_codigo)
        {
            List<Tarima_almacen> lst = new List<Tarima_almacen>();
            try
            {
                Tarima_almacenMng oMng = new Tarima_almacenMng() { O_Tarima_almacen = new Tarima_almacen() { Mercancia_codigo = mercancia_codigo } };
                oMng.fillLstByCode();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static List<Tarima_almacen> tarimaAlacenFillByEntrada(int id_entrada)
        {
            List<Tarima_almacen> lst = new List<Tarima_almacen>();
            try
            {
                Tarima_almacenMng oMng = new Tarima_almacenMng() { O_Tarima_almacen = new Tarima_almacen() { Id_entrada = id_entrada } };
                oMng.fillLstByEntrada();
                lst = oMng.Lst;
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static void tarimaAlmacenSetSalida(int id_orden_carga, int id_salida, IDbTransaction trans)
        {
            try
            {
                //El id_entrada se utiliza para asignar el id de la orden de carga
                Tarima_almacen o = new Tarima_almacen() { Id_entrada = id_orden_carga, Id_salida = id_salida };
                Tarima_almacenMng oMng = new Tarima_almacenMng() { O_Tarima_almacen = o };
                oMng.SetSalida(trans);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Trafico
        public static void traficoAdd(Tarima_almacen_trafico o)
        {
            try
            {
                Tarima_almacen_traficoMng oTATMng = new Tarima_almacen_traficoMng();
                oTATMng.O_Tarima_almacen_trafico = o;
                oTATMng.add();
            }
            catch
            {
                throw;
            }
        }

        public static List<Tarima_almacen_trafico> traficoGetAvailableToRem()
        {
            List<Tarima_almacen_trafico> lst = new List<Tarima_almacen_trafico>();
            try
            {
                Tarima_almacen_traficoMng oTATMng = new Tarima_almacen_traficoMng();
                oTATMng.fillLstAvailableToRem();
                lst = oTATMng.Lst;
            }
            catch (Exception)
            {
                
                throw;
            }
            return lst;
        }

        public static List<FullCalendar> traficoLstWithRem(DateTime firstDate)
        {
            List<FullCalendar> lst = new List<FullCalendar>();
            try
            {
                Tarima_almacen_traficoMng oMng = new Tarima_almacen_traficoMng() { O_Tarima_almacen_trafico = new Tarima_almacen_trafico() { Fecha_cita = firstDate } };
                oMng.LstWithRem();
                for (int i = 0; i < oMng.Lst.Count; i++)
                {
                    Tarima_almacen_trafico o = oMng.Lst[i];
                    DateTime dtCita = DateTime.ParseExact(string.Concat(new string[] { Convert.ToDateTime(o.Fecha_cita).ToString("yyyy-MM-dd"), " ", o.Hora_cita }), "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.CurrentCulture);

                    lst.Add(new FullCalendar() { id = o.Id, title = o.Folio_cita, start = dtCita, end = dtCita.AddMinutes(40), id_orden_carga = o.PCarga.Id, folio_orden_carga = o.PCarga.Folio_orden_carga });
                }
            }
            catch
            {
                throw;
            }
            return lst;
        }

        public static Tarima_almacen_trafico traficoGetById(int id_tarima_almacen_trafico)
        {
            Tarima_almacen_trafico o = new Tarima_almacen_trafico() { Id = id_tarima_almacen_trafico };
            o.PLstRem = new List<Tarima_almacen_remision>();
            try
            {
                Tarima_almacen_traficoMng oMngST = new Tarima_almacen_traficoMng() { O_Tarima_almacen_trafico = o };
                oMngST.selById();

                o.PSalidaDestino = new Salida_destino() { Id = o.Id_salida_destino };
                Salida_destinoMng oMngSD = new Salida_destinoMng() { O_Salida_destino = o.PSalidaDestino };
                oMngSD.selById();

                o.PTransporte = new Transporte() { Id = Convert.ToInt32(o.Id_transporte) };
                TransporteMng oMngT = new TransporteMng() { O_Transporte = o.PTransporte };
                oMngT.selById();

                o.PTransporteTipo = new Transporte_tipo() { Id = Convert.ToInt32(o.Id_transporte_tipo_cita) };
                Transporte_tipoMng oMngTT = new Transporte_tipoMng() { O_Transporte_tipo = o.PTransporteTipo };
                oMngTT.selById();

                Tarima_almacen_remisionMng oMng = new Tarima_almacen_remisionMng() { O_Tarima_almacen_remision = new Tarima_almacen_remision() { Id_tarima_almacen_trafico = id_tarima_almacen_trafico } };
                oMng.selByIdTrafico();
                o.PLstRem = oMng.Lst;

                foreach (Tarima_almacen_remision itemSR in oMng.Lst)
                {
                    Tarima_almacen_remision_detail oSRD = new Tarima_almacen_remision_detail() { Id_tarima_almacen_remision = itemSR.Id };
                    Tarima_almacen_remision_detailMng oSRDMng = new Tarima_almacen_remision_detailMng() { O_Tarima_almacen_remision_detail = oSRD };
                    oSRDMng.fillLstByIdRemision();

                    itemSR.PLstTARDet = oSRDMng.Lst;

                    //Se obtiene el estandar de bultos por pallet
                    //int numPallet = EntradaCtrl.InventarioGetPalletsByBultos(Convert.ToInt32(itemSR.Id_entrada_inventario), itemSR.LstSRDetail.Sum(p => p.Bulto));
                    //itemSR.Pallet = numPallet;
                }
            }
            catch
            {
                throw;
            }
            return o;
        }

        #endregion trafico

        #region Transporte Condicion
        public static List<Entrada_transporte_condicion> entradaTransporteCondicionGet(int id_entrada_transporte)
        {
            List<Entrada_transporte_condicion> lst = new List<Entrada_transporte_condicion>();
            try
            {
                Entrada_transporte_condicionMng oMng = new Entrada_transporte_condicionMng()
                {
                    O_Entrada_transporte_condicion = new Entrada_transporte_condicion()
                    {
                        Id_entrada_transporte = id_entrada_transporte
                    }
                };
                oMng.fillLstEntrada();
                lst = oMng.Lst;
            }
            catch (Exception)
            {
                
                throw;
            }
            return lst;
        }
        #endregion

        #region Remision

        public static int tarimaRemisionAdd(Tarima_almacen_remision o)
        {
            int id = 0;
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                o.Folio = FolioCtrl.getFolio(enumTipo.RA, trans);

                Tarima_almacen_remisionMng oTARMng = new Tarima_almacen_remisionMng();
                oTARMng.O_Tarima_almacen_remision = o;
                oTARMng.add(trans);
                id = o.Id;

                Tarima_almacen_remision_detailMng oTARDetMng = new Tarima_almacen_remision_detailMng();
                foreach (Tarima_almacen_remision_detail itemTARDet in o.PLstTARDet)
                {
                    itemTARDet.Id_tarima_almacen_remision = id;
                    oTARDetMng.O_Tarima_almacen_remision_detail = itemTARDet;
                    oTARDetMng.add(trans);
                }
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if(trans!=null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return id;
        }

        public static List<Tarima_almacen_remision> tarimaRemisionFindByAllByCode(string mercancia_codigo)
        {
            List<Tarima_almacen_remision> lst = new List<Tarima_almacen_remision>();
            try
            {
                Tarima_almacen_remision o = new Tarima_almacen_remision() { Mercancia_codigo = mercancia_codigo };
                Tarima_almacen_remisionMng Mng = new Tarima_almacen_remisionMng();
                Mng.O_Tarima_almacen_remision = o;
                Mng.fillLstByCode();
                lst = Mng.Lst;
            }
            catch
            {
                throw;   
            }
            return lst;
        }

        public static Tarima_almacen_remision tarimaRemisionGetAllInfoById(int id_remision)
        {
            Tarima_almacen_remision o = new Tarima_almacen_remision();
            try
            {
                Tarima_almacen_remisionMng oMng = new Tarima_almacen_remisionMng();
                o.Id = id_remision;
                oMng.O_Tarima_almacen_remision = o;
                oMng.selById();

                Tarima_almacen_remision_detailMng oTARDetMng = new Tarima_almacen_remision_detailMng();
                Tarima_almacen_remision_detail oTARDet = new Tarima_almacen_remision_detail() { Id_tarima_almacen_remision = id_remision };
                oTARDetMng.O_Tarima_almacen_remision_detail = oTARDet;
                oTARDetMng.fillLstByIdRemision();
                o.PLstTARDet = oTARDetMng.Lst;

                Tarima_almacen_traficoMng oTATMng = new Tarima_almacen_traficoMng();
                Tarima_almacen_trafico oTAT = new Tarima_almacen_trafico() { Id = o.Id_tarima_almacen_trafico };
                oTATMng.O_Tarima_almacen_trafico = oTAT;
                oTATMng.selById();

                TransporteMng oTMng = new TransporteMng();
                Transporte oT = new Transporte();
                oT.Id = Convert.ToInt32(oTAT.Id_transporte);
                oTMng.O_Transporte = oT;
                oTMng.selById();
                oTAT.PTransporte = oT;

                Transporte_tipoMng oTTMng = new Transporte_tipoMng();
                Transporte_tipo oTT = new Transporte_tipo();
                oTT.Id = oTAT.Id_transporte_tipo;
                oTTMng.O_Transporte_tipo = oTT;
                oTTMng.selById();
                oTAT.PTransporteTipo = oTT;

                o.PTarAlmTrafico = oTAT;

                UsuarioMng oUMng = new UsuarioMng();
                Usuario oU = new Usuario() { Id = o.Id_usuario_elaboro };
                oUMng.O_Usuario = oU;
                oUMng.selById();
                o.PUsuario = oU;

            }
            catch
            {
                throw;
            }
            return o;
        }

        #endregion

        #region Remision Detail
        
        public static List<Tarima_almacen_remision_detail> tarimaRemisionDetCargas(int id_tarima_almacen_remision)
        {
            List<Tarima_almacen_remision_detail> lst = new List<Tarima_almacen_remision_detail>();
            try
            {
                Tarima_almacen_remision_detailMng oMng = new Tarima_almacen_remision_detailMng()
                {
                    O_Tarima_almacen_remision_detail = new Tarima_almacen_remision_detail()
                    {
                        Id_tarima_almacen_remision = id_tarima_almacen_remision
                    }
                };
                oMng.fillLstCargas();
                lst = oMng.Lst;
            }
            catch 
            {
                throw;
            }
            return lst;
        }

        public static Tarima_almacen_remision_detail tarimaRemisionDetCargasDet(int id_tarima_almacen_remision_detail)
        {
            Tarima_almacen_remision_detail o = new Tarima_almacen_remision_detail() { Id = id_tarima_almacen_remision_detail };
            try
            {
                Tarima_almacen_remision_detailMng oMng = new Tarima_almacen_remision_detailMng()
                {
                    O_Tarima_almacen_remision_detail = o
                };
                oMng.selCargasById();
            }
            catch
            {
                throw;
            }
            return o;
        }
        
        #endregion

        #region Orden Carga

        public static Tarima_almacen_carga CargaRpt(int idOc)
        {
            Tarima_almacen_carga o = new Tarima_almacen_carga() { Id = idOc };
            try
            {
                Tarima_almacen_cargaMng oMng = new Tarima_almacen_cargaMng();
                oMng.O_Tarima_almacen_carga = o;
                oMng.fillFormat();

                oMng.selById();

                o.PTarAlmTrafico = traficoGetById(o.Id_tarima_almacen_trafico);
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static Tarima_almacen_carga CargaUdtFolioProv(int id_orden_carga)
        {
            Tarima_almacen_carga o = new Tarima_almacen_carga { Id = id_orden_carga };
            IDbTransaction trans = null;
            try
            {
                Tarima_almacen_cargaMng oMng = new Tarima_almacen_cargaMng() { O_Tarima_almacen_carga = o };
                oMng.selById();
                if (o.Folio_orden_carga.StartsWith("PRV"))
                {
                    trans = GenericDataAccess.BeginTransaction();
                    o.Folio_orden_carga = FolioCtrl.getFolio(enumTipo.OCA, trans);
                    oMng.udtFolio(trans);

                    GenericDataAccess.CommitTransaction(trans);
                }
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return o;
        }

        public static Tarima_almacen_carga CargaForArribo(string folio_oc)
        {
            Tarima_almacen_carga o = new Tarima_almacen_carga() { Folio_orden_carga = folio_oc};
            try
            {
                Tarima_almacen_cargaMng oMng = new Tarima_almacen_cargaMng();
                oMng.O_Tarima_almacen_carga = o;
                oMng.selByFolio();
                oMng.fillForArribo();
                o.PTarAlmTrafico = traficoGetById(o.Id_tarima_almacen_trafico);
            }
            catch
            {
                throw;
            }
            return o;
        }

        #endregion

        #region Orden Carga Detail

        public static void Carga_Detail(Tarima_almacen_carga_detail oTACDet, int id_usuario)
        {
            IDbTransaction trans = null;
            try
            {
                Tarima_almacen_remision_detail oTARDet = new Tarima_almacen_remision_detail() { Id = oTACDet.Id_tarima_almacen_remision_detail };
                Tarima_almacen_remision_detailMng oTARMngDet = new Tarima_almacen_remision_detailMng() { O_Tarima_almacen_remision_detail = oTARDet };
                oTARMngDet.selById();

                Tarima_almacen_remision oTAR = new Tarima_almacen_remision() { Id = oTARDet.Id_tarima_almacen_remision };
                Tarima_almacen_remisionMng oTARMng = new Tarima_almacen_remisionMng() { O_Tarima_almacen_remision = oTAR };
                oTARMng.selById();

                trans = GenericDataAccess.BeginTransaction();

                Tarima_almacen_carga oTAC = new Tarima_almacen_carga() { Id_tarima_almacen_trafico = oTAR.Id_tarima_almacen_trafico };
                Tarima_almacen_cargaMng oTACMng = new Tarima_almacen_cargaMng() { O_Tarima_almacen_carga = oTAC };
                oTACMng.selByIdTrafico(trans);

                if(oTAC.Id <= 0)
                {
                    oTAC.Folio_orden_carga = FolioCtrl.getFolio(enumTipo.PRV, trans);
                    oTAC.Id_usuario = id_usuario;
                    oTAC.Id_tipo_carga = 1;
                    oTACMng.add(trans);
                }

                //Tarima_almacen_carga_detail oTACDet = new Tarima_almacen_carga_detail() { Id_tarima_almacen = id_tar_alm };
                Tarima_almacen_carga_detailMng oTACDetMng = new Tarima_almacen_carga_detailMng() { O_Tarima_almacen_carga_detail = oTACDet };
                oTACDetMng.selByIdTar(trans);

                if (oTACDet.Id > 0)
                {
                    oTACDetMng.dlt(trans);
                }
                else
                {
                    oTACDet.Id_tarima_almacen_carga = oTAC.Id;
                    oTACDetMng.add(trans);
                }

                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
        }

        #endregion
    }
}
