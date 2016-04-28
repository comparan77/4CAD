<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOCWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmOCWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <link href="../../js/qtip/jquery.qtip.min.css" rel="stylesheet" type="text/css" />

    <script src="../../js/qtip/jquery.qtip.min.js" type="text/javascript"></script>
    <script src="../../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../../js/moment.min.js" type="text/javascript"></script>
    <script src="../../js/fullCalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../../js/fullCalendar/es.js" type="text/javascript"></script>
    <script src="../../js/operation/almacen/frmOCWH.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <div>
<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-search" style="cursor: n-resize;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Citas</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">
    <div id="dates_calendar"></div>
</div>
</div>

<div id="guia_embarque" title="Orden de Carga" style="position: relative">

    <div id="step1">

    <div id="div_orden_carga" style="position: absolute; top: 10px; right: 10px;">
        <a id="lnk_orden_carga" href="#"></a>
        <span id="lnk_dlt_orden_carga" class="ui-icon ui-icon-trash icon-button-action dltDiaTrabajado floatRight"></span>
        <input type="hidden" id="h_orden_carga" value="0" />
    </div>
    <div class="divForm" style="margin-bottom: 10px;">
        <div>
            <input type="hidden" id="hf_id_salida_trafico" />
            <label>Cita:</label>
            <input type="text" id="txt_cita" readonly="readonly" class="txtNoBorder txtLarge" />
        </div>
        <div>
            <label>Destino:</label>
            <input type="text" id="txt_destino" readonly="readonly" class="txtNoBorder" />
        </div>
        <div>
            <label>Línea:</label>
            <input type="text" id="txt_linea" readonly="readonly" class="txtNoBorder txtLarge" />
        </div>
        <div>
            <label>Unidad:</label>
            <input type="text" id="txt_unidad" readonly="readonly" class="txtNoBorder" />
        </div>
    </div>

    <table class="grdCasc" border="1" cellpadding="2" cellspacing="0">
        <thead>
            <tr>
                <th align="left">Folio Remisi&oacute;n</th>
                <th align="left">Mercanc&iacute;a C&oacute;digo</th>
                <th align="center">Tarimas Solicitadas</th>
                <th align="center">Cajas</th>
                <th align="center">Piezas</th>
                <th align="center">Tarimas Cargadas</th>
            </tr>
        </thead>
        <tbody id="tbody_remisiones">
        
        </tbody>
        <tfoot>
            <%--<tr>
                <td colspan="4"></td>
                <td align="right" id="td_pieza_total">0</td>
                <td align="right" id="td_bulto_total">0</td>
                <td align="center" id="td_pallet_total">0</td>
                <td>&nbsp;</td>
            </tr>--%>
        </tfoot>
    </table>

    <div class="divForm" style="margin-top: 10px;">
        <div>
            <label>Tipo de Carga:</label>
            <asp:DropDownList runat="server" ID="ddl_tipo_carga"></asp:DropDownList>
        </div>
        <div>
            <button class="floatRight" id="btnSave">Guardar Orden de Carga</button>
        </div>
    </div>

    </div>

    <div id="step2" style="display: none">
    <span class="icon-button-action" id="spn_rem_selected"></span>
    <table class="grdCascSmall" width="100%">
        <thead>
            <tr>
                <th>Mercancia C&oacute;digo</th>
                <th align="center">RR</th>
                <th align="center">Estandar</th>
                <th align="center">Tarimas</th>
                <th align="center">Cajas</th>
                <th align="center">Piezas</th>
                <th align="center">Cargadas</th>
            </tr>
        </thead>
        <tbody id="tbody_cargadas"></tbody>
    </table>
    </div>

    <div id="step3" style="display: none">
    <span class="icon-button-action" id="spn_rem_det_selected"></span>
    <div class="divForm">
        <div>
            <label>Estandar:</label>
            <input type="text" class="txtSmall txtNoBorder" id="txt_estandar_sel" />
        </div>
        <div>
            <label>Tarimas Solicitadas:</label>
            <input type="text" class="txtSmall txtNoBorder" id="txt_tar_sol" />
        </div>
        <div>
            <label>Tarimas Cargadas</label>
            <input type="text" class="txtSmall txtNoBorder" id="txt_tar_car" />
        </div>
    </div>
    <div>
        <ul id="ul_tarimas_disp"></ul>
    </div>
    
</div>

</div>

</asp:Content>
