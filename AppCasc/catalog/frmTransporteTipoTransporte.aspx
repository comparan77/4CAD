<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmTransporteTipoTransporte.aspx.cs" Inherits="AppCasc.catalog.frmTransporteTipoTransporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/redmond/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="../css/frmCatalog.css" rel="stylesheet" type="text/css" />

    <%--<script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>--%>
    <script src="../js/catalog/frmTransporteTipoTransporte.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:HiddenField runat="server" ID="hfTitleErr" />
<asp:HiddenField runat="server" ID="hfDescErr" />

<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-accordion-header-active ui-state-active ui-corner-top">Asociar Transportes</h3>
<div class="ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content-active contentSection">
<div id="div_TransporteTipoTransporte" class="divForm">

<div>
    <label>Seleccione Transporte:</label>
    <asp:DropDownList runat="server" ID="ddlTransporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporte_changed"></asp:DropDownList>
</div>

<div>
    <div id="TiposAsociados">
        <label>Tipos de Transporte Asociados:</label>
        <asp:ListBox runat="server" ID="lstAsociados" AutoPostBack="true" OnSelectedIndexChanged="lstAsociados_changed" Width="350" Height="150"></asp:ListBox>
    </div>
    <div id="divBtnMove">
        <asp:Button runat="server" ID="btnMove" Text="" Visible="false" CommandArgument="" Enabled="false" OnCommand="move_click" />
    </div>
    <div id="TiposNoAsociados">
        <label>Tipos de Transporte NO Asociados:</label>
        <asp:ListBox runat="server" ID="lstNoAsociados" AutoPostBack="true" OnSelectedIndexChanged="lstNoAsociados_changed" Width="350"></asp:ListBox>
    </div>
</div>

</div>
</div>
</asp:Content>
