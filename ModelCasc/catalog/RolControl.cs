using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using ModelCasc.exception;

namespace ModelCasc.catalog
{
    public class RolControl
    {

        //public static void ValidPermission(enumRol eRol, Page page)
        //{

        //    if (string.Compare(page.Request.Url.AbsolutePath, "/default.aspx") == 0)
        //        return;

        //    List<string> lstAdministrador = new List<string>();

        //    List<string> lstEjecutivo = new List<string>();
            
        //    List<string> lstOperador = new List<string>();
            
        //    try
        //    {
        //        switch (eRol)
        //        {
        //            case enumRol.Administrador:
        //                lstAdministrador.Add("/catalog/frmBodega.aspx");
        //                lstAdministrador.Add("/catalog/frmBodegaLst.aspx");
        //                lstAdministrador.Add("/catalog/frmClienteGrupo.aspx");
        //                lstAdministrador.Add("/catalog/frmClienteGrupoLst.aspx");
        //                lstAdministrador.Add("/catalog/frmCliente.aspx");
        //                lstAdministrador.Add("/catalog/frmClienteLst.aspx");
        //                lstAdministrador.Add("/catalog/frmCortina.aspx");
        //                lstAdministrador.Add("/catalog/frmCortinaLst.aspx");
        //                lstAdministrador.Add("/catalog/frmCuentaTipo.aspx");
        //                lstAdministrador.Add("/catalog/frmCuentaTipoLst.aspx");
        //                lstAdministrador.Add("/catalog/frmCustodia.aspx");
        //                lstAdministrador.Add("/catalog/frmCustodiaLst.aspx");
        //                lstAdministrador.Add("/catalog/frmDocumento.aspx");
        //                lstAdministrador.Add("/catalog/frmDocumentoLst.aspx");
        //                lstAdministrador.Add("/catalog/frmRol.aspx");
        //                lstAdministrador.Add("/catalog/frmRolLst.aspx");
        //                lstAdministrador.Add("/catalog/frmTransporte.aspx");
        //                lstAdministrador.Add("/catalog/frmTransporteLst.aspx");
        //                lstAdministrador.Add("/catalog/frmTransporteTipo.aspx");
        //                lstAdministrador.Add("/catalog/frmTransporteTipoLst.aspx");
        //                lstAdministrador.Add("/catalog/frmTransporteTipoTransporte.aspx");
        //                lstAdministrador.Add("/catalog/frmUsuario.aspx");
        //                lstAdministrador.Add("/catalog/frmUsuarioLst.aspx");
        //                lstAdministrador.Add("/catalog/frmVigilante.aspx");
        //                lstAdministrador.Add("/catalog/frmVigilanteLst.aspx");
        //                lstAdministrador.Add("/report/frmchart.aspx");
        //                lstAdministrador.Add("/report/frmGeneralChart.aspx");
        //                lstAdministrador.Add("/report/frmTransporteChart.aspx");
        //                lstAdministrador.Add("/report/frmChart.aspx");
        //                lstAdministrador.Add("/report/frmCancelMov.aspx");
        //                lstAdministrador.Add("/operation/frmCancelDoc.aspx");
        //                lstAdministrador.Add("/operation/frmEditMov.aspx");
        //                lstAdministrador.Add("/operation/frmRelEntSal.aspx");
        //                lstAdministrador.Add("/operation/frmReporter.aspx");
        //                if (!lstAdministrador.Contains(page.Request.Url.AbsolutePath))
        //                    throw new ExPermission();
        //                break;
        //            case enumRol.Ejecutivo:
        //                lstEjecutivo.Add("/report/frmChart.aspx");
        //                lstEjecutivo.Add("/report/frmGeneralChart.aspx");
        //                lstEjecutivo.Add("/report/frmTransporteChart.aspx");
        //                if (!lstEjecutivo.Contains(page.Request.Url.AbsolutePath))
        //                    throw new ExPermission();
        //                break;
        //            case enumRol.Operador:
        //                lstOperador.Add("/operation/frmEntradas.aspx");
        //                lstOperador.Add("/operation/frmSalidas.aspx");
        //                lstOperador.Add("/operation/frmRelEntSal.aspx");
        //                lstOperador.Add("/report/frmChart.aspx");
        //                if (!lstOperador.Contains(page.Request.Url.AbsolutePath))
        //                    throw new ExPermission();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (ExPermission)
        //    {
        //        page.Response.Redirect("~/default.aspx");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
