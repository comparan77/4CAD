<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOrdenCarga.aspx.cs" Inherits="AppCasc.operation.frmOrdenCarga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/mobiscroll-2.1-beta.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../js/mobiscroll-2.1-beta.custom.min.js" type="text/javascript"></script>
    <script src="../js/moment.min.js" type="text/javascript"></script>
    <script src="../js/fullCalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../js/fullCalendar/es.js" type="text/javascript"></script>
<%--    <script src="../js/operation/helperRemDetail.js?v1.1.150619_1446" type="text/javascript"></script>--%>
    <script src="../js/operation/frmOrdenCarga.js?v1.1.150619_1446" type="text/javascript"></script>
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

<div id="guia_embarque" title="Orden de Carga">
    
    <div class="divForm" style="margin-bottom: 10px;">
        <div>
            <label>Cita:</label>
            <input type="text" id="txt_cita" readonly="readonly" class="txtNoBorder txtLarge" />
        </div>
        <div>
            <label>Destino:</label>
            <input type="text" id="txt_destino" readonly="readonly" class="txtNoBorder" />
        </div>
        <div>
            <label>Línea:</label>
            <input type="text" id="txt_linea" readonly="readonly" class="txtNoBorder" />
        </div>
        <div>
            <label>Unidad:</label>
            <input type="text" id="txt_unidad" readonly="readonly" class="txtNoBorder" />
        </div>
    </div>

    <table class="grdCasc" border="1" cellpadding="2" cellspacing="0">
        <thead>
            <tr>
                <th align="left">Referencia</th>
                <th align="left">Folio Remisi&oacute;n</th>
                <th align="left">Orden de Compra</th>
                <th align="left">C&oacute;digo</th>
                <th align="right">Piezas</th>
                <th align="right">Bultos</th>
                <th align="center">Pallets</th>
                <th align="center">Seleccionar</th>
            </tr>
        </thead>
        <tbody id="tbody_remisiones">
        
        </tbody>
        <tfoot>
            <tr>
                <td colspan="4"></td>
                <td align="right" id="td_pieza_total">0</td>
                <td align="right" id="td_bulto_total">0</td>
                <td align="center" id="td_pallet_total">0</td>
                <td>&nbsp;</td>
            </tr>
        </tfoot>
    </table>
</div>

</asp:Content>
