using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    public class FolioCtrl
    {
        private static string addZero(int numDigitos, int folio, int anioDB)
        {
            string dfolio = string.Empty;
            int numZero = numDigitos - folio.ToString().Length;
            for (int i = 0; i < numZero; i++)
            {
                dfolio += "0";
            }
            try
            {
                dfolio = "-" + dfolio + folio.ToString() + "-" + anioDB.ToString().Substring(2, 2);
            }
            catch { }

            return dfolio;
        }

        public static string getFolio(enumTipo tipo, IDbTransaction trans)
        {
            string folio = string.Empty;
            string errMsg = string.Empty;

            FolioMng oMng = new FolioMng();
            Folio o = new Folio();

            try
            {
                //o.Anio_actual = id_bodega;
                o.Tipo = tipo.ToString();
                oMng.O_Folio = o;
                oMng.getFolio(trans);
                folio = addZero(o.Digitos, o.Actual, o.Anio_actual);
                folio = o.Tipo + folio;
            }
            catch (Exception)
            {
                switch (tipo)
                {
                    case enumTipo.E:
                        errMsg = "La bodega no tiene asignación de folios para la Entrada";
                        break;
                    case enumTipo.S:
                        errMsg = "La bodega no tiene asignación de folios para la Salida";
                        break;
                    default:
                        break;
                }
                throw new Exception(errMsg);
            }

            return folio;
        }

        public static string ClienteReferenciaGet(int id_cliente, enumTipo tipo, IDbTransaction trans)
        {
            string referencia = string.Empty;
            string errMsg = string.Empty;

            Cliente_codigo_canceladoMng oCCCMng = new Cliente_codigo_canceladoMng();

            try
            {
                
                switch (tipo)
                {
                    case enumTipo.E:
                        Cliente_codigo_cancelado oCCC = new Cliente_codigo_cancelado() { Id_cliente = id_cliente, Tipo = tipo.ToString() };
                        oCCCMng.O_Cliente_codigo_cancelado = oCCC;

                        oCCCMng.getAvailable(trans);
                        if (oCCC.Codigo.Length > 0)
                            referencia = oCCC.Codigo;
                        else
                        {
                            ClienteMng oCMng = new ClienteMng();
                            Cliente oC = new Cliente() { Id = id_cliente };
                            oCMng.O_Cliente = oC;
                            oCMng.selById(trans);

                            Cliente_codigoMng oCCMng = new Cliente_codigoMng();
                            Cliente_codigo oCC = new Cliente_codigo();
                            oCC.Id_cliente_grupo = oC.Id_cliente_grupo; //El procedimiento usará el parametro para asignar el id del cliente
                            oCCMng.O_Cliente_codigo = oCC;
                            
                            oCCMng.getRefEntByCliente(trans);
                            referencia = oCC.Clave + addZero(oCC.Digitos, oCC.Consec_arribo, oCC.Anio_actual);

                            oCCMng.udtRef(trans);
                        }
                        break;
                    case enumTipo.S:
                        break;
                    default:
                        break;
                }
                //referencia = addZero(o.Digitos, o.Actual, o.Id_bodega);
                //referencia = o.Tipo + referencia;
            }
            catch (Exception)
            {
                throw;
            }

            return referencia;
        }
    }
}
