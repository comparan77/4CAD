using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.IO;
using ModelCasc.catalog;
using ModelCasc.operation.liverpool;

namespace ModelCasc.operation
{
    public class MaquilaCtrl
    {
        #region Orden Trabajo

        public static Orden_trabajo OrdenTrabajoAdd(Orden_trabajo o)
        {
            IDbTransaction trans = null;
            try
            {
                trans = GenericDataAccess.BeginTransaction();
                o.Folio = FolioCtrl.getFolio(enumTipo.OT, trans);
                Orden_trabajoMng oMng = new Orden_trabajoMng() { O_Orden_trabajo = o };
                oMng.add(trans);
                Orden_trabajo_servicioMng oOTSMng = new Orden_trabajo_servicioMng();
                foreach (Orden_trabajo_servicio itemOTS in o.PLstOTSer)
                {
                    itemOTS.Id_orden_trabajo = o.Id;
                    oOTSMng.O_Orden_trabajo_servicio = itemOTS;
                    oOTSMng.add(trans);
                }
                GenericDataAccess.CommitTransaction(trans);
            }
            catch
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return o;
        }

        public static List<Orden_trabajo> OrdenTrabajoGet()
        {
            List<Orden_trabajo> lst = new List<Orden_trabajo>();
            try
            {
                Orden_trabajoMng oMng = new Orden_trabajoMng();
                oMng.fillOpen();
                lst = oMng.Lst;

                Orden_trabajo_servicioMng oOTSMng = new Orden_trabajo_servicioMng();

                Entrada_liverpoolMng oELMng = new Entrada_liverpoolMng();
                Entrada_liverpool oEL;

                foreach (Orden_trabajo itemOT in lst)
                {
                    itemOT.PLstOTSer = new List<Orden_trabajo_servicio>();

                    Orden_trabajo_servicio oOTS = new Orden_trabajo_servicio() { Id_orden_trabajo = itemOT.Id };
                    oOTSMng.O_Orden_trabajo_servicio = oOTS;
                    oOTSMng.selByIdOT();

                    foreach (Orden_trabajo_servicio itemOTS in oOTSMng.Lst)
                    {

                        switch (itemOTS.Id_servicio)
                        {
                            case 1: //etiqueta de precio
                                oEL = new Entrada_liverpool() { Trafico = itemOTS.Ref1, Pedido = Convert.ToInt32(itemOTS.Ref2) };
                                oELMng.O_Entrada_liverpool = oEL;
                                oELMng.selByUniqueKey();
                                itemOTS.PEntLiv = oEL;
                                break;
                            default:
                                break;
                        }
                        itemOT.PLstOTSer.Add(itemOTS);
                    }
                }
            }
            catch
            {

                throw;
            }
            return lst;
        }

        #endregion

        #region Maquila

        public static object MaquilaAddLst(List<Maquila> lst, string pathImg)
        {
            int rowsAfected = 0;
            IDbTransaction trans = null;
            try
            {
                MaquilaMng oMng = new MaquilaMng();
                Maquila_pasoMng oMPMng = new Maquila_pasoMng();
                trans = GenericDataAccess.BeginTransaction();
                foreach (Maquila itemMaq in lst)
                {
                    oMng.O_Maquila = itemMaq;
                    oMng.add(trans);

                    //si no cuenta con captura de pasos
                    string path = Path.Combine(pathImg, itemMaq.Id_ord_tbj_srv + @"\");
                    int numFoto = 1;
                    foreach (Maquila_paso itemMP in itemMaq.PLstPasos)
                    {
                        string foto64 = itemMP.Foto64;
                        itemMP.Foto64 = Path.Combine(itemMaq.Id_ord_tbj_srv + @"\" +  numFoto + ".jpg");
                        oMPMng.O_Maquila_paso = itemMP;
                        oMPMng.add(trans);
                        CommonCtrl.AddImgToDirectory(path, numFoto.ToString(), foto64);
                        numFoto++;
                        rowsAfected++;
                    }
                }
                GenericDataAccess.CommitTransaction(trans);
            }
            catch 
            {
                if (trans != null)
                    GenericDataAccess.RollbackTransaction(trans);
                throw;
            }
            return rowsAfected;
        }

        #endregion
    }
}
