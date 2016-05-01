<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmPrintOpWH.aspx.cs" Inherits="AppCasc.operation.almacen.frmPrintOpWH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/frmOperation.css" rel="stylesheet" type="text/css" />
    <script src="../../js/operation/almacen/frmPrintOpWH.js?v1.1.150619_1446" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 id="div-floor-control" style="cursor: n-resize; margin-top: 5px;" class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Reimpresi&oacute;n de Movimientos</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection divForm">

    <div>
        <label>Tipo de Movimiento:</label>
        <select id="ddl_tipo_mov" class="txtSmall">
            <option value="arribo">Arribo</option>
            <option value="embarque">Embarque</option>
        </select>
    </div>
    <div>
        <label>Folio Cita:</label>
        <input type="text" id="txt_folio_cita" />
    </div>
    <div>
        <label>RR:</label>
        <input type="text" id="txt_rr" />
    </div>
    <div>
        <label>C&oacute;digo de Mercanc&iacute;a:</label>
        <input type="text" id="txt_mercancia_codigo" />
    </div>
    <div>
        <label>Folio:</label>
        <input type="text" id="txt_folio" />
    </div>
    <div>
        <button id="btn_search">Buscar</button>
    </div>
    <hr style="border-color: transparent;" />
    <table class="grdCascSmall">
        <thead>
            <tr>
                <th>Cita</th>
                <th>RR</th>
                <th>Folio</th>
                <th>Mercanc&iacute;a</th>
                <th>Nombre</th>
                <th align="center">S&oacute;lo car&aacute;tula</th>
                <th align="center">Imprimir</th>
            </tr>
        </thead>
        <tbody id="tbody_result">
            
        </tbody>
    </table>

</div>

</asp:Content>
